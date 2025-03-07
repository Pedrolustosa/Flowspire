import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
  ],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
  @Input() isExpanded = true;
  @Output() toggleSidebar = new EventEmitter<boolean>();

  navItems = [
    {
      icon: 'fas fa-chart-line',
      route: '/dashboard',
      label: 'Dashboard'
    },
    {
      icon: 'fas fa-wallet',
      route: '/transactions',
      label: 'Transações'
    }
  ];

  constructor(
    private router: Router,
  ) {}

  handleSidebarToggle(): void {
    this.isExpanded = !this.isExpanded;
    this.toggleSidebar.emit(this.isExpanded);
  }

  isCurrentRoute(route: string): boolean {
    return this.router.url.startsWith(route);
  }

  logout(): void {
    this.router.navigate(['/login']);
  }
}