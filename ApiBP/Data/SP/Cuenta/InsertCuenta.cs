using System.ComponentModel.DataAnnotations;

namespace ApiBP.Data.SP.Cuenta
{
    public class InsertCuenta
    {
        [Key]
        public int codigo { get; set; }
    }
}
