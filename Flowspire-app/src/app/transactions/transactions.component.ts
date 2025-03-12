import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TransactionService } from '../services/transaction.service';
import { CategoryService } from '../services/category.service';
import { TransactionDTO, FinancialReport, CategoryDTO } from '../models/Transaction';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

@Component({
  selector: 'app-transaction',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionComponent implements OnInit {
  transactions: TransactionDTO[] = [];
  financialReport: FinancialReport = {
    totalIncome: 0,
    totalExpenses: 0,
    expensesByCategory: {},
    transactions: []
  };
  categories: CategoryDTO[] = [];
  newTransaction: TransactionDTO = {
    description: '',
    amount: 0,
    date: new Date(),
    categoryId: 0,
    userId: '',
    category: {
      id: 0,
      name: '',
      userId: ''
    }
  };
  startDate: Date | null = null;
  endDate: Date | null = null;
  pdfGenerated: boolean = false;
  isGenerateButtonDisabled: boolean = true;

  constructor(
    private transactionService: TransactionService,
    private categoryService: CategoryService,
    private authService: AuthService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) {
    const user = this.authService.getCurrentUserValue();
    if (user && user.id) {
      this.newTransaction.userId = user.id;
    } else {
      this.authService.getCurrentUser().subscribe({
        next: (user) => {
          this.newTransaction.userId = user.id;
          this.loadTransactions();
        },
        error: (err) => {
          this.toastr.error('Erro ao buscar usuário atual.', 'Erro');
        }
      });
    }
  }

  ngOnInit(): void {
    this.loadCategories();
    this.loadTransactions();
    this.validateDates();
  }

  loadCategories(): void {
    this.spinner.show();
    this.categoryService.getCategories().subscribe({
      next: (data) => {
        this.categories = data;
        if (data.length > 0) {
          this.newTransaction.categoryId = data[0].id;
          this.newTransaction.category = data[0];
        }
        this.spinner.hide();
      },
      error: (err) => {
        this.toastr.error(err.error?.Error || 'Erro ao carregar categorias.', 'Erro');
        this.spinner.hide();
      }
    });
  }

  loadTransactions(): void {
    this.spinner.show();
    const userId = this.newTransaction.userId;
    if (userId) {
      this.transactionService.getTransactionsByUserId(userId).subscribe({
        next: (data) => {
          this.transactions = data;
          this.spinner.hide();
        },
        error: (err) => {
          this.toastr.error(err.error?.Error || 'Erro ao carregar transações.', 'Erro');
          this.spinner.hide();
        }
      });
    } else {
      this.spinner.hide();
      this.toastr.error('Usuário não autenticado. Faça login novamente.', 'Erro');
    }
  }

  loadFinancialReport(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.spinner.show();
      const validStartDate = this.startDate && this.startDate instanceof Date && !isNaN(this.startDate.getTime())
        ? this.startDate
        : new Date(new Date().getFullYear(), new Date().getMonth(), 1);
      const validEndDate = this.endDate && this.endDate instanceof Date && !isNaN(this.endDate.getTime())
        ? this.endDate
        : new Date(new Date().getFullYear(), new Date().getMonth() + 1, 0);

      if (!validStartDate || !validEndDate) {
        this.toastr.error('Datas inválidas. Por favor, selecione datas válidas.', 'Erro');
        this.spinner.hide();
        reject('Invalid dates');
        return;
      }

      this.transactionService.getFinancialReport(validStartDate, validEndDate).subscribe({
        next: (data) => {
          console.log('Dados do Relatório:', data);
          this.financialReport = {
            totalIncome: data.totalIncome || 0,
            totalExpenses: data.totalExpenses || 0,
            expensesByCategory: data.expensesByCategory || {},
            transactions: data.transactions || [],
            netBalance: (data.totalIncome || 0) - (data.totalExpenses || 0)
          };
          this.spinner.hide();
          resolve();
        },
        error: (err) => {
          this.toastr.error(err.error?.Error || 'Erro ao carregar relatório financeiro.', 'Erro');
          this.spinner.hide();
          reject(err);
        }
      });
    });
  }

  addTransaction(): void {
    if (this.newTransaction.categoryId <= 0) {
      this.toastr.error('Selecione uma categoria válida.', 'Erro');
      return;
    }

    this.spinner.show();
    this.transactionService.addTransaction(this.newTransaction).subscribe({
      next: (response) => {
        this.toastr.success('Transação adicionada com sucesso!', 'Sucesso');
        const selectedCategory = this.categories.find(c => c.id === this.newTransaction.categoryId);
        this.newTransaction = {
          description: '',
          amount: 0,
          date: new Date(),
          categoryId: this.categories.length > 0 ? this.categories[0].id : 0,
          userId: this.newTransaction.userId,
          category: selectedCategory || {
            id: 0,
            name: '',
            userId: ''
          }
        };
        this.loadTransactions();
        if (this.startDate && this.endDate && this.startDate instanceof Date && this.endDate instanceof Date) {
          this.loadFinancialReport().catch(() => {});
        }
        this.spinner.hide();
      },
      error: (err) => {
        this.toastr.error(err.error?.Error || 'Erro ao adicionar transação.', 'Erro');
        this.spinner.hide();
      }
    });
  }

  onDateChange(): void {
    this.validateDates();
    if (!this.isGenerateButtonDisabled) {
      this.loadFinancialReport().catch(() => {});
    }
  }

  async generateReport(): Promise<void> {
    if (this.isGenerateButtonDisabled) {
      this.toastr.error('Por favor, selecione datas válidas antes de gerar o relatório.', 'Erro');
      return;
    }
    try {
      await this.loadFinancialReport();
      this.createPDFReport();
      this.pdfGenerated = true;
      setTimeout(() => this.pdfGenerated = false, 3000);
    } catch (error) {
      this.toastr.error('Erro ao gerar o relatório.', 'Erro');
    }
  }

  resetDates(): void {
    this.startDate = null;
    this.endDate = null;
    this.financialReport = {
      totalIncome: 0,
      totalExpenses: 0,
      expensesByCategory: {},
      transactions: []
    };
    this.validateDates();
  }

  private validateDates(): void {
    const isStartDateValid = this.startDate && this.startDate instanceof Date && !isNaN(this.startDate.getTime());
    const isEndDateValid = this.endDate && this.endDate instanceof Date && !isNaN(this.endDate.getTime());
    const isStartBeforeEnd = isStartDateValid && isEndDateValid;

    this.isGenerateButtonDisabled = !(isStartDateValid && isEndDateValid && isStartBeforeEnd);
  }

  private createPDFReport(): void {
    const doc = new jsPDF();
    const user = this.authService.getCurrentUserValue();
    const userName = user?.fullName || 'Usuário Não Identificado';
    const startDateStr = this.startDate instanceof Date && !isNaN(this.startDate.getTime())
      ? this.startDate.toLocaleDateString('pt-BR')
      : 'Data Não Selecionada';
    const endDateStr = this.endDate instanceof Date && !isNaN(this.endDate.getTime())
      ? this.endDate.toLocaleDateString('pt-BR')
      : 'Data Não Selecionada';
    const dateRange = `${startDateStr} - ${endDateStr}`;
    const currentDate = new Date().toLocaleDateString('pt-BR');

    doc.setFontSize(20);
    doc.setFont('helvetica', 'bold');
    doc.text('Relatório Financeiro', 105, 20, { align: 'center' });

    doc.setFontSize(10);
    doc.setFont('helvetica', 'normal');
    doc.text(`Usuário: ${userName}`, 15, 35);
    doc.text(`Período: ${dateRange}`, 15, 40);
    doc.text(`Data de Geração: ${currentDate}`, 15, 45);

    doc.setDrawColor(0, 0, 0);
    doc.line(15, 50, 195, 50);

    const financialData = [
      ['Receita Total', `R$ ${this.financialReport.totalIncome.toFixed(2)}`],
      ['Despesa Total', `R$ ${this.financialReport.totalExpenses.toFixed(2)}`],
      ['Saldo Líquido', `R$ ${(this.financialReport.totalIncome - this.financialReport.totalExpenses).toFixed(2)}`]
    ];

    autoTable(doc, {
      startY: 55,
      head: [['Descrição', 'Valor']],
      body: financialData,
      theme: 'grid',
      headStyles: { fillColor: [41, 128, 185], textColor: [255, 255, 255], fontSize: 12 },
      bodyStyles: { fontSize: 10, textColor: [50, 50, 50] },
      columnStyles: {
        0: { cellWidth: 80 },
        1: { cellWidth: 80, halign: 'right' }
      },
      margin: { left: 15, right: 15 }
    });

    let finalY = (doc as any).lastAutoTable.finalY || 55;
    if (Object.keys(this.financialReport.expensesByCategory).length > 0) {
      const categoryData = Object.entries(this.financialReport.expensesByCategory).map(([category, amount]) => [
        category,
        `R$ ${amount.toFixed(2)}`
      ]);

      autoTable(doc, {
        startY: finalY + 10,
        head: [['Categoria', 'Valor']],
        body: categoryData,
        theme: 'grid',
        headStyles: { fillColor: [41, 128, 185], textColor: [255, 255, 255], fontSize: 12 },
        bodyStyles: { fontSize: 10, textColor: [50, 50, 50] },
        columnStyles: {
          0: { cellWidth: 80 },
          1: { cellWidth: 80, halign: 'right' }
        },
        margin: { left: 15, right: 15 }
      });
      finalY = (doc as any).lastAutoTable.finalY || finalY + 10;
    } else {
      doc.setFontSize(10);
      doc.text('Nenhuma despesa categorizada no período.', 15, finalY + 10);
      finalY += 15;
    }

    doc.setFontSize(8);
    doc.text(`Gerado por Flowspire - ${currentDate}`, 105, 290, { align: 'center' });

    const safeUserName = userName.replace(/[^a-zA-Z0-9]/g, '_');
    doc.save(`relatorio_financeiro_${safeUserName}_${new Date().toISOString().split('T')[0]}.pdf`);
  }
}