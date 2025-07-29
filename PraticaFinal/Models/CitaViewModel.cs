using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraticaFinal.Models
{
    public class CitaViewModel
    {
        [Key]
        public int CitaID { get; set; }

        [Required]
        [ForeignKey("Paciente")]
        public int PacienteID { get; set; }

        [Required]
        [ForeignKey("Servicio")]
        public int ServicioID { get; set; }

        [Required]
        [ForeignKey("Terapeuta")]
        public int TerapeutaID { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora es obligatoria")]
        [Range(8, 18, ErrorMessage = "La hora debe estar entre 8 y 18")]
        public int Hora { get; set; }

        // Relaciones
        public virtual PacienteViewModel Paciente { get; set; }
        public virtual ServicioViewModel Servicio { get; set; }
        public virtual TerapeutaViewModel Terapeuta { get; set; }

        // Cálculos no mapeados a la base de datos
        [NotMapped]
        public int Duracion => Servicio != null ? Servicio.DuracionMin : 0;

        [NotMapped]
        public int DiasRestantes => (Fecha.Date - DateTime.Now.Date).Days;

        [NotMapped]
        public string Estado
        {
            get
            {
                var hoy = DateTime.Now.Date;
                if (Fecha.Date > hoy)
                    return "Vigente";
                else if (Fecha.Date == hoy)
                    return "En proceso";
                else
                    return "Finalizado";
            }
        }
    }
}
