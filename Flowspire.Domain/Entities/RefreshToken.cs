namespace Flowspire.Domain.Entities;

public class RefreshToken
{
    public int Id { get; private set; }
    public string Token { get; private set; }
    public string UserId { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime Expires { get; private set; }
    public bool IsRevoked { get; private set; }

    private RefreshToken() { }

    public static RefreshToken Create(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId é obrigatório.");

        return new RefreshToken
        {
            Token = Guid.NewGuid().ToString("N"),
            UserId = userId,
            Created = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };
    }

    public void Revoke()
    {
        if (IsRevoked)
            throw new InvalidOperationException("Token já foi revogado.");
        IsRevoked = true;
    }
}