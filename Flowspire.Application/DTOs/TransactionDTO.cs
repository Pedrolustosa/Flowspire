namespace Flowspire.Application.DTOs;
public class TransactionDTO
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; }
    public string UserId { get; set; }
}