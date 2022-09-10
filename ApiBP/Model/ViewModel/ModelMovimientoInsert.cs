namespace ApiBP.Model.ViewModel
{
    public class ModelMovimientoInsert
    {
        public int Cuenta { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public string  Descripcion { get; set; }
    }
}
