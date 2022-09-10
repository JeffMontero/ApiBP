using System.ComponentModel.DataAnnotations;

namespace ApiBP.Data.SP.Cuenta
{
    public class DeleteCuenta
    {
        [Key]
        public int codigo { get; set; }
    }
}
