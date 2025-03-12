import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TransactionDTO, FinancialReport } from '../models/Transaction';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private apiUrl = `${environment.apiUrl}/transaction`;

  constructor(private http: HttpClient) {}

  addTransaction(transactionDto: TransactionDTO): Observable<any> {
    return this.http.post(`${this.apiUrl}`, transactionDto);
  }

  getTransactionsByUserId(userId: string): Observable<TransactionDTO[]> {
    return this.http.get<TransactionDTO[]>(`${this.apiUrl}/user/${userId}`);
  }

  getFinancialReport(startDate: Date, endDate: Date): Observable<FinancialReport> {
    const validStartDate = startDate instanceof Date && !isNaN(startDate.getTime()) ? startDate : new Date();
    const validEndDate = endDate instanceof Date && !isNaN(endDate.getTime()) ? endDate : new Date();

    return this.http.get<FinancialReport>(`${this.apiUrl}/report`, {
      params: {
        startDate: validStartDate.toISOString(),
        endDate: validEndDate.toISOString()
      }
    });
  }
}