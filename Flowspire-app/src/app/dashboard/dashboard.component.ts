import { Component, AfterViewInit, Inject, OnDestroy, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Chart from 'chart.js/auto';
import { isPlatformBrowser } from '@angular/common';
import { PLATFORM_ID } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { DashboardService } from '../services/dashboard.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Balance, Budget, Alert, MonthlyHistory, CategoryTrend, CategorySummary, RecentTransaction, FinancialGoal, DashboardOverview } from '../models/Dashboard';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements AfterViewInit, OnDestroy, OnInit {
  userName: string = '';
  revenue: number = 0;
  expense: number = 0;
  balance: number = 0;
  categorySummary: CategorySummary[] = [];
  recentTransactions: RecentTransaction[] = [];
  financialGoals: FinancialGoal[] = [];
  alerts: Alert[] = [];
  budgets: Budget[] = [];
  categoryTrends: CategoryTrend[] = [];
  monthlyHistory: MonthlyHistory[] = [];
  chart: Chart | undefined;

  startMonth: string = '';
  endMonth: string = '';
  availableMonths: string[] = [];

  private monthMap: { [key: string]: number } = {
    'janeiro': 0,
    'fevereiro': 1,
    'março': 2,
    'abril': 3,
    'maio': 4,
    'junho': 5,
    'julho': 6,
    'agosto': 7,
    'setembro': 8,
    'outubro': 9,
    'novembro': 10,
    'dezembro': 11
  };

  constructor(
    @Inject(PLATFORM_ID) private platformId: Object,
    private authService: AuthService,
    private dashboardService: DashboardService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private cdr: ChangeDetectorRef
  ) {
    this.updateUserName();
  }

  ngOnInit() {
    this.generateMonthOptions();
    if (this.availableMonths.length > 0) {
      this.startMonth = this.availableMonths[this.availableMonths.length - 1];
      this.endMonth = this.availableMonths[this.availableMonths.length - 1];
    }

    this.authService.getCurrentUser().subscribe(user => {
      this.userName = user && user.fullName ? user.fullName : 'Usuário Anônimo';
      this.cdr.detectChanges();
    });
  }

  ngAfterViewInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.loadDashboardData();
    }
  }

  private updateUserName(): void {
    const user = this.authService.getCurrentUserValue();
    this.userName = user && user.fullName ? user.fullName : 'Usuário Anônimo';
  }

  generateMonthOptions(): void {
    const now = new Date();
    this.availableMonths = [];
    for (let i = 11; i >= 0; i--) {
      const date = new Date(now.getFullYear(), now.getMonth() - i, 1);
      const monthStr = date.toLocaleString('pt-BR', { month: 'long', year: 'numeric' });
      this.availableMonths.push(monthStr);
    }
  }

  getDateFromMonth(monthStr: string): Date {
    if (!monthStr || monthStr.trim() === '') {
      return new Date();
    }

    const parts = monthStr.split(' ');
    const monthName = parts[0].toLowerCase();
    const year = parts[parts.length - 1];

    const monthIndex = this.monthMap[monthName];
    if (monthIndex === undefined) {
      return new Date();
    }

    const date = new Date(Number(year), monthIndex, 1);
    if (isNaN(date.getTime())) {
      return new Date();
    }
    return date;
  }

  loadDashboardData(): void {
    this.spinner.show();

    let startDate = this.getDateFromMonth(this.startMonth);
    let endDate = new Date(this.getDateFromMonth(this.endMonth));
    endDate.setMonth(endDate.getMonth() + 1);
    endDate.setDate(0);

    if (isNaN(startDate.getTime()) || isNaN(endDate.getTime())) {
      this.toastr.error('Datas inválidas selecionadas. Usando datas padrão.', 'Erro');
      const now = new Date();
      startDate = new Date(now.getFullYear(), now.getMonth(), 1);
      endDate = new Date(now.getFullYear(), now.getMonth() + 1, 0);
    }

    this.dashboardService.getAllData(startDate, endDate).subscribe({
      next: (data) => {
        const dashboard = data.dashboard;
        this.revenue = data.currentBalance.totalRevenue || dashboard.totalIncome || 0;
        this.expense = data.currentBalance.totalExpense || dashboard.totalExpenses || 0;
        this.balance = this.revenue - this.expense;
        this.categorySummary = [...(dashboard.categorySummary || []), ...(data.categorySummaryExpense || []), ...(data.categorySummaryRevenue || [])];
        this.recentTransactions = data.recentTransactions || [];
        this.financialGoals = (data.financialGoals || []).map((goal: { deadline: string | number | Date }) => ({
          ...goal,
          deadline: new Date(goal.deadline)
        }));
        this.alerts = dashboard.alerts || [];
        this.budgets = dashboard.budgets || [];
        this.categoryTrends = dashboard.categoryTrends || [];
        this.monthlyHistory = dashboard.monthlyHistory || [];
        this.updateUserName();
        this.updateChart(this.monthlyHistory);
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.toastr.error(err.error?.Error || 'Erro ao carregar os dados.', 'Erro');
        this.loadFallbackData(startDate, endDate);
      },
      complete: () => this.spinner.hide()
    });
  }

  private loadFallbackData(startDate: Date, endDate: Date): void {
    this.dashboardService.getCurrentBalance().subscribe({
      next: (data) => {
        this.revenue = data.totalRevenue;
        this.expense = data.totalExpense;
        this.balance = data.currentBalance;
      },
      error: (err) => this.toastr.error(err.error?.Error || 'Erro ao carregar o saldo.', 'Erro')
    });

    this.dashboardService.getCategorySummary(startDate, endDate, 'Expense').subscribe({
      next: (summary) => {
        this.categorySummary = summary;
        this.updateChart();
      },
      error: (err) => this.toastr.error(err.error?.Error || 'Erro ao carregar o resumo de despesas.', 'Erro')
    });

    this.dashboardService.getCategorySummary(startDate, endDate, 'Revenue').subscribe({
      next: (summary) => this.updateChart(),
      error: (err) => this.toastr.error(err.error?.Error || 'Erro ao carregar o resumo de receitas.', 'Erro')
    });

    this.dashboardService.getRecentTransactions(5).subscribe({
      next: (transactions) => this.recentTransactions = transactions,
      error: (err) => this.toastr.error(err.error?.Error || 'Erro ao carregar as transações recentes.', 'Erro')
    });

    this.dashboardService.getFinancialGoals().subscribe({
      next: (goals) => this.financialGoals = goals.map(goal => ({
        ...goal,
        deadline: new Date(goal.deadline)
      })),
      error: (err) => this.toastr.error(err.error?.Error || 'Erro ao carregar as metas financeiras.', 'Erro'),
      complete: () => this.spinner.hide()
    });
  }

  onMonthChange(): void {
    this.loadDashboardData();
  }

  updateChart(monthlyHistory?: MonthlyHistory[], revenueSummary?: CategorySummary[], type?: string) {
    const ctx = document.getElementById('monthlyChart') as HTMLCanvasElement;
    if (!ctx) return;

    if (this.chart) this.chart.destroy();

    const months = monthlyHistory ? monthlyHistory.map(h => h.month).slice(0, 3) : this.availableMonths.slice(0, 3);
    const revenueData = monthlyHistory ? monthlyHistory.map(h => h.income).slice(0, 3) : [0, 0, 0];
    const expenseData = monthlyHistory ? monthlyHistory.map(h => h.expenses).slice(0, 3) : [0, 0, 0];

    this.chart = new Chart(ctx, {
      type: 'bar',
      data: {
        labels: months,
        datasets: [
          {
            label: 'Receitas',
            data: revenueData,
            backgroundColor: '#34D399',
            borderColor: '#166534',
            borderWidth: 1
          },
          {
            label: 'Despesas',
            data: expenseData,
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

  ngOnDestroy() {
    if (this.chart) this.chart.destroy();
  }
}