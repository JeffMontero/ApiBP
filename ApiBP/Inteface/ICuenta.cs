using ApiBP.Data;
using ApiBP.Model;
using ApiBP.Model.ViewModel;

namespace ApiBP.Inteface
{
    public interface ICuenta
    {
        Task<Response> SPInsertCuenta(ModelCuentaInsert cuentaInsert);
        Task<Response> SPUpdateCuenta(ModelCuentaUpdate cuentaUpdate);
        Task<Response> SpDeleteCuenta(ModelCuentaDelete cuentaDelete);
        Task<IEnumerable<Cuentum>> GetCuentas();
        Task<Cuentum> GetCuentasId(int Id);
    }
}
