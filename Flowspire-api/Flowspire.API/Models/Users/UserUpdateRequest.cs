﻿using Flowspire.Domain.Enums;

namespace Flowspire.API.Models.Users;

public class UserUpdateRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; } = Gender.NotSpecified;
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public List<UserRole> Roles { get; set; } = new();
}
