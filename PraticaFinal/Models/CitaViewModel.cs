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
        public TimeSpan Hora { get; set; }

        // Cálculo desde Servicio
        [NotMapped]
        public int Duracion
        {
            get => Servicio != null ? Servicio.DuracionMin : 0;
        }

        // Cálculo de días restantes
        [NotMapped]
        public int DiasRestantes
        {
            get => (Fecha.Date - DateTime.Now.Date).Days;
        }

        // Estado de la cita
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

        // Relaciones
        public virtual PacienteViewModel Paciente { get; set; }
        public virtual ServicioViewModel Servicio { get; set; }
        public virtual TerapeutaViewModel Terapeuta { get; set; }
    }
}
