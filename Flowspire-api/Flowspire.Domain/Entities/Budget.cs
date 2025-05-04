using Flowspire.Domain.Entities;
using Flowspire.Domain.ValueObjects;

public class Budget
{
    public int Id { get; private set; }
    public Money Amount { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }
    public string UserId { get; private set; }
    public User User { get; private set; }

    private Budget() { }

    public static Budget Create(Money amount, DateTime start, DateTime end, int categoryId, string userId)
    {
        if (start >= end) throw new ArgumentException("Start date must be before end date.");
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("UserId is required.");

        return new Budget
        {
            Amount = amount,
            StartDate = start,
            EndDate = end,
            CategoryId = categoryId,
            UserId = userId
        };
    }

    public void Update(Money amount, DateTime start, DateTime end, int categoryId)
    {
        if (start >= end) throw new ArgumentException("Start date must be before end date.");

        Amount = amount;
        StartDate = start;
        EndDate = end;
        CategoryId = categoryId;
    }
}