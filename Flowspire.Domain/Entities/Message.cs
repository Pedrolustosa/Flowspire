namespace Flowspire.Domain.Entities;
public class Message
{
    public int Id { get; private set; }
    public string SenderId { get; private set; }
    public User Sender { get; private set; }
    public string ReceiverId { get; private set; }
    public User Receiver { get; private set; }
    public string Content { get; private set; }
    public DateTime SentAt { get; private set; }
    public bool IsRead { get; private set; }

    private Message() { }

    public static Message Create(string senderId, string receiverId, string content)
    {
        if (string.IsNullOrWhiteSpace(senderId)) throw new ArgumentException("SenderId é obrigatório.");
        if (string.IsNullOrWhiteSpace(receiverId)) throw new ArgumentException("ReceiverId é obrigatório.");
        if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Conteúdo da mensagem é obrigatório.");

        return new Message
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Content = content.Trim(),
            SentAt = DateTime.UtcNow,
            IsRead = false
        };
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}