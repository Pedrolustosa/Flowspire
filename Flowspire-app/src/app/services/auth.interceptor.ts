import { HttpInterceptorFn, HttpHandlerFn, HttpRequest, HttpEvent } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> => {
  const accessToken = localStorage.getItem('accessToken');
  let authReq = req;
  if (accessToken) {
    authReq = req.clone({ setHeaders: { Authorization: `Bearer ${accessToken}` } });
  }
  return next(authReq).pipe(
    catchError(error => {
      if (error.status === 401) {
        const refreshToken = localStorage.getItem('refreshToken');
        if (refreshToken) {
          const http = inject(HttpClient);
          return http.post<any>(`${environment.apiUrl}/user/refresh-token`, { refreshToken }).pipe(
            switchMap(response => {
              if (response.accessToken) {
                localStorage.setItem('accessToken', response.accessToken);
                localStorage.setItem('refreshToken', response.refreshToken);
                const newAuthReq = req.clone({ setHeaders: { Authorization: `Bearer ${response.accessToken}` } });
                return next(newAuthReq);
              } else {
                localStorage.removeItem('accessToken');
                localStorage.removeItem('refreshToken');
                return throwError(() => error);
              }
            }),
            catchError(refreshError => {
              localStorage.removeItem('accessToken');
              localStorage.removeItem('refreshToken');
              return throwError(() => refreshError);
            })
          );
        } else {
          localStorage.removeItem('accessToken');
          localStorage.removeItem('refreshToken');
          return throwError(() => error);
        }
      }
      return throwError(() => error);
    })
  );
};
