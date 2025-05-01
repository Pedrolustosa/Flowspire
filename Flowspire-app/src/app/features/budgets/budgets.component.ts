import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BudgetService } from '../../core/services/budget.service';
import { CategoryService } from '../../core/services/category.service';
import { TransactionService } from '../../core/services/transaction.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Budget } from '../../shared/models/budget';
import { CategoryDTO, TransactionDTO } from '../../shared/models/Transaction';

@Component({
  selector: 'app-budgets',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './budgets.component.html',
  styleUrls: ['./budgets.component.css']
})
export class BudgetsComponent implements OnInit {
  form!: FormGroup;
  budgets: Budget[] = [];
  categories: CategoryDTO[] = [];
  transactions: TransactionDTO[] = [];

  constructor(
    private fb: FormBuilder,
    private budgetService: BudgetService,
    private categoryService: CategoryService,
    private transactionService: TransactionService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadBudgets();
    this.loadCategories();
    this.loadTransactions();
  }

  initializeForm(): void {
    this.form = this.fb.group({
      amount: [0, [Validators.required, Validators.min(1)]],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      categoryId: [null, Validators.required],
      categoryName: [''],
      userId: ['']
    });
  }

  loadBudgets(): void {
    this.spinner.show();
    this.budgetService.getUserBudgets().subscribe({
      next: data => {
        this.budgets = data;
        this.spinner.hide();
      },
      error: () => {
        this.toastr.error('Erro ao carregar orçamentos.');
        this.spinner.hide();
      }
    });
  }

  loadCategories(): void {
    this.categoryService.getUserCategories().subscribe({
      next: data => this.categories = data,
      error: () => this.toastr.error('Erro ao carregar categorias.')
    });
  }

  loadTransactions(): void {
    this.spinner.show();
    this.transactionService.getUserTransactions().subscribe({
      next: (data: TransactionDTO[]) => {
        this.transactions = data;
        this.spinner.hide();
      },
      error: () => {
        this.toastr.error('Erro ao carregar transações.');
        this.spinner.hide();
      }
    });
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.spinner.show();
    const budget: Budget = this.form.value;
    const selectedCategory = this.categories.find(cat => cat.id === budget.categoryId);
    budget.categoryName = selectedCategory ? selectedCategory.name : '';

    const user = localStorage.getItem('user');
    if (user) {
      const parsed = JSON.parse(user);
      budget.userId = parsed.id;
    }

    this.budgetService.createBudget(budget).subscribe({
      next: () => {
        this.toastr.success('Orçamento criado com sucesso!');
        this.form.reset();
        this.loadBudgets();
        this.spinner.hide();
      },
      error: () => {
        this.toastr.error('Erro ao criar orçamento.');
        this.spinner.hide();
      }
    });
  }

  deleteBudget(id: number): void {
    if (!confirm('Deseja realmente excluir este orçamento?')) return;

    this.spinner.show();
    this.budgetService.deleteBudget(id).subscribe({
      next: () => {
        this.toastr.success('Orçamento excluído.');
        this.loadBudgets();
        this.spinner.hide();
      },
      error: () => {
        this.toastr.error('Erro ao excluir orçamento.');
        this.spinner.hide();
      }
    });
  }

  getUsagePercentage(budget: Budget): number {
    const now = new Date();
    const start = new Date(budget.startDate);
    const end = new Date(budget.endDate);

    if (now < start || now > end) return 0;

    const totalSpent = this.transactions.reduce((sum: number, t: TransactionDTO) => {
      const date = new Date(t.date);
      return (
        t.transactionType === 'Expense' &&
        t.categoryId === budget.categoryId &&
        date >= start &&
        date <= end
      ) ? sum + t.amount : sum;
    }, 0);

    return budget.amount > 0 ? (totalSpent / budget.amount) * 100 : 0;
  }
}
