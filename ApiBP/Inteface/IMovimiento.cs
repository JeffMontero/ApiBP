using ApiBP.Data;
using ApiBP.Model;
using ApiBP.Model.ViewModel;

namespace ApiBP.Inteface
{
    public interface IMovimiento
    {
        Task<Response> InsertMovimiento(ModelMovimiento movimiento);
        Task<Response> SPInsertMovimiento(ModelMovimientoInsert movimientoInsert);
        Task<Response> ReporteMovimiento(ModelReporte reporte);
        Task<Response> SPDeleteMovimiento(ModelMovimientoDelete movimientoDelete);
        Task<IEnumerable<Movimiento>> GetMovimientos();
        Task<Movimiento> GetMovimientoId(int Id);

    }
}
