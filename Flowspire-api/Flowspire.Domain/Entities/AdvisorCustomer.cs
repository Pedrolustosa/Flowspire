﻿using Flowspire.Domain.Entities;

public class AdvisorCustomer
{
    public int Id { get; private set; }
    public string AdvisorId { get; private set; }
    public User Advisor { get; private set; }
    public string CustomerId { get; private set; }
    public User Customer { get; private set; }
    public DateTime AssignedAt { get; private set; }

    private AdvisorCustomer() { }

    public static AdvisorCustomer Create(string advisorId, string customerId)
    {
        if (string.IsNullOrWhiteSpace(advisorId)) throw new ArgumentException("AdvisorId is required.");
        if (string.IsNullOrWhiteSpace(customerId)) throw new ArgumentException("CustomerId is required.");

        return new AdvisorCustomer
        {
            AdvisorId = advisorId,
            CustomerId = customerId,
            AssignedAt = DateTime.UtcNow
        };
    }
}