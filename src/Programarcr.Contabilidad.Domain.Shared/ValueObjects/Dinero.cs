using System.Collections.Generic;
using Volo.Abp.Domain.Values;

namespace Programarcr.Contabilidad.ValueObjects
{
    public class Dinero: ValueObject
    {
        public decimal Value { get; private set; }
        public string MonedaId { get; private set; }
        public Dinero(decimal value, string monedaId)
        {
            Value = value;
            MonedaId = monedaId;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
            yield return MonedaId;
        }
    }
}