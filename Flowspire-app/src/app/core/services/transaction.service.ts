import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../../environments/environment';
import { FinancialReport, TransactionDTO } from '../../shared/models/Transaction';
import { ApiResponse } from '../../shared/models/api-response';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private apiUrl = `${environment.apiUrl}/financialtransaction`;

  constructor(private http: HttpClient) {}

  getTransactionById(id: number): Observable<TransactionDTO> {
    return this.http
      .get<ApiResponse<TransactionDTO>>(`${this.apiUrl}/${id}`)
      .pipe(map(res => res.data));
  }

  getAllTransactions(): Observable<TransactionDTO[]> {
    return this.http
      .get<ApiResponse<TransactionDTO[]>>(`${this.apiUrl}`)
      .pipe(map(res => res.data));
  }

  getUserTransactions(): Observable<TransactionDTO[]> {
    return this.http
      .get<ApiResponse<TransactionDTO[]>>(`${this.apiUrl}/user`)
      .pipe(map(res => res.data));
  }

  getFinancialReport(startDate: Date, endDate: Date): Observable<FinancialReport> {
    return this.http
      .get<ApiResponse<FinancialReport>>(`${this.apiUrl}/range`, {
        params: {
          startDate: startDate.toISOString(),
          endDate: endDate.toISOString()
        }
      })
      .pipe(map(res => res.data));
  }

  createTransaction(transactionDto: TransactionDTO): Observable<any> {
    return this.http
      .post<ApiResponse<any>>(`${this.apiUrl}`, transactionDto)
      .pipe(map(res => res.message));
  }

  updateTransaction(id: number, transactionDto: TransactionDTO): Observable<any> {
    return this.http
      .put<ApiResponse<any>>(`${this.apiUrl}/${id}`, transactionDto)
      .pipe(map(res => res.message));
  }

  deleteTransaction(id: number): Observable<any> {
    return this.http
      .delete<ApiResponse<any>>(`${this.apiUrl}/${id}`)
      .pipe(map(res => res.message));
  }
}