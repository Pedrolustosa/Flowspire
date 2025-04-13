import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FinancialReport, TransactionDTO } from '../models/Transaction';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private apiUrl = `${environment.apiUrl}/financialtransaction`;

  constructor(private http: HttpClient) {}

  getTransactionById(id: number): Observable<TransactionDTO> {
    return this.http.get<TransactionDTO>(`${this.apiUrl}/${id}`);
  }

  getAllTransactions(): Observable<TransactionDTO[]> {
    return this.http.get<TransactionDTO[]>(`${this.apiUrl}`);
  }

  getUserTransactions(): Observable<TransactionDTO[]> {
    return this.http.get<TransactionDTO[]>(`${this.apiUrl}/user`);
  }

  getFinancialReport(startDate: Date, endDate: Date): Observable<FinancialReport> {
    return this.http.get<FinancialReport>(`${this.apiUrl}/range`, {
      params: {
        startDate: startDate.toISOString(),
        endDate: endDate.toISOString()
      }
    });
  }

  createTransaction(transactionDto: TransactionDTO): Observable<TransactionDTO> {
    return this.http.post<TransactionDTO>(`${this.apiUrl}`, transactionDto);
  }

  updateTransaction(id: number, transactionDto: TransactionDTO): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, transactionDto);
  }

  deleteTransaction(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
