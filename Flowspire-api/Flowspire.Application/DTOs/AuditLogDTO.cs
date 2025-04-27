namespace Flowspire.Application.DTOs
{
    public class AuditLogDTO
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? IpAddress { get; set; }
        public string Method { get; set; } = null!;
        public string Path { get; set; } = null!;
        public int StatusCode { get; set; }
        public long ExecutionTimeMs { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
