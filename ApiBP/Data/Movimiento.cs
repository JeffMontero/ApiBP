using System;
using System.Collections.Generic;

namespace ApiBP.Data
{
    public partial class Movimiento
    {
        public long IdMovimiento { get; set; }
        public int Cuenta { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; } = null!;
        public string? MovDescripcion { get; set; }

        public virtual Cuentum CuentaNavigation { get; set; } = null!;
    }
}
