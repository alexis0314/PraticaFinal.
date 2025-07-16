using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PraticaFinal.Models
{
    public class PraticaFinalContextFactory : IDesignTimeDbContextFactory<PraticaFinalContext>
    {
        public PraticaFinalContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PraticaFinalContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-INQDAG5;Database=PraticaFinal;Trusted_Connection=True;TrustServerCertificate=True");

            return new PraticaFinalContext(optionsBuilder.Options);
        }
    }
}
