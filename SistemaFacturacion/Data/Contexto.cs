using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Models;

namespace SistemaFacturacion.Data
{
    public class Contexto(DbContextOptions<Contexto> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Ventas> Facturas { get; set; }
        public DbSet<Financiamiento> Financiamientos { get; set; }

        public DbSet<VentasDetalle> FacturaDetalle { get; set; }

        public DbSet<CuentasXcobrar> CuentasXcobrar { get; set; }
    }
}
