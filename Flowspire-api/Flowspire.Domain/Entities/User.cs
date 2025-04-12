using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public enum Gender
{
    Male,
    Female,
    NotSpecified
}

public class User : IdentityUser
{
    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    // Audit fields
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Personal Data
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public Gender Gender { get; private set; }

    // Address Data
    public string? AddressLine1 { get; private set; }
    public string? AddressLine2 { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? Country { get; private set; }
    public string? PostalCode { get; private set; }

    private User() { }

    public static User Create(string email, string password, string firstName, string lastName, string phoneNumber,
                              DateTime? birthDate = null,
                              Gender gender = Gender.NotSpecified,
                              string? addressLine1 = null,
                              string? addressLine2 = null,
                              string? city = null,
                              string? state = null,
                              string? country = null,
                              string? postalCode = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password is required.");
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");
        ValidatePhoneNumber(phoneNumber);

        var now = DateTime.UtcNow;

        var user = new User
        {
            UserName = email,
            Email = email,
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            PhoneNumber = phoneNumber.Trim(),
            CreatedAt = now,
            UpdatedAt = now,
            Gender = gender,
            BirthDate = birthDate,
            AddressLine1 = addressLine1?.Trim(),
            AddressLine2 = addressLine2?.Trim(),
            City = city?.Trim(),
            State = state?.Trim(),
            Country = country?.Trim(),
            PostalCode = postalCode?.Trim()
        };

        return user;
    }

    public void UpdatePersonalInfo(string firstName, string lastName, DateTime? birthDate, Gender gender)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        BirthDate = birthDate;
        Gender = gender;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateAddress(string? addressLine1, string? addressLine2, string? city, string? state, string? country, string? postalCode)
    {
        AddressLine1 = addressLine1?.Trim();
        AddressLine2 = addressLine2?.Trim();
        City = city?.Trim();
        State = state?.Trim();
        Country = country?.Trim();
        PostalCode = postalCode?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePhoneNumber(string phoneNumber)
    {
        ValidatePhoneNumber(phoneNumber);
        PhoneNumber = phoneNumber.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddTransaction(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));

        Transactions.Add(transaction);
    }

    // Helper method for validating the phone number using E.164 format
    private static void ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required.");

        var phoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$");
        if (!phoneRegex.IsMatch(phoneNumber))
            throw new ArgumentException("Phone number format is invalid.");
    }
}
