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

    public static Budget Create(int categoryId, decimal amount, DateTime startDate, DateTime endDate, string userId)
    {
        if (categoryId <= 0) throw new ArgumentException("Categoria inválida.");
        if (amount <= 0) throw new ArgumentException("O valor do orçamento deve ser positivo.");
        if (startDate >= endDate) throw new ArgumentException("Data inicial deve ser anterior à final.");
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("UserId é obrigatório.");

        return new Budget
        {
            CategoryId = categoryId,
            Amount = amount,
            StartDate = startDate,
            EndDate = endDate,
            UserId = userId
        };
    }

    public void Update(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("O valor do orçamento deve ser positivo.");
        Amount = amount;
    }
}