import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Budget } from '../../shared/models/budget';
import { ApiResponse } from '../../shared/models/api-response';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {
  private apiUrl = `${environment.apiUrl}/budget`;

  constructor(private http: HttpClient) {}

  createBudget(budget: Budget): Observable<void> {
    return this.http.post<ApiResponse<string>>(`${this.apiUrl}`, budget).pipe(map(() => {}));
  }

  getBudgetById(id: number): Observable<Budget> {
    return this.http.get<ApiResponse<Budget>>(`${this.apiUrl}/${id}`).pipe(
      map(res => res.data)
    );
  }

  getUserBudgets(): Observable<Budget[]> {
    return this.http.get<ApiResponse<Budget[]>>(`${this.apiUrl}/user`).pipe(
      map(res => res.data)
    );
  }

  updateBudget(id: number, budget: Budget): Observable<void> {
    return this.http.put<ApiResponse<string>>(`${this.apiUrl}/${id}`, budget).pipe(map(() => {}));
  }

  deleteBudget(id: number): Observable<void> {
    return this.http.delete<ApiResponse<string>>(`${this.apiUrl}/${id}`).pipe(map(() => {}));
  }
}