using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Foro.Entities
{
    [Table("pacientes")]
    public class Paciente
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("nombre")]
        [Required]
        [StringLength(70)]
        public string Nombre { get; set; }

        [Column("numero_telefono")]
        [Required]
        [StringLength(20)]
        public string NumeroTelefono { get; set; }

    }
}
