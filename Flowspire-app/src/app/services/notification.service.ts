import { Injectable, inject } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';
import { MessageDTO } from './message.service';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private hubConnection!: signalR.HubConnection;
  private authService = inject(AuthService);

  constructor() {}

  public startConnection(): void {
    const accessToken = this.authService.getAccessToken();
    if (!accessToken) {
      console.error('Usuário não autenticado para conectar ao SignalR');
      return;
    }

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiUrl.replace('/api', '')}/notificationHub`, {
        accessTokenFactory: () => this.authService.getAccessToken() || '',
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Conexão com SignalR estabelecida'))
      .catch((err) => console.error('Erro ao conectar ao SignalR:', err));
  }

  public onReceiveMessage(callback: (message: MessageDTO) => void): void {
    this.hubConnection.on('ReceiveMessage', (message: MessageDTO) => {
      callback(message);
    });
  }

  public stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection
        .stop()
        .then(() => console.log('Conexão com SignalR encerrada'))
        .catch((err) => console.error('Erro ao encerrar SignalR:', err));
    }
  }
}