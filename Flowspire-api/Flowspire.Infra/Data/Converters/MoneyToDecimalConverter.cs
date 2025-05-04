using Flowspire.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Flowspire.Infra.Data.Converters
{
    public class MoneyToDecimalConverter : ValueConverter<Money, decimal>
    {
        public MoneyToDecimalConverter()
            : base(
                m => m.Value,
                v => new Money(v))
        { }
    }
}
