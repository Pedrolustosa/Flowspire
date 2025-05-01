import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ApiResponse } from '../../shared/models/api-response';

export interface AuditLog {
  id: string;
  userId: string | null;
  ipAddress: string | null;
  method: string;
  path: string;
  statusCode: number;
  executionTimeMs: number;
  timestamp: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuditLogService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAuditLogs(page: number = 1, pageSize: number = 10): Observable<ApiResponse<{ data: AuditLog[], totalCount: number }>> {
    return this.http.get<ApiResponse<{ data: AuditLog[], totalCount: number }>>(
      `${this.apiUrl}/auditlogs?page=${page}&pageSize=${pageSize}`
    );
  }  
}
