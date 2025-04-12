using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;

public class FinancialTransaction
{
    public int Id { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public decimal OriginalAmount { get; private set; }
    public decimal? Fee { get; private set; }
    public decimal? Discount { get; private set; }
    public DateTime Date { get; private set; }
    public TransactionType Type { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }
    public string UserId { get; private set; }
    public User User { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public string Notes { get; private set; }
    public string PaymentMethod { get; private set; }

    public bool IsRecurring { get; private set; }
    public DateTime? NextOccurrence { get; private set; }

    private FinancialTransaction() { }

    public static FinancialTransaction Create(
        string description,
        decimal amount,
        DateTime date,
        TransactionType type,
        int categoryId,
        string userId,
        decimal? fee = null,
        decimal? discount = null,
        string? notes = null,
        string? paymentMethod = null,
        bool isRecurring = false,
        DateTime? nextOccurrence = null,
        string? externalTransactionId = null)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.");
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.");
        if (categoryId <= 0)
            throw new ArgumentException("Invalid category.");
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId is required.");

        return new FinancialTransaction
        {
            Description = description.Trim(),
            Amount = amount,
            OriginalAmount = amount,
            Date = date,
            Type = type,
            CategoryId = categoryId,
            UserId = userId,
            Fee = fee,
            Discount = discount,
            Notes = notes,
            PaymentMethod = paymentMethod,
            IsRecurring = isRecurring,
            NextOccurrence = nextOccurrence,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public void Update(
        string description,
        decimal amount,
        TransactionType type,
        int categoryId,
        decimal? fee = null,
        decimal? discount = null,
        string? notes = null,
        string? paymentMethod = null)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.");
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.");
        if (categoryId <= 0)
            throw new ArgumentException("Invalid category.");

        Description = description.Trim();
        Amount = amount;
        Type = type;
        CategoryId = categoryId;
        Fee = fee;
        Discount = discount;
        Notes = notes;
        PaymentMethod = paymentMethod;
        UpdatedAt = DateTime.Now;
    }
}
