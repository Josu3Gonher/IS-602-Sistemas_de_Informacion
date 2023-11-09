using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Foro.Entities
{
    [Table("citas")]
    public class Cita
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("fecha")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Column("nombre_medico")]
        [Required]
        [StringLength(100)]
        public string NombreDelMedicoAVisitar { get; set; }

        [Column("paciente_id")]
        public int PacienteId { get; set; }
    }
}
