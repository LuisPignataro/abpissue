using JetBrains.Annotations;
using Programarcr.Contabilidad.Configuracion;
using Programarcr.Contabilidad.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Programarcr.Contabilidad.Cuentas
{
    public class Cuenta: FullAuditedAggregateRoot<string>
    {
        public CuentaTypeEnum Tipo { get; protected set; }
        
        [NotNull]
        public Moneda  Moneda { get; protected set; }

        public BalanceTypeEnum Balance { get; protected set; }

        [NotNull]
        public string Nombre { get; set; }
        
        [CanBeNull]
        public string ParentId { get; protected set; }
        public virtual Cuenta Parent { get; protected set; } = null;

        public bool HasChildren { get; protected set; }
        public int Level { get; set; }
        public ICollection<Cuenta> Children { get; protected set; }

        protected Cuenta() {

            Children = new Collection<Cuenta>();
        }

        public Cuenta(string id, string parentId, CuentaTypeEnum tipo, Moneda moneda, BalanceTypeEnum balance): this()
        {
            Check.NotNull(moneda, nameof(moneda));
            Check.NotNull(id, nameof(id));
            Check.NotNull(moneda, nameof(moneda));

            Id = id;
            if (!string.IsNullOrEmpty(parentId))
                ParentId = parentId;

            Tipo = tipo;
            Moneda = moneda;
            Balance = balance;
        }

        public Cuenta AddChild(string numero, Moneda moneda, BalanceTypeEnum balance, string nombre, ICuentaFormatProvider formatProvider)
        {
            if (!formatProvider.IsValid(numero))
                throw new NumeroCuentaNoValidaException(numero, nameof(numero));

            numero =  formatProvider.RemoveFormat(numero);

            if (formatProvider.GetLevelOf(numero) != (formatProvider.GetLevelOf(Id) + 1))
                throw new NumeroCuentaNoHijaException(numero, Id);

            var cuenta = new Cuenta(numero, Id, Tipo, moneda, balance)
            {
                Nombre = nombre,
                Level = this.Level + 1
            };

            Children.Add(cuenta);
            HasChildren = true;

            return cuenta;
        }
    }
}
