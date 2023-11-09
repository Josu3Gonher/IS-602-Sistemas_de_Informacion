using Microsoft.EntityFrameworkCore;

namespace Examen1_U1.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Estudiante> Estudiantes { get; set; }

        //mapear la relación recursiva entre estudiantes y sus asesores
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación recursiva
            modelBuilder.Entity<Estudiante>()
                .HasMany(e => e.Tutores)
                .WithMany(e => e.EstudiantesEnTutoria)
                .UsingEntity(j => j.ToTable("relacion_estudiantes"));
        }
    }
}
