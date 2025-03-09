import { Injectable, inject, PLATFORM_ID } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, forkJoin, timeout, catchError, of, TimeoutError } from 'rxjs';
import { environment } from '../../environments/environment';
import { Balance, Budget, Alert, MonthlyHistory, CategoryTrend, CategorySummary, RecentTransaction, FinancialGoal, DashboardOverview } from '../models/Dashboard';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('accessToken');
    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }

  getDashboard(startDate: Date, endDate: Date): Observable<DashboardOverview> {
    if (isNaN(startDate.getTime()) || isNaN(endDate.getTime())) {
      const now = new Date();
      startDate = new Date(now.getFullYear(), now.getMonth(), 1);
      endDate = new Date(now.getFullYear(), now.getMonth() + 1, 0);
    }

    const params = {
      startDate: startDate.toISOString(),
      endDate: endDate.toISOString()
    };
    return this.http.get<DashboardOverview>(`${this.apiUrl}/dashboard`, {
      headers: this.getHeaders(),
      params
    }).pipe(
      timeout(10000),
      catchError(error => {
        if (error instanceof TimeoutError) {
          console.error('Request timed out');
        }
        return of({} as DashboardOverview);
      })
    );
  }

  getCategorySummary(startDate: Date, endDate: Date, type: 'Expense' | 'Revenue' = 'Expense'): Observable<CategorySummary[]> {
    if (isNaN(startDate.getTime()) || isNaN(endDate.getTime())) {
      const now = new Date();
      startDate = new Date(now.getFullYear(), now.getMonth(), 1);
      endDate = new Date(now.getFullYear(), now.getMonth() + 1, 0);
    }

    const params = {
      startDate: startDate.toISOString(),
      endDate: endDate.toISOString(),
      type
    };
    return this.http.get<CategorySummary[]>(`${this.apiUrl}/dashboard/category-summary`, {
      headers: this.getHeaders(),
      params
    }).pipe(
      timeout(5000),
      catchError(() => of([]))
    );
  }

  getRecentTransactions(limit: number = 5): Observable<RecentTransaction[]> {
    return this.http.get<RecentTransaction[]>(`${this.apiUrl}/dashboard/recent-transactions`, {
      headers: this.getHeaders(),
      params: { limit: limit.toString() }
    }).pipe(
      timeout(5000),
      catchError(() => of([]))
    );
  }

  getCurrentBalance(): Observable<Balance> {
    return this.http.get<Balance>(`${this.apiUrl}/dashboard/current-balance`, {
      headers: this.getHeaders()
    }).pipe(
      timeout(5000),
      catchError(() => of({ totalRevenue: 0, totalExpense: 0, currentBalance: 0 }))
    );
  }

  getFinancialGoals(): Observable<FinancialGoal[]> {
    return this.http.get<FinancialGoal[]>(`${this.apiUrl}/dashboard/financial-goals`, {
      headers: this.getHeaders()
    }).pipe(
      timeout(5000),
      catchError(() => of([]))
    );
  }

  getAllData(startDate: Date, endDate: Date): Observable<any> {
    return forkJoin({
      dashboard: this.getDashboard(startDate, endDate),
      categorySummaryExpense: this.getCategorySummary(startDate, endDate, 'Expense'),
      recentTransactions: this.getRecentTransactions(5),
      currentBalance: this.getCurrentBalance(),
      financialGoals: this.getFinancialGoals()
    });
  }
}