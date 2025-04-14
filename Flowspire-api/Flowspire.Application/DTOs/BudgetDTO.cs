using System;

namespace Flowspire.Application.DTOs;

public class BudgetDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string UserId { get; set; }
}
