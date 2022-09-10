namespace ApiBP.Model.ViewModel
{
    public class ModelCuentaUpdate
    {
        public int IdCuenta { get; set; }
        public int Cliente { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public string Estado { get; set; } 
    }
}
