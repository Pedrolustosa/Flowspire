using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowspire.Domain.ValueObjects
{
    public class FullName
    {
        public string FirstName { get; }
        public string LastName { get; }

        private FullName() { }

        public FullName(string first, string last)
        {
            if (string.IsNullOrWhiteSpace(first)) throw new ArgumentException("First name is required.");
            if (string.IsNullOrWhiteSpace(last)) throw new ArgumentException("Last name is required.");

            FirstName = first.Trim();
            LastName = last.Trim();
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }

}
