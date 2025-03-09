import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, RouterOutlet } from '@angular/router';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
import { SidebarComponent } from './sidebar/sidebar.component';
import { ToastrModule } from 'ngx-toastr';
import { LoadingService } from './services/loading.service';

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

  private router = inject(Router);
  private loadingService = inject(LoadingService);
  private spinner = inject(NgxSpinnerService);

  constructor() {
    this.loadingService.isLoading$.subscribe(isLoading => {
      if (isLoading) {
        this.spinner.show();
      } else {
        this.spinner.hide();
      }
    });
  }

  onSidebarToggle(event: boolean): void {
    this.isSidebarExpanded = event;
  }

  shouldShowSidebar(): boolean {
    return !['/login', '/register'].includes(this.router.url);
  }
}