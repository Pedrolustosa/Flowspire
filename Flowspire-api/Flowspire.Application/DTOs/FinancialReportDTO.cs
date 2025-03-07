namespace Flowspire.Application.DTOs;
public class FinancialReportDTO
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public Dictionary<string, decimal> ExpensesByCategory { get; set; }
}