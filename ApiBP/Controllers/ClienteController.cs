using ApiBP.Inteface;
using ApiBP.Model;
using ApiBP.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBP.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ICliente _cliente;

        public ClienteController(ICliente cliente)
        {
            _cliente = cliente;
        }

        /// <summary>
        /// Insertar Datos de cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Response>> InsertCliente([FromBody] ModelClienteInsert clienteInsert)
        {
            Response response = new Response();
            try
            {
                var respuesta = await _cliente.SPInsertCliente(clienteInsert);

                response = respuesta;
                return response;
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
            
        }

        /// <summary>
        /// Actualizar datos del cliente
        /// </summary>
        /// <param name="clienteUpdate"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Response>> UpdateCliente([FromBody] ModelClienteUpdate clienteUpdate)
        {
            Response response = new Response();
            try
            {
                var respuesta = await _cliente.SPUpdateCliente(clienteUpdate);

                response = respuesta;
                return response;
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }

        }

        /// <summary>
        /// Eliminar datos del cliente
        /// </summary>
        /// <param name="clienteDelete"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteCliente([FromBody] ModelClienteDelete clienteDelete)
        {
            Response response = new Response();
            try
            {
                var respuesta = await _cliente.SpDeleteCliente(clienteDelete);

                response = respuesta;
                return response;
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }

        }

        /// <summary>
        /// Obtener listado de clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Response>> GetClientes()
        {
            Response response = new Response();
            try
            {
                var clientes = await _cliente.GetClientes();

                response.ObjetoResult = clientes;
                response.IsSuccess = true;

                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return response;
            }
        }

        /// <summary>
        /// Obtener datos de cliente por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<Response>> GetClientesId(int id)
        {
            Response response = new Response();
            try
            {
                var clientes = await _cliente.GetClienteId(id);

                response.ObjetoResult = clientes;
                response.IsSuccess = true;

                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return response;
            }
        }
    }
}
