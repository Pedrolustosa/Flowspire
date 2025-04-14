namespace Flowspire.Domain.Entities;

public class Category
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string UserId { get; private set; }
    public User User { get; private set; }
    public ICollection<FinancialTransaction> FinancialTransactions { get; private set; } = new List<FinancialTransaction>();

    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDefault { get; private set; }
    public int SortOrder { get; private set; }

    private Category() { }

    public static Category Create(string name, string userId, string description = null, bool isDefault = false, int sortOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required.", nameof(name));
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId is required.", nameof(userId));

        return new Category
        {
            Name = name.Trim(),
            UserId = userId,
            Description = description?.Trim(),
            IsDefault = isDefault,
            SortOrder = sortOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required.", nameof(name));
        Name = name.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string description)
    {
        Description = description?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateSortOrder(int sortOrder)
    {
        SortOrder = sortOrder;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ToggleDefault()
    {
        IsDefault = !IsDefault;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddTransaction(FinancialTransaction transaction)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction.CategoryId != Id)
            throw new ArgumentException("Transaction belongs to a different category.", nameof(transaction));
        FinancialTransactions.Add(transaction);
    }
}