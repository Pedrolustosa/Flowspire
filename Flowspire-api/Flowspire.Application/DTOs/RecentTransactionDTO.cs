namespace Flowspire.Application.DTOs;

public class RecentTransactionDTO
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } // "Expense" ou "Revenue"
    public string Category { get; set; }
    public DateTime Date { get; set; }
}