export interface TransactionDTO {
  id?: number;
  description: string;
  amount: number;
  originalAmount?: number;
  fee?: number;
  discount?: number;
  date: Date;
  transactionType: TransactionType;
  categoryId: number;
  categoryName?: string;
  userId: string;
  createdAt?: Date;
  updatedAt?: Date;
  notes?: string;
  paymentMethod?: string;
  isRecurring?: boolean;
  nextOccurrence?: Date;
  externalTransactionId?: string;
}

export enum TransactionType {
  Income = 'Income',
  Expense = 'Expense',
  Savings = 'Savings',
  Investment = 'Investment'
}
  
export interface CategoryDTO {
  id: number;
  name: string;
  userId: string;
}
  
export interface FinancialReport {
  totalIncome: number;
  totalExpenses: number;
  expensesByCategory: { [category: string]: number };
  transactions: TransactionDTO[];
  netBalance: number;
}