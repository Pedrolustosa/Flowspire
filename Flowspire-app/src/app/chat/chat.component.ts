import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MessageService, MessageDTO } from '../services/message.service';
import { NotificationService } from '../services/notification.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
})
export class ChatComponent implements OnInit, OnDestroy {
  private messageService = inject(MessageService);
  private notificationService = inject(NotificationService);
  private authService = inject(AuthService);

  messages: MessageDTO[] = [];
  newMessage: string = '';
  otherUserIdInput: string = ''; // Campo para entrada do ID
  otherUserId: string = ''; // ID confirmado após busca
  currentUserId: string | null = null;
  isChatActive: boolean = false; // Controla se o chat está ativo

  ngOnInit(): void {
    this.authService.currentUser$.subscribe((user) => {
      this.currentUserId = user?.id || null;
      if (!this.currentUserId) {
        console.error('Usuário não autenticado');
      }
    });
  }

  ngOnDestroy(): void {
    this.notificationService.stopConnection();
  }

  searchUser(): void {
    if (!this.otherUserIdInput.trim() || !this.currentUserId) return;

    this.otherUserId = this.otherUserIdInput.trim();
    this.isChatActive = true;
    this.loadMessages();
    this.setupSignalR();
  }

  loadMessages(): void {
    if (!this.currentUserId || !this.otherUserId) return;

    this.messageService.getMessages(this.otherUserId).subscribe({
      next: (messages) => {
        this.messages = messages;
        this.scrollToBottom();
      },
      error: (err) => console.error('Erro ao carregar mensagens:', err),
    });
  }

  setupSignalR(): void {
    this.notificationService.startConnection();
    this.notificationService.onReceiveMessage((message) => {
      if (
        (message.senderId === this.currentUserId && message.receiverId === this.otherUserId) ||
        (message.senderId === this.otherUserId && message.receiverId === this.currentUserId)
      ) {
        this.messages.push(message);
        this.scrollToBottom();
      }
    });
  }

  sendMessage(): void {
    if (!this.newMessage.trim() || !this.currentUserId || !this.otherUserId) return;

    const message: MessageDTO = {
      senderId: this.currentUserId,
      receiverId: this.otherUserId,
      content: this.newMessage,
    };

    this.messageService.sendMessage(message).subscribe({
      next: (response) => {
        this.messages.push(response);
        this.newMessage = '';
        this.scrollToBottom();
      },
      error: (err) => console.error('Erro ao enviar mensagem:', err),
    });
  }

  scrollToBottom(): void {
    const container = document.querySelector('.messages-container');
    if (container) {
      container.scrollTop = container.scrollHeight;
    }
  }
}