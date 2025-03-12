export interface TransactionDTO {
    id?: number;
    description: string;
    amount: number;
    date: Date;
    categoryId: number;
    category?: CategoryDTO;
    userId: string;
  }
  
  export interface CategoryDTO {
    id: number;
    name: string;
    userId: string;
  }
  
  export interface FinancialReport {
    totalIncome: number;
    totalExpenses: number;
    expensesByCategory: { [key: string]: number };
    netBalance?: number;
    transactions?: TransactionDTO[];
  }