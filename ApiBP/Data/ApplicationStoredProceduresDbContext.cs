using ApiBP.Data.SP.Cliente;
using ApiBP.Data.SP.Cuenta;
using ApiBP.Data.SP.Movimiento;
using Microsoft.EntityFrameworkCore;

namespace ApiBP.Data
{
    public class ApplicationStoredProceduresDbContext : ApplicationDbContext
    {
        public ApplicationStoredProceduresDbContext()
        {
        }

        public ApplicationStoredProceduresDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region Cliente
        public virtual DbSet<InsertCliente> SetInsertCliente { get; set; }
        public virtual DbSet<UpdateCliente> SetUpdateCliente { get; set; }
        public virtual DbSet<DeleteCliente> SetDeleteCliente { get; set; }
        public virtual DbSet<InsertPersona> SetInsertPersona { get; set; }
        public virtual DbSet<UpdatePersona> SetUpdatePersona { get; set; }
        #endregion

        #region Cuenta
        public virtual DbSet<InsertCuenta> SetInsertCuenta { get; set; }
        public virtual DbSet<UpdateCuenta> SetUpdateCuenta { get; set; }
        public virtual DbSet<DeleteCuenta> SetDeleteCuenta { get; set; }
        #endregion

        #region Movimiento
        public virtual DbSet<InsertMovimiento> SetInsertMovimiento { get; set; }
        public virtual DbSet<DeleteMovimiento> SetDeleteMovimiento { get; set; }
        #endregion
    }
}
