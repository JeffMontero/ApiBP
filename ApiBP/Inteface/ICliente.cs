using ApiBP.Data;
using ApiBP.Model;
using ApiBP.Model.ViewModel;

namespace ApiBP.Inteface
{
    public interface ICliente
    {
        Task<Response> SPInsertCliente(ModelClienteInsert clienteInsert);
        Task<Response> SPUpdateCliente(ModelClienteUpdate clienteUpdate);
        Task<Response> SpDeleteCliente(ModelClienteDelete clienteDelete);
        Task<Response> SPInsertPersona(ModelClienteInsert clienteInsert);
        Task<Response> SPUpdatePersona(ModelClienteUpdate clienteUpdate);
        Task<IEnumerable<Cliente>> GetClientes();
        Task<Cliente> GetClienteId(int id);
    }
}
