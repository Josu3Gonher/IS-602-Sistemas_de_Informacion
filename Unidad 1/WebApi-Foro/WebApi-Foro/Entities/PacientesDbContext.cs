using Microsoft.EntityFrameworkCore;

namespace WebApi_Foro.Entities
{
    public class PacientesDbContext : DbContext
    {
        public PacientesDbContext(DbContextOptions<PacientesDbContext> options) : base(options)
        {

        }

        public DbSet<Paciente> Pacientes { get; set; }
    }
}
