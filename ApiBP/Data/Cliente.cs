using System;
using System.Collections.Generic;

namespace ApiBP.Data
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuentum>();
        }

        public int IdCliente { get; set; }
        public int Persona { get; set; }
        public string Clave { get; set; } = null!;
        public string Estado { get; set; } = null!;

        public virtual Persona PersonaNavigation { get; set; } = null!;
        public virtual ICollection<Cuentum> Cuenta { get; set; }
    }
}
