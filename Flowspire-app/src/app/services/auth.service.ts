import { Injectable, inject, PLATFORM_ID } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { isPlatformBrowser } from '@angular/common';
import { environment } from '../../environments/environment';
import { User } from '../models/User';
import { LoginRequest } from '../models/LoginRequest';
import { RegisterCustomerRequest } from '../models/RegisterCustomerRequest';
import { RefreshTokenRequest } from '../models/RefreshTokenRequest';
import { UpdateRequest } from '../models/UpdateRequest';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private spinner = inject(NgxSpinnerService);
  private toastr = inject(ToastrService);
  private platformId = inject(PLATFORM_ID);
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

  login(request: LoginRequest): Observable<any> {
    this.spinner.show();
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
            this.getCurrentUser().subscribe();
          } else {
            this.toastr.error('Token de acesso não encontrado na resposta.', 'Erro');
          }
        },
        error: (err) => {
          this.toastr.error(err.error?.Error || 'Credenciais inválidas', 'Erro');
        },
        complete: () => this.spinner.hide()
      })
    );
  }

  register(request: RegisterCustomerRequest): Observable<any> {
    this.spinner.show();
    return this.http.post<any>(`${this.apiUrl}/user/register-customer`, request).pipe(
      tap({
        next: (response) => {
          this.toastr.success(response.Message, 'Sucesso');
          this.router.navigate(['/login']);
        },
        error: (err) => {
          this.toastr.error(err.error?.Error || 'Erro no registro', 'Erro');
        },
        complete: () => this.spinner.hide()
      })
    );
  }

  refreshToken(): Observable<any> {
    const refreshToken = isPlatformBrowser(this.platformId) ? localStorage.getItem('refreshToken') : null;
    if (!refreshToken) {
      this.logout();
      return new Observable(observer => observer.error('No refresh token available'));
    }

    const request: RefreshTokenRequest = { refreshToken };
    this.spinner.show();
    return this.http.post<any>(`${this.apiUrl}/user/refresh-token`, request).pipe(
      tap({
        next: (response) => {
          if (response.AccessToken) {
            if (isPlatformBrowser(this.platformId)) {
              localStorage.setItem('accessToken', response.AccessToken);
              localStorage.setItem('refreshToken', response.RefreshToken);
            }
            this.accessTokenSubject.next(response.AccessToken);
            this.isAuthenticatedSubject.next(true);
            this.toastr.info('Token atualizado com sucesso.', 'Informação');
          }
        },
        error: (err) => {
          this.toastr.error('Erro ao atualizar o token. Faça login novamente.', 'Erro');
          this.logout();
        },
        complete: () => this.spinner.hide()
      })
    );
  }

  updateUser(request: UpdateRequest): Observable<User> {
    this.spinner.show();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.getAccessToken()}`
    });
    return this.http.put<User>(`${this.apiUrl}/user/update`, request, { headers }).pipe(
      tap({
        next: (response) => {
          this.currentUserSubject.next(response);
          this.toastr.success('Perfil atualizado com sucesso!', 'Sucesso');
        },
        error: (err) => {
          this.toastr.error(err.error?.Error || 'Erro ao atualizar o perfil.', 'Erro');
        },
        complete: () => this.spinner.hide()
      })
    );
  }

  getCurrentUser(): Observable<User> {
    this.spinner.show();
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
        },
        complete: () => this.spinner.hide()
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
    return this.accessTokenSubject.value;
  }

  isLoggedIn(): boolean {
    return this.isAuthenticatedSubject.value;
  }

  getCurrentUserValue(): User | null {
    return this.currentUserSubject.value;
  }
}