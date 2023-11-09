using Microsoft.EntityFrameworkCore;

namespace WebApi_Foro.Entities
{
    public class CitasDbContext : DbContext
    {
        public CitasDbContext(DbContextOptions<CitasDbContext> options) : base(options)
        {

        }

        public DbSet<Cita> Citas { get; set; }

    }
}
