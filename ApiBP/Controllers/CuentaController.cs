using ApiBP.Inteface;
using ApiBP.Model;
using ApiBP.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBP.Controllers
{
    [Route("api/cuenta")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuenta _cuenta;

        public CuentaController(ICuenta cuenta)
        {
            _cuenta = cuenta;
        }

        /// <summary>
        /// Insertar datos de nueva cuenta
        /// </summary>
        /// <param name="cuentaInsert"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Response>> InsertCuenta([FromBody] ModelCuentaInsert cuentaInsert)
        {
            Response response = new Response();
            try
            {
                var respuesta = await _cuenta.SPInsertCuenta(cuentaInsert);

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
        /// Actualizar datos de cuenta
        /// </summary>
        /// <param name="cuentaUpdate"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Response>> UpdateCuenta([FromBody] ModelCuentaUpdate cuentaUpdate)
        {
            Response response = new Response();
            try
            {
                var respuesta = await _cuenta.SPUpdateCuenta(cuentaUpdate);

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
        /// Eliminar datos de cuenta
        /// </summary>
        /// <param name="cuentaDelete"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteCuenta([FromBody] ModelCuentaDelete cuentaDelete)
        {
            Response response = new Response();
            try
            {
                var respuesta = await _cuenta.SpDeleteCuenta(cuentaDelete);

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
        /// Obtener listado de todas las cuentas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Response>> GetCuentas()
        {
            Response response = new Response();
            try
            {
                var cuentas = await _cuenta.GetCuentas();

                response.ObjetoResult = cuentas;
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
        /// Obtener cuenta por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("Id")]
        public async Task<ActionResult<Response>> GetCuentaId(int Id)
        {
            Response response = new Response();
            try
            {
                var cuentas = await _cuenta.GetCuentasId(Id);

                response.ObjetoResult = cuentas;
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
