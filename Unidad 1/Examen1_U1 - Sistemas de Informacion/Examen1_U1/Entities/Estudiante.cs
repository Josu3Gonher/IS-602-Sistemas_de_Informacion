using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen1_U1.Entities
{
    [Table("estudiantes")]
    public class Estudiante
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nombre")]
        [Required]
        [StringLength(80)]
        public string Nombre { get; set; }

        [Column("numero_cuenta")]
        [Required]
        [StringLength(maximumLength:11,MinimumLength = 11)]
        public string NumeroCuenta { get; set; }

        [Column("correo")]
        [Required]
        [StringLength(50)]
        public string Correo { get; set; }

        //Relación recursiva con el asesor académico (un estudiante puede tener un tutor que también es un estudiante)
        public List<Estudiante> Tutores { get; set; } = new List<Estudiante>();
        public List<Estudiante> EstudiantesEnTutoria { get; set; } = new List<Estudiante>();
    }
}
