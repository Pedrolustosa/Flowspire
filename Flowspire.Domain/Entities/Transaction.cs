namespace Flowspire.Domain.Entities;
public class Transaction
{
    public int Id { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string Category { get; private set; }
    public string UserId { get; private set; }
    public User User { get; private set; }

    private Transaction() { }

    public static Transaction Create(string description, decimal amount, DateTime date, string category, string userId)
    {
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Descrição é obrigatória.");
        if (amount <= 0) throw new ArgumentException("O valor deve ser positivo.");
        if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Categoria é obrigatória.");
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("UserId é obrigatório.");

        return new Transaction
        {
            Description = description.Trim(),
            Amount = amount,
            Date = date,
            Category = category.Trim(),
            UserId = userId
        };
    }

    public void Update(string description, decimal amount, string category)
    {
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Descrição é obrigatória.");
        if (amount <= 0) throw new ArgumentException("O valor deve ser positivo.");
        if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Categoria é obrigatória.");

        Description = description.Trim();
        Amount = amount;
        Category = category.Trim();
    }
}