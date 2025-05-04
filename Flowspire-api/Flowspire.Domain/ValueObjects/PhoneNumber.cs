using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowspire.Domain.ValueObjects
{
    using System.Text.RegularExpressions;

    public class PhoneNumber
    {
        public string Value { get; }

        private PhoneNumber() { }

        public PhoneNumber(string number)
        {
            var regex = new Regex(@"^\+?[1-9]\d{1,14}$");
            if (!regex.IsMatch(number)) throw new ArgumentException("Invalid phone number format.");
            Value = number;
        }

        public override string ToString() => Value;
    }

}
