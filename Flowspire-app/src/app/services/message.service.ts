import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

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

  constructor() {}

  sendMessage(message: MessageDTO): Observable<MessageDTO> {
    return this.http.post<MessageDTO>(`${this.apiUrl}/send`, message);
  }

  getMessages(otherUserId: string): Observable<MessageDTO[]> {
    return this.http.get<MessageDTO[]>(`${this.apiUrl}/${otherUserId}`);
  }
}