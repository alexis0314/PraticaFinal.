using System.ComponentModel.DataAnnotations;

namespace PraticaFinal.Models
{
    public class TerapeutaViewModel
    {
        [Key]
        public int TerapeutaID { get; set; }

        [Required(ErrorMessage = "El nombre del terapeuta es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria")]
        public string Especialidad { get; set; }
    }
}
