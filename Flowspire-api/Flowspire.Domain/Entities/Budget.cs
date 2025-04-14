using Flowspire.Domain.Entities;

public class Budget
{
    public int Id { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string UserId { get; private set; }
    public User User { get; private set; }

    private Budget() { }

    public static Budget Create(decimal amount, DateTime startDate, DateTime endDate, int categoryId, string userId)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.", nameof(amount));
        if (startDate >= endDate)
            throw new ArgumentException("Start date must be before end date.", nameof(startDate));
        if (categoryId <= 0)
            throw new ArgumentException("Invalid category.", nameof(categoryId));
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId is required.", nameof(userId));

        return new Budget
        {
            Amount = amount,
            StartDate = startDate,
            EndDate = endDate,
            CategoryId = categoryId,
            UserId = userId
        };
    }

    public void Update(decimal amount, DateTime startDate, DateTime endDate, int categoryId)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.", nameof(amount));
        if (startDate >= endDate)
            throw new ArgumentException("Start date must be before end date.", nameof(startDate));
        if (categoryId <= 0)
            throw new ArgumentException("Invalid category.", nameof(categoryId));

        Amount = amount;
        StartDate = startDate;
        EndDate = endDate;
        CategoryId = categoryId;
    }
}