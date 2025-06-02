namespace Flowspire.Application.DTOs;

public class DashboardDTO
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public List<string> Alerts { get; set; }
    public List<MonthlySummaryDTO> MonthlyHistory { get; set; }
    public List<CategoryTrendDTO> CategoryTrends { get; set; }
    public List<CategorySummaryDTO> CategorySummary { get; set; }
}
