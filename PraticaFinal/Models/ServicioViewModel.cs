using System.ComponentModel.DataAnnotations;

namespace PraticaFinal.Models
{
    public class ServicioViewModel
    {
        [Key]
        public int ServicioID { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria")]
        [Range(1, 300, ErrorMessage = "Duración debe estar entre 1 y 300 minutos")]
        public int DuracionMin { get; set; } // Se usará para calcular en Cita
    }
}
