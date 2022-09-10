using ApiBP.Data;
using ApiBP.Inteface;
using ApiBP.Model;
using ApiBP.Model.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiBP.Service
{
    public class ServiceMovimiento : IMovimiento
    {
        private readonly ApplicationDbContext _context;
        public IConfiguration Configuration { get; }

        public ServiceMovimiento(ApplicationDbContext context, IConfiguration Configuration)
        {
            _context = context;
            this.Configuration = Configuration;
        }

        /// <summary>
        /// Logica de movimientos y validaciones del mismo
        /// </summary>
        /// <param name="movimiento"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response> InsertMovimiento(ModelMovimiento movimiento)
        {
            Response response = new Response();

            try
            {
                //Verificar si existe cuenta y está habilitada
                var cuenta = _context.Cuenta.Where(x => x.IdCuenta == movimiento.Cuenta && x.Estado == "A").FirstOrDefault();
                if (cuenta == null)
                {
                    throw new Exception("Cuenta no existe o está inhabilitada");
                   
                }

                var valorMov = movimiento.Valor;
                decimal saldo = 0;
                var descripcion = "";
                decimal sumRetiro = 0;

                //Obtener movimientos de cuenta
                var listMov = _context.Movimientos.Where(x=> x.Cuenta == movimiento.Cuenta).ToList();

                if (listMov.Count()<=0)
                {
                    saldo = cuenta.SaldoInicial;
                }
                else
                {
                    //Retiro maximo diario
                    sumRetiro = listMov.Where(x => x.TipoMovimiento == "D" && x.Fecha >= DateTime.Today).Sum(y => y.Valor);
                    saldo = listMov.First().Saldo;
                }

                //Credito en cuenta
                if (movimiento.TipoMovimiento.Equals("C") && valorMov>0)
                {

                    saldo += valorMov;
                    descripcion = "Depósito de:" + valorMov;
 
                }
                //Debito en cuenta
                else if(movimiento.TipoMovimiento.Equals("D") && valorMov>0){

                    saldo -= valorMov;
                    descripcion = "Retiro de:" + valorMov;

                    //Validacion de Cupo diario 
                    if ((sumRetiro+valorMov)>1000)
                    {
                        throw new Exception("Cupo diario excedido");
                    }
                    //Validacion de saldo suficiente
                    if (saldo<0)
                    {
                        throw new Exception("Saldo no disponible");
                    }
                }
                else
                {
                    throw new Exception("Operacion incorrecta");
                }

                ModelMovimientoInsert movInsert = new ModelMovimientoInsert();

                movInsert.Cuenta = movimiento.Cuenta;
                movInsert.TipoMovimiento = movimiento.TipoMovimiento;
                movInsert.Valor = movimiento.Valor;
                movInsert.Saldo = saldo;
                movInsert.Descripcion = descripcion;

                //Ejecutamos SP para guardar movimiento
               var res= await SPInsertMovimiento(movInsert);

                response = res;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("ErrorConcurrencia");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("ErrorIngresoDatos");
            }
            catch (SqlException ex)
            {
                throw new Exception("ErrorConexionBaseDatos");
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.ObjetoResult = null;
            }
            return response;
        }

        /// <summary>
        /// ejecucion de SP para Insertar datos de movimiento
        /// </summary>
        /// <param name="movimientoInsert"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response> SPInsertMovimiento(ModelMovimientoInsert movimientoInsert)
        {
            Response response = new Response();
            try
            {
               
                var builderDbContext = new DbContextOptionsBuilder<ApplicationDbContext>();
                string _connectionString = Configuration.GetConnectionString("ConexionDB");
                builderDbContext.UseSqlServer(_connectionString);
                List<SqlParameter> parametros = new List<SqlParameter>();

                using (ApplicationStoredProceduresDbContext ctxSp = new ApplicationStoredProceduresDbContext(builderDbContext.Options))
                {
                    parametros.Add(new SqlParameter("@Cuenta", movimientoInsert.Cuenta));
                    parametros.Add(new SqlParameter("@TipoMovimiento", movimientoInsert.TipoMovimiento));
                    parametros.Add(new SqlParameter("@Valor", movimientoInsert.Valor));
                    parametros.Add(new SqlParameter("@Saldo", movimientoInsert.Saldo));
                    parametros.Add(new SqlParameter("@MovDescripcion", movimientoInsert.Descripcion));

                    var res = await ctxSp.SetInsertMovimiento.FromSqlRaw("InserMovimiento " +
                        "@Cuenta, " +
                        "@TipoMovimiento, " +
                        "@Valor, " +
                        "@Saldo, " +
                        "@MovDescripcion ",
                        parametros.ToArray()).ToListAsync();
                    response.ObjetoResult = res.FirstOrDefault().codigo;

                    response.Message = "Insertado";
                    response.IsSuccess = true;
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("ErrorConcurrencia");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("ErrorIngresoDatos");
            }
            catch (SqlException ex)
            {
                throw new Exception("ErrorConexionBaseDatos");
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Message = ex.Message;
                response.ObjetoResult = null;
            }
            return response;
        }

        /// <summary>
        /// Reporte de movimientos por idCliente y rango de fechas
        /// </summary>
        /// <param name="reporte"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response> ReporteMovimiento(ModelReporte reporte)
        {
            Response response = new Response(); 
            try
            {
                //Consulta y armado de Json para reporte
                var dataReporte = await (from cli in _context.Clientes
                                         join per in _context.Personas on cli.Persona equals per.IdPersona
                                         join cue in _context.Cuenta on cli.IdCliente equals cue.Cliente
                                         join mov in _context.Movimientos on cue.IdCuenta equals mov.Cuenta
                                         where cli.IdCliente == reporte.IdCliente && (mov.Fecha.Date >= reporte.FechaInicio.Date &&
                                         mov.Fecha.Date <= reporte.FechaFin)
                                         select new
                                         {
                                             Fecha = mov.Fecha,
                                             Nombre = per.Nombre,
                                             NumeroCuenta = cue.NumeroCuenta,
                                             Tipo = cue.TipoCuenta,
                                             SaldoInicial = cue.SaldoInicial,
                                             Estado = cue.Estado,
                                             Movimiento = mov.Valor,
                                             SaldoDisponible = mov.Saldo
                                         }).ToListAsync();
                response.IsSuccess= true;
                response.Message = "OK";
                response.ObjetoResult = dataReporte;
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("ErrorConcurrencia");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("ErrorIngresoDatos");
            }
            catch (SqlException ex)
            {
                throw new Exception("ErrorConexionBaseDatos");
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Ejecutar SP para eliminar movimiento
        /// </summary>
        /// <param name="movimientoDelete"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response> SPDeleteMovimiento(ModelMovimientoDelete movimientoDelete)
        {
            Response response = new Response();
            try
            {

                var builderDbContext = new DbContextOptionsBuilder<ApplicationDbContext>();
                string _connectionString = Configuration.GetConnectionString("ConexionDB");
                builderDbContext.UseSqlServer(_connectionString);
                List<SqlParameter> parametros = new List<SqlParameter>();

                using (ApplicationStoredProceduresDbContext ctxSp = new ApplicationStoredProceduresDbContext(builderDbContext.Options))
                {
                    parametros.Add(new SqlParameter("@IdMovimiento", movimientoDelete.IdMovimiento));

                    var res = await ctxSp.SetDeleteMovimiento.FromSqlRaw("DeleteMovimiento " +
                        "@IdMovimiento ",
                        parametros.ToArray()).ToListAsync();
                    response.ObjetoResult = res.FirstOrDefault().codigo;

                    response.Message = "Eliminado";
                    response.IsSuccess = true;
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("ErrorConcurrencia");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("ErrorIngresoDatos");
            }
            catch (SqlException ex)
            {
                throw new Exception("ErrorConexionBaseDatos");
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Message = ex.Message;
                response.ObjetoResult = null;
            }
            return response;
        }

        /// <summary>
        /// Obtener listado de movimientos
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Movimiento>> GetMovimientos()
        {
            try
            {
                var list = await _context.Movimientos.ToListAsync();
                return list;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("ErrorConcurrencia");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("ErrorIngresoDatos");
            }
            catch (SqlException ex)
            {
                throw new Exception("ErrorConexionBaseDatos");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Obtener movimiento por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Movimiento> GetMovimientoId(int Id)
        {
            try
            {
                var list = await _context.Movimientos.Where(x => x.IdMovimiento == Id).FirstOrDefaultAsync();
                return list;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("ErrorConcurrencia");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("ErrorIngresoDatos");
            }
            catch (SqlException ex)
            {
                throw new Exception("ErrorConexionBaseDatos");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
