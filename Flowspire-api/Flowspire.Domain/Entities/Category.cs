using Flowspire.Domain.Entities;

public class Category
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string UserId { get; private set; }
    public User User { get; private set; }
    public ICollection<FinancialTransaction> FinancialTransactions { get; private set; } = new List<FinancialTransaction>();

    private Category() { }

    public static Category Create(string name, string userId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome da categoria é obrigatório.");
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("UserId é obrigatório.");

        return new Category
        {
            Name = name.Trim(),
            UserId = userId
        };
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome da categoria é obrigatório.");
        Name = name.Trim();
    }

    public void AddTransaction(FinancialTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction.CategoryId != Id) throw new ArgumentException("Transação pertence a outra categoria.");
        FinancialTransactions.Add(transaction);
    }
}