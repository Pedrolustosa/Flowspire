namespace Flowspire.Application.DTOs;

public class FinancialGoalDTO
{
    public string Name { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public DateTime Deadline { get; set; }
    public double ProgressPercentage { get; set; }
}