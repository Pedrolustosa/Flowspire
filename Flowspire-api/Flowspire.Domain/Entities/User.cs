using Flowspire.Domain.Enums;
using Flowspire.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Flowspire.Domain.Entities;

public class User : IdentityUser
{
    public FullName Name { get; private set; }
    public PhoneNumber Phone { get; private set; }
    public Address Address { get; private set; }

    public DateTime? BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private User() { }

    public static User Create(
        string email,
        string password,
        FullName name,
        PhoneNumber phone,
        DateTime? birthDate = null,
        Gender gender = Gender.NotSpecified,
        Address? address = null)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required.");
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password is required.");

        var now = DateTime.UtcNow;
        return new User
        {
            UserName  = email,
            Email     = email,
            Name      = name,
            Phone     = phone,
            BirthDate = birthDate,
            Gender    = gender,
            Address   = address ?? new Address(null, null, null, null, null, null),
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    public void UpdateName(FullName name) { Name = name; Touch(); }
    public void UpdatePhoneNumber(PhoneNumber p) { Phone = p; Touch(); }
    public void UpdateAddress(Address addr) { Address = addr; Touch(); }
    public void UpdateBirthDate(DateTime? d) { BirthDate = d; Touch(); }
    public void UpdateGender(Gender g) { Gender = g; Touch(); }
    private void Touch() => UpdatedAt = DateTime.UtcNow;
}