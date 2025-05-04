namespace Flowspire.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; private set; }
    public string? UserId { get; private set; }
    public string? IpAddress { get; private set; }
    public string Method { get; private set; } = null!;
    public string Path { get; private set; } = null!;
    public int StatusCode { get; private set; }
    public long ExecutionTimeMs { get; private set; }
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

    private AuditLog() { }

    public static AuditLog Create(string? userId, string method, string path, int statusCode, long execMs, string? ip)
    {
        return new AuditLog
        {
            UserId = userId,
            Method = method,
            Path = path,
            StatusCode = statusCode,
            ExecutionTimeMs = execMs,
            IpAddress = ip,
            Timestamp = DateTime.UtcNow
        };
    }
}
