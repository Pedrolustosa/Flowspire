using Flowspire.Domain.Enums;
using Flowspire.Domain.ValueObjects;

namespace Flowspire.Domain.Entities
{
    public class FinancialTransaction
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public decimal Fee { get; private set; }
        public decimal Discount { get; private set; }
        public DateTime Date { get; private set; }
        public TransactionType Type { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public string UserId { get; private set; }
        public User User { get; private set; }
        public string? Notes { get; private set; }
        public string? PaymentMethod { get; private set; }
        public bool IsRecurring { get; private set; }
        public DateTime? NextOccurrence { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private FinancialTransaction() { }

        public static FinancialTransaction Create(
            string description,
            Money amountVo,
            DateTime date,
            TransactionType type,
            int categoryId,
            string userId,
            Money? feeVo = null,
            Money? discountVo = null,
            string? notes = null,
            string? paymentMethod = null,
            bool isRecurring = false,
            DateTime? nextOccurrence = null)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));
            if (categoryId <= 0)
                throw new ArgumentException("Invalid category.", nameof(categoryId));
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId is required.", nameof(userId));

            return new FinancialTransaction
            {
                Description    = description.Trim(),
                Amount         = amountVo.Value,
                Fee            = feeVo?.Value ?? 0m,
                Discount       = discountVo?.Value ?? 0m,
                Date           = date,
                Type           = type,
                CategoryId     = categoryId,
                UserId         = userId,
                Notes          = notes,
                PaymentMethod  = paymentMethod,
                IsRecurring    = isRecurring,
                NextOccurrence = nextOccurrence,
                CreatedAt      = DateTime.UtcNow,
                UpdatedAt      = DateTime.UtcNow
            };
        }

        public void Update(
            string description,
            Money amountVo,
            TransactionType type,
            int categoryId,
            Money? feeVo = null,
            Money? discountVo = null,
            string? notes = null,
            string? paymentMethod = null)
        {
            Description    = description.Trim();
            Amount         = amountVo.Value;
            Fee            = feeVo?.Value ?? 0m;
            Discount       = discountVo?.Value ?? 0m;
            Type           = type;
            CategoryId     = categoryId;
            Notes          = notes;
            PaymentMethod  = paymentMethod;
            UpdatedAt      = DateTime.UtcNow;
        }
    }
}
