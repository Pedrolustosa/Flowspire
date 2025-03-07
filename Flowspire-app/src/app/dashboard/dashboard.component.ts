import { Component, AfterViewInit } from '@angular/core';
import Chart from 'chart.js/auto';
import { isPlatformBrowser } from '@angular/common';
import { PLATFORM_ID, Inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements AfterViewInit {
  userName: string = '';

  constructor(
    private authService: AuthService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.authService.getCurrentUser().subscribe({
      next: (user) => {
        this.userName = user.fullName;
      }
    });
  }

  ngAfterViewInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.createChart();
    }
  }

  createChart() {
    const ctx = document.getElementById('monthlyChart') as HTMLCanvasElement;
    new Chart(ctx, {
      type: 'bar',
      data: {
        labels: ['Janeiro', 'Fevereiro', 'Março'],
        datasets: [
          {
            label: 'Receitas',
            data: [2000, 1800, 2200],
            backgroundColor: '#34D399',
            borderColor: '#166534',
            borderWidth: 1
          },
          {
            label: 'Despesas',
            data: [800, 950, 600],
            backgroundColor: '#EF4444',
            borderColor: '#991B1B',
            borderWidth: 1
          }
        ]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          y: { beginAtZero: true, title: { display: true, text: 'Valor (R$)' } },
          x: { title: { display: true, text: 'Meses' } }
        },
        plugins: {
          legend: { position: 'top' },
          title: { display: true, text: 'Receitas e Despesas Mensais' }
        }
      }
    });
  }
}