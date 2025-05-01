import { HttpInterceptorFn, HttpHandlerFn, HttpRequest, HttpEvent } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, throwError, of } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../environments/environment';

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> => {
  const accessToken = localStorage.getItem('accessToken');
  const http = inject(HttpClient);
  const router = inject(Router);
  const toastr = inject(ToastrService);

  let authReq = req;
  if (accessToken) {
    authReq = req.clone({ setHeaders: { Authorization: `Bearer ${accessToken}` } });
  }

  return next(authReq).pipe(
    catchError(error => {
      if (error.status === 401) {
        const refreshToken = localStorage.getItem('refreshToken');
        if (refreshToken) {
          return http.post<any>(`${environment.apiUrl}/user/refresh-token`, { refreshToken }).pipe(
            switchMap(response => {
              if (response.success && response.data?.accessToken) {
                localStorage.setItem('accessToken', response.data.accessToken);
                localStorage.setItem('refreshToken', response.data.refreshToken);
                const newAuthReq = req.clone({ setHeaders: { Authorization: `Bearer ${response.data.accessToken}` } });
                return next(newAuthReq);
              } else {
                handleSessionExpired(router, toastr);
                return throwError(() => error);
              }
            }),
            catchError(refreshError => {
              handleSessionExpired(router, toastr);
              return throwError(() => refreshError);
            })
          );
        } else {
          handleSessionExpired(router, toastr);
          return throwError(() => error);
        }
      }
      return throwError(() => error);
    })
  );
};

function handleSessionExpired(router: Router, toastr: ToastrService) {
  localStorage.removeItem('accessToken');
  localStorage.removeItem('refreshToken');
  toastr.warning('Sua sessão expirou. Faça login novamente.', 'Sessão Expirada');
  router.navigate(['/login']).catch(err => {
    console.error('Erro ao redirecionar para login após sessão expirada:', err);
  });
}