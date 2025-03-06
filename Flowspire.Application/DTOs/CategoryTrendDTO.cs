namespace Flowspire.Application.DTOs;

public class CategoryTrendDTO
{
    public string CategoryName { get; set; }
    public decimal CurrentPeriodExpenses { get; set; }
    public decimal PreviousPeriodExpenses { get; set; }
    public decimal TrendPercentage { get; set; }
}
