using ApiBP.Data;
using ApiBP.Inteface;
using ApiBP.Model;
using ApiBP.Model.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiBP.Service
{
    public class ServiceCuenta : ICuenta
    {
        private readonly ApplicationDbContext _context;
        public IConfiguration Configuration { get; }


        public ServiceCuenta(ApplicationDbContext context, IConfiguration Configuration)
        {
            _context = context;
            this.Configuration = Configuration;
        }

        /// <summary>
        /// Ejecucion de SP para Eliminar cuenta
        /// </summary>
        /// <param name="cuentaDelete"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response> SpDeleteCuenta(ModelCuentaDelete cuentaDelete)
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
                    parametros.Add(new SqlParameter("@IdCuenta", cuentaDelete.IdCuenta));
                   

                    var res = await ctxSp.SetDeleteCuenta.FromSqlRaw("DeleteCuenta " +
                        "@IdCuenta ",
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
        /// Ejecucion de SP para insertar datos de cuenta
        /// </summary>
        /// <param name="cuentaInsert"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response> SPInsertCuenta(ModelCuentaInsert cuentaInsert)
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
                    parametros.Add(new SqlParameter("@Cliente", cuentaInsert.Cliente));
                    parametros.Add(new SqlParameter("@Numerocuenta", cuentaInsert.NumeroCuenta));
                    parametros.Add(new SqlParameter("@TipoCuenta", cuentaInsert.TipoCuenta));
                    parametros.Add(new SqlParameter("@SaldoInicial", cuentaInsert.Saldoinicial));

                    var res = await ctxSp.SetInsertCuenta.FromSqlRaw("InsertCuenta " +
                        "@Cliente, " +
                        "@Numerocuenta, " +
                        "@TipoCuenta, " +
                        "@SaldoInicial ",
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
        /// Ejecucion de SP para actualizar datos de cuenta
        /// </summary>
        /// <param name="cuentaUpdate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response> SPUpdateCuenta(ModelCuentaUpdate cuentaUpdate)
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
                    parametros.Add(new SqlParameter("@IdCuenta", cuentaUpdate.IdCuenta));
                    parametros.Add(new SqlParameter("@Cliente", cuentaUpdate.Cliente));
                    parametros.Add(new SqlParameter("@Numerocuenta", cuentaUpdate.NumeroCuenta));
                    parametros.Add(new SqlParameter("@TipoCuenta", cuentaUpdate.TipoCuenta));
                    parametros.Add(new SqlParameter("@SaldoInicial", cuentaUpdate.SaldoInicial));
                    parametros.Add(new SqlParameter("@Estado", cuentaUpdate.Estado));

                    var res = await ctxSp.SetUpdateCuenta.FromSqlRaw("UpdateCuenta " +
                        "@IdCuenta, " +
                        "@Cliente, " +
                        "@Numerocuenta, " +
                        "@TipoCuenta, " +
                        "@SaldoInicial, " +
                        "@Estado ",
                        parametros.ToArray()).ToListAsync();
                    response.ObjetoResult = res.FirstOrDefault().codigo;

                    response.Message = "Actualizado";
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
        /// Obtener listado de cuentas
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Cuentum>> GetCuentas()
        {
            try
            {
                var list = await _context.Cuenta.ToListAsync();

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
        /// Obtener cuenta por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Cuentum> GetCuentasId(int Id)
        {
            try
            {
                var list = await _context.Cuenta.Where(x=> x.IdCuenta == Id).FirstOrDefaultAsync();

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
