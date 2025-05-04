using Flowspire.Domain.Enums;

namespace Flowspire.API.Models;

public class RegisterCustomerRequest
{
    public string Email { get; set; }
    public string NameFirst { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; } = Gender.NotSpecified;
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
}
