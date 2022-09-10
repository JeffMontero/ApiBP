using ApiBP.Inteface;
using ApiBP.Model;
using ApiBP.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBP.Controllers
{
    [Route("api/movimiento")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimiento _movimiento;

        public MovimientoController(IMovimiento movimiento)
        {
            _movimiento = movimiento;
        }

        /// <summary>
        /// Insertar datos de movimiento
        /// </summary>
        /// <param name="movimiento"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Response>> InsertMovimiento([FromBody] ModelMovimiento movimiento)
        {
            Response response = new Response();
            try
            {
                var respuesta = await _movimiento.InsertMovimiento(movimiento);

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
        /// Obtener reporte de movimientos por rango de fechas
        /// </summary>
        /// <param name="reporte"></param>
        /// <returns></returns>
        [HttpGet("reporte")]
        public async Task<ActionResult<Response>> GetReporte([FromQuery] ModelReporte reporte)
        {
            Response response = new Response();
            try
            {
                var respuesta = await _movimiento.ReporteMovimiento(reporte);
                return respuesta;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return response;
            }
        }

        /// <summary>
        /// Eliminar movimiento
        /// </summary>
        /// <param name="movimientoDelete"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteMovimiento([FromBody] ModelMovimientoDelete movimientoDelete)
        {
            Response response = new Response();
            try
            {
                var respuesta = await _movimiento.SPDeleteMovimiento(movimientoDelete);

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
        /// Obtener listado de movimientos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Response>> GetMovimientos()
        {
            Response response = new Response();
            try
            {
                var movimientos = await _movimiento.GetMovimientos();

                response.ObjetoResult = movimientos;
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
        /// Obtener datos de movimiento por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("Id")]
        public async Task<ActionResult<Response>> GetMovimientoId(int Id)
        {
            Response response = new Response();
            try
            {
                var movimiento = await _movimiento.GetMovimientoId(Id);

                response.ObjetoResult = movimiento;
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
