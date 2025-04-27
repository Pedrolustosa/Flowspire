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
import { UpdateRequest } from '../models/UpdateRequest';
import { LoadingService } from './loading.service';
import { ApiResponse } from '../models/api-response';

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
        error: () => this.logout()
      });
    }
  }

  login(request: LoginRequest): Observable<ApiResponse<{ accessToken: string; refreshToken: string }>> {
    this.loadingService.setLoading(true);
    return this.http.post<ApiResponse<{ accessToken: string; refreshToken: string }>>(`${this.apiUrl}/user/login`, request).pipe(
      tap({
        next: (response) => {
          if (response.success && response.data?.accessToken) {
            if (isPlatformBrowser(this.platformId)) {
              localStorage.setItem('accessToken', response.data.accessToken);
              localStorage.setItem('refreshToken', response.data.refreshToken);
            }
            this.accessTokenSubject.next(response.data.accessToken);
            this.isAuthenticatedSubject.next(true);
            this.toastr.success(response.message || 'Login realizado com sucesso.', 'Sucesso');

            this.getCurrentUser().subscribe({
              next: () => {
                this.router.navigate(['/dashboard']).catch(() => {});
              },
              error: () => {
                this.toastr.error('Login efetuado, mas houve problema ao carregar seu perfil.', 'Aviso');
                this.router.navigate(['/dashboard']).catch(() => {});
              }
            });

          } else {
            this.toastr.error(response.message || 'Erro no login.', 'Erro');
          }
        },
        error: (err) => {
          this.toastr.error(err.error?.message || 'Erro ao realizar login.', 'Erro');
          this.loadingService.setLoading(false);
        },
        complete: () => this.loadingService.setLoading(false)
      })
    );
  }

  register(request: RegisterCustomerRequest): Observable<ApiResponse<null>> {
    this.loadingService.setLoading(true);
    return this.http.post<ApiResponse<null>>(`${this.apiUrl}/user/register-customer`, request).pipe(
      tap({
        next: (response) => {
          if (response.success) {
            this.toastr.success(response.message || 'Cadastro realizado!', 'Sucesso');
            this.router.navigate(['/login']);
          } else {
            this.toastr.error(response.message || 'Erro no registro.', 'Erro');
          }
        },
        error: (err) => {
          this.toastr.error(err.error?.message || 'Erro no registro.', 'Erro');
          this.loadingService.setLoading(false);
        },
        complete: () => this.loadingService.setLoading(false)
      })
    );
  }

  refreshToken(token: string): Observable<ApiResponse<{ accessToken: string; refreshToken: string }>> {
    const request = { refreshToken: token };
    this.loadingService.setLoading(true);
    return this.http.post<ApiResponse<{ accessToken: string; refreshToken: string }>>(`${this.apiUrl}/user/refresh-token`, request).pipe(
      tap({
        next: (response) => {
          if (response.success && response.data?.accessToken) {
            if (isPlatformBrowser(this.platformId)) {
              localStorage.setItem('accessToken', response.data.accessToken);
              localStorage.setItem('refreshToken', response.data.refreshToken);
            }
            this.accessTokenSubject.next(response.data.accessToken);
            this.isAuthenticatedSubject.next(true);
            this.toastr.info(response.message || 'Token atualizado.', 'Informação');
            this.getCurrentUser().subscribe({
              error: () => this.logout()
            });
          } else {
            this.toastr.error(response.message || 'Erro ao atualizar token.', 'Erro');
            this.logout();
          }
        },
        error: (err) => {
          this.toastr.error(err.error?.message || 'Erro ao atualizar token.', 'Erro');
          this.logout();
          this.loadingService.setLoading(false);
        },
        complete: () => this.loadingService.setLoading(false)
      })
    );
  }

  updateUser(request: UpdateRequest): Observable<ApiResponse<User>> {
    this.loadingService.setLoading(true);
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.getAccessToken()}`
    });
    return this.http.put<ApiResponse<User>>(`${this.apiUrl}/user/update`, request, { headers }).pipe(
      tap({
        next: (response) => {
          if (response.success) {
            this.currentUserSubject.next(response.data);
            this.toastr.success(response.message || 'Perfil atualizado com sucesso.', 'Sucesso');
          } else {
            this.toastr.error(response.message || 'Erro ao atualizar perfil.', 'Erro');
          }
        },
        error: (err) => {
          this.toastr.error(err.error?.message || 'Erro ao atualizar perfil.', 'Erro');
          this.loadingService.setLoading(false);
        },
        complete: () => this.loadingService.setLoading(false)
      })
    );
  }

  getCurrentUser(): Observable<ApiResponse<User>> {
    this.loadingService.setLoading(true);
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.getAccessToken()}`
    });
    return this.http.get<ApiResponse<User>>(`${this.apiUrl}/user/me`, { headers }).pipe(
      tap({
        next: (response) => {
          if (response.success) {
            this.currentUserSubject.next(response.data);
          } else {
            this.toastr.error(response.message || 'Erro ao buscar dados do usuário.', 'Erro');
          }
        },
        error: (err) => {
          this.toastr.error(err.error?.message || 'Erro ao buscar dados do usuário.', 'Erro');
          this.loadingService.setLoading(false);
        },
        complete: () => this.loadingService.setLoading(false)
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