using System.ComponentModel.DataAnnotations;

namespace ApiBP.Data.SP.Cliente
{
    public class InsertCliente
    {
        [Key]
        public int codigo { get; set; }
    }
}
