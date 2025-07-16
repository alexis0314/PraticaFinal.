using Microsoft.EntityFrameworkCore;

namespace PraticaFinal.Models
{
    public class PraticaFinalContext : DbContext
    {
        public PraticaFinalContext(DbContextOptions<PraticaFinalContext> options)
            : base(options)
        {
        }

        public DbSet<PacienteViewModel> Pacientes { get; set; }
        public DbSet<ServicioViewModel> Servicios { get; set; }
        public DbSet<TerapeutaViewModel> Terapeutas { get; set; }
        public DbSet<CitaViewModel> Citas { get; set; }
    }

}
