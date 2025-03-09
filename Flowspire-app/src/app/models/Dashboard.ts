export interface Balance {
  totalRevenue: number;
  totalExpense: number;
  currentBalance: number;
}

export interface Budget {
  categoryName: string;
  budgetAmount: number;
  spentAmount: number;
  percentageUsed: number;
}

export interface Alert {
  message: string;
}

export interface MonthlyHistory {
  month: string;
  income: number;
  expenses: number;
}

export interface CategoryTrend {
  categoryName: string;
  currentPeriodExpenses: number;
  previousPeriodExpenses: number;
  trendPercentage: number;
}

export interface CategorySummary {
  categoryName: string;
  income: number;
  expenses: number;
}

export interface RecentTransaction {
  description: string;
  amount: number;
  type: string;
  category: string;
  date: Date;
}

export interface FinancialGoal {
  name: string;
  targetAmount: number;
  currentAmount: number;
  deadline: Date;
  progressPercentage: number;
}

export interface DashboardOverview {
  totalIncome: number;
  totalExpenses: number;
  budgets: Budget[];
  alerts: Alert[];
  monthlyHistory: MonthlyHistory[];
  categoryTrends: CategoryTrend[];
  categorySummary: CategorySummary[];
}