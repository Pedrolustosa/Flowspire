import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, RouterOutlet } from '@angular/router';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SidebarComponent } from './sidebar/sidebar.component';
import { ToastrModule } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    RouterOutlet,
    NgxSpinnerModule,
    ToastrModule,
    SidebarComponent,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isSidebarExpanded = true;

  constructor(
    private router: Router,
  ) {}

  onSidebarToggle(): void {
    this.isSidebarExpanded = !this.isSidebarExpanded;
  }

  shouldShowSidebar(): boolean {
    return !['/login', '/register'].includes(this.router.url);
  }
}