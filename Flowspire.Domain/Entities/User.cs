using Microsoft.AspNetCore.Identity;
using Flowspire.Domain.Enums;

namespace Flowspire.Domain.Entities;
public class User : IdentityUser
{
    public string FullName { get; private set; }
    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    private User() { }

    public static User Create(string email, string fullName, string password)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email é obrigatório.");
        if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Nome completo é obrigatório.");
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Senha é obrigatória.");

        return new User
        {
            UserName = email,
            Email = email,
            FullName = fullName.Trim()
        };
    }

    public void UpdateFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Nome completo é obrigatório.");
        FullName = fullName.Trim();
    }

    public void AddTransaction(Transaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        Transactions.Add(transaction);
    }
}