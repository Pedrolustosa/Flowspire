import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ApiResponse } from '../../shared/models/api-response';

export interface MessageDTO {
  id?: string;
  senderId: string;
  receiverId: string;
  content: string;
  sentAt?: string;
  isRead?: boolean;
  readAt?: string | null;
}

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/message`;

  sendMessage(message: MessageDTO): Observable<MessageDTO> {
    return this.http
      .post<ApiResponse<MessageDTO>>(`${this.apiUrl}/send`, message)
      .pipe(map(res => res.data));
  }

  getMessages(otherUserId: string): Observable<MessageDTO[]> {
    return this.http
      .get<ApiResponse<MessageDTO[]>>(`${this.apiUrl}/${otherUserId}`)
      .pipe(map(res => res.data));
  }
}