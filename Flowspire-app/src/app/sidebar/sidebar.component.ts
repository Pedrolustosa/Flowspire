import { Component, Input, Output, EventEmitter, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent {
  @Input() isExpanded = true;
  @Output() toggleSidebar = new EventEmitter<boolean>();

  navItems = [
    { icon: 'fas fa-chart-line', route: '/dashboard', label: 'Dashboard' },
    { icon: 'fas fa-wallet', route: '/transactions', label: 'Transações' },
    { icon: 'fas fa-tags', route: '/category', label: 'Categorias' },
    { icon: 'fas fa-comment-dots', route: '/chat', label: 'Chat' },
    { icon: 'fas fa-user', route: '/profile', label: 'Perfil' },
    { icon: 'fas fa-file-alt', route: '/audit-logs', label: 'Audit Logs' }
  ];

  private authService = inject(AuthService);
  private router = inject(Router);

  handleSidebarToggle(): void {
    this.isExpanded = !this.isExpanded;
    this.toggleSidebar.emit(this.isExpanded);
  }

  isCurrentRoute(route: string): boolean {
    return this.router.url.startsWith(route);
  }

  logout(): void {
    this.authService.logout();
  }
}