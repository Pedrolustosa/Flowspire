using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowspire.Domain.ValueObjects
{
    public class Address
    {
        public string? Line1 { get; }
        public string? Line2 { get; }
        public string? City { get; }
        public string? State { get; }
        public string? Country { get; }
        public string? PostalCode { get; }

        private Address() { }

        public Address(string? line1, string? line2, string? city, string? state, string? country, string? postalCode)
        {
            Line1 = line1?.Trim();
            Line2 = line2?.Trim();
            City = city?.Trim();
            State = state?.Trim();
            Country = country?.Trim();
            PostalCode = postalCode?.Trim();
        }
    }

}
