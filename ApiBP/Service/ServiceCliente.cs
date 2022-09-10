using ApiBP.Data;
using ApiBP.Inteface;
using ApiBP.Model;
using ApiBP.Model.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiBP.Service
{
    public class ServiceCliente : ICliente
    {
        private readonly ApplicationDbContext _context;
        public IConfiguration Configuration { get; }


        public  ServiceCliente(ApplicationDbContext context, IConfiguration Configuration)
        {
            _context = context;
            this.Configuration = Configuration;
        }

        /// <summary>
        /// Ejecucion de SP para insertar datos cliente
        /// </summary>
        /// <param name="clienteInsert"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response> SPInsertCliente(ModelClienteInsert clienteInsert)
        {
           Response response= new Response();
            try
            {
                var idPersona = await SPInsertPersona(clienteInsert);
                var builderDbContext = new DbContextOptionsBuilder<ApplicationDbContext>();
                string _connectionString = Configuration.GetConnectionString("ConexionDB");
                builderDbContext.UseSqlServer(_connectionString);
                List<SqlParameter> parametros = new List<SqlParameter>();

                using (ApplicationStoredProceduresDbContext ctxSp = new ApplicationStoredProceduresDbContext(builderDbContext.Options))
                {
                    parametros.Add(new SqlParameter("@Persona", idPersona.ObjetoResult));
                    parametros.Add(new SqlParameter("@Clave", clienteInsert.Contraseña));

                    var res = await ctxSp.SetInsertCliente.FromSqlRaw("InsertCliente " +
                        "@Persona, " +
                        "@Clave ",
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
        /// Ejecucion de Sp para actualizar datos cliente 
        /// </summary>
        /// <param name="clienteUpdate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response> SPUpdateCliente(ModelClienteUpdate clienteUpdate)
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
                    parametros.Add(new SqlParameter("@IdCliente", clienteUpdate.IdCliente));
                    parametros.Add(new SqlParameter("@Clave", clienteUpdate.Contraseña));
                    parametros.Add(new SqlParameter("@Estado", clienteUpdate.Estado));

                    var res = await ctxSp.SetUpdateCliente.FromSqlRaw("UpdateCliente " +
                        "@IdCliente, " +
                        "@Clave, " +
                        "@Estado ",
                        parametros.ToArray()).ToListAsync();
                    await SPUpdatePersona(clienteUpdate);
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
        /// Ejecucion de SP para eliminar datos de cliente
        /// </summary>
        /// <param name="clienteDelete"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response> SpDeleteCliente(ModelClienteDelete clienteDelete)
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
                    parametros.Add(new SqlParameter("@IdCliente", clienteDelete.IdCliente));
                   

                    var res = await ctxSp.SetDeleteCliente.FromSqlRaw("DeleteCliente " +
                        "@IdCliente ",
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
        /// Ejecucion de SP para insertar datos de persona
        /// </summary>
        /// <param name="clienteInsert"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response> SPInsertPersona(ModelClienteInsert clienteInsert)
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
                    parametros.Add(new SqlParameter("@Nombre", clienteInsert.Nombre));
                    parametros.Add(new SqlParameter("@Genero", clienteInsert.Genero));
                    parametros.Add(new SqlParameter("@Edad", clienteInsert.Edad));
                    parametros.Add(new SqlParameter("@Identificacion", clienteInsert.Identificacion));
                    parametros.Add(new SqlParameter("@Direccion", clienteInsert.Direccion));
                    parametros.Add(new SqlParameter("@Telefono", clienteInsert.Telefono));

                    var res = await ctxSp.SetInsertPersona.FromSqlRaw("InsertPersona " +
                        "@Nombre, " +
                        "@Genero, " +
                        "@Edad, " +
                        "@Identificacion, " +
                        "@Direccion, " +
                        "@Telefono ",
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
        /// Ejecucion de SP para actualizar datos de persona
        /// </summary>
        /// <param name="clienteUpdate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response> SPUpdatePersona(ModelClienteUpdate clienteUpdate)
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
                    parametros.Add(new SqlParameter("@IdPersona", clienteUpdate.IdPersona));
                    parametros.Add(new SqlParameter("@Nombre", clienteUpdate.Nombre));
                    parametros.Add(new SqlParameter("@Genero", clienteUpdate.Genero));
                    parametros.Add(new SqlParameter("@Edad", clienteUpdate.Edad));
                    parametros.Add(new SqlParameter("@Identificacion", clienteUpdate.Identificacion));
                    parametros.Add(new SqlParameter("@Direccion", clienteUpdate.Direccion));
                    parametros.Add(new SqlParameter("@Telefono", clienteUpdate.Telefono));

                    var res = await ctxSp.SetUpdatePersona.FromSqlRaw("UpdatePersona " +
                        "@IdPersona, "+
                        "@Nombre, " +
                        "@Genero, " +
                        "@Edad, " +
                        "@Identificacion, " +
                        "@Direccion, " +
                        "@Telefono ",
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
        /// Obtener listado de clientes
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Cliente>> GetClientes()
        {
           
            try
            {
                var list = await _context.Clientes.ToListAsync();

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
        /// Obtener cliente por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Cliente> GetClienteId(int id)
        {
            try
            {
                var list = await _context.Clientes.Where(x => x.IdCliente == id).FirstOrDefaultAsync();
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
