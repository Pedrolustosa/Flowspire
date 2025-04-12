// src/app/services/auth.service.ts
import { Injectable, inject, PLATFORM_ID } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { isPlatformBrowser } from '@angular/common';
import { environment } from '../../environments/environment';
import { User } from '../models/User';
import { LoginRequest } from '../models/LoginRequest';
import { RegisterCustomerRequest } from '../models/RegisterCustomerRequest';
import { RefreshTokenRequest } from '../models/RefreshTokenRequest';
import { UpdateRequest, UpdateRequestWrapper } from '../models/UpdateRequest';
import { LoadingService } from './loading.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private toastr = inject(ToastrService);
  private platformId = inject(PLATFORM_ID);
  private loadingService = inject(LoadingService);
  private apiUrl = environment.apiUrl;

  private isAuthenticatedSubject = new BehaviorSubject<boolean>(
    isPlatformBrowser(this.platformId) ? !!localStorage.getItem('accessToken') : false
  );
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  private accessTokenSubject = new BehaviorSubject<string | null>(
    isPlatformBrowser(this.platformId) ? localStorage.getItem('accessToken') : null
  );
  accessToken$ = this.accessTokenSubject.asObservable();

  private currentUserSubject = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSubject.asObservable();

  constructor() {
    if (this.isAuthenticatedSubject.value) {
      this.getCurrentUser().subscribe({
        next: (user) => {
          console.log('User loaded on initialization:', user);
        },
        error: (err) => {
          console.error('Failed to fetch current user on initialization:', err);
          this.logout();
        }
      });
    }
  }

  login(request: LoginRequest): Observable<any> {
    this.loadingService.setLoading(true);
    return this.http.post<any>(`${this.apiUrl}/user/login`, request).pipe(
      tap({
        next: (response) => {
          if (response.accessToken) {
            if (isPlatformBrowser(this.platformId)) {
              localStorage.setItem('accessToken', response.accessToken);
              localStorage.setItem('refreshToken', response.refreshToken);
            }
            this.accessTokenSubject.next(response.accessToken);
            this.isAuthenticatedSubject.next(true);
            this.toastr.success('Login bem-sucedido!', 'Sucesso');
            this.getCurrentUser().subscribe({
              next: (user) => {
                console.log('Current user after login:', user);
              },
              error: (err) => {
                console.error('Failed to fetch current user after login:', err);
                this.logout();
              }
            });
          }
        },
        error: (err) => {
          this.toastr.error(err.error?.Error || 'Credenciais inválidas', 'Erro');
          this.loadingService.setLoading(false);
        },
        complete: () => {
          this.loadingService.setLoading(false);
        }
      })
    );
  }

  register(request: RegisterCustomerRequest): Observable<any> {
    this.loadingService.setLoading(true);
    return this.http.post<any>(`${this.apiUrl}/user/register-customer`, request).pipe(
      tap({
        next: (response) => {
          this.toastr.success(response.Message, 'Sucesso');
          this.router.navigate(['/login']);
        },
        error: (err) => {
          this.toastr.error(err.error?.Error || 'Erro no registro', 'Erro');
          this.loadingService.setLoading(false);
        },
        complete: () => {
          this.loadingService.setLoading(false);
        }
      })
    );
  }

  refreshToken(token: string): Observable<any> {
    const request = { refreshToken: token };
    this.loadingService.setLoading(true);
    return this.http.post<any>(`${this.apiUrl}/user/refresh-token`, request).pipe(
      tap({
        next: (response) => {
          if (response.accessToken) {
            if (isPlatformBrowser(this.platformId)) {
              localStorage.setItem('accessToken', response.accessToken);
              localStorage.setItem('refreshToken', response.refreshToken);
            }
            this.accessTokenSubject.next(response.accessToken);
            this.isAuthenticatedSubject.next(true);
            this.toastr.info('Token atualizado com sucesso.', 'Informação');
            this.getCurrentUser().subscribe({
              error: (err) => {
                console.error('Failed to fetch current user after refresh:', err);
                this.logout();
              }
            });
          }
        },
        error: (err) => {
          console.error('Token refresh failed:', err);
          this.logout();
          this.loadingService.setLoading(false);
        },
        complete: () => {
          this.loadingService.setLoading(false);
        }
      })
    );
  }

  updateUser(request: UpdateRequest): Observable<User> {
    this.loadingService.setLoading(true);
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.getAccessToken()}`
    });
    const body: UpdateRequestWrapper = { request: request };
    return this.http.put<User>(`${this.apiUrl}/user/update`, body, { headers }).pipe(
      tap({
        next: (response) => {
          this.currentUserSubject.next(response);
          this.toastr.success('Perfil atualizado com sucesso!', 'Sucesso');
        },
        error: (err) => {
          this.toastr.error(err.error?.Error || 'Erro ao atualizar o perfil.', 'Erro');
          this.loadingService.setLoading(false);
        },
        complete: () => {
          this.loadingService.setLoading(false);
        }
      })
    );
  }

  getCurrentUser(): Observable<User> {
    this.loadingService.setLoading(true);
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.getAccessToken()}`
    });
    return this.http.get<User>(`${this.apiUrl}/user/me`, { headers }).pipe(
      tap({
        next: (response) => {
          this.currentUserSubject.next(response);
        },
        error: (err) => {
          this.toastr.error(err.error?.Error || 'Erro ao buscar dados do usuário.', 'Erro');
          this.loadingService.setLoading(false);
        },
        complete: () => {
          this.loadingService.setLoading(false);
        }
      })
    );
  }

  logout() {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');
    }
    this.accessTokenSubject.next(null);
    this.isAuthenticatedSubject.next(false);
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']);
    this.toastr.info('Você saiu da conta.', 'Informação');
  }

  getAccessToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem('accessToken');
    }
    return this.accessTokenSubject.value;
  }

  getRefreshToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem('refreshToken');
    }
    return null;
  }

  isLoggedIn(): boolean {
    return this.isAuthenticatedSubject.value;
  }

  getCurrentUserValue(): User | null {
    return this.currentUserSubject.value;
  }
}