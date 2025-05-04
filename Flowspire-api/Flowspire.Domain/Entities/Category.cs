namespace Flowspire.Domain.Entities;

public class Category
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string UserId { get; private set; }
    public User User { get; private set; }
    public bool IsDefault { get; private set; }
    public int SortOrder { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public ICollection<FinancialTransaction> FinancialTransactions { get; private set; } = new List<FinancialTransaction>();

    private Category() { }

    public static Category Create(string name, string userId, string? description = null, bool isDefault = false, int sortOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("UserId is required.");

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
        Name = name.Trim(); Touch();
    }

    public void UpdateDescription(string desc)
    {
        Description = desc?.Trim(); Touch();
    }

    public void UpdateSortOrder(int sort) { SortOrder = sort; Touch(); }
    public void ToggleDefault() { IsDefault = !IsDefault; Touch(); }
    private void Touch() => UpdatedAt = DateTime.UtcNow;
}