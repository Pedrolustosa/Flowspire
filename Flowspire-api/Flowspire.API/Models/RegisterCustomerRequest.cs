using System;

namespace Flowspire.API.Models
{
    public class RegisterCustomerRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }  // Required
        public string LastName { get; set; }   // Required
        public string Password { get; set; }
        public string PhoneNumber { get; set; } // Required
        public DateTime? BirthDate { get; set; } // Optional
        public Gender Gender { get; set; } = Gender.NotSpecified;
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }
}
