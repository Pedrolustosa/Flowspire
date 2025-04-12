using System;

namespace Flowspire.Application.DTOs
{
    public class FinancialTransactionDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public decimal OriginalAmount { get; set; }

        public decimal? Fee { get; set; }

        public decimal? Discount { get; set; }

        public DateTime Date { get; set; }
        public string TransactionType { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Notes { get; set; }

        public string PaymentMethod { get; set; }

        public bool IsRecurring { get; set; }

        public DateTime? NextOccurrence { get; set; }
    }
}