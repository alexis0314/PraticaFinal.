using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraticaFinal.Models
{
    public class Cita
    {
        [Key]
        public int CitaID { get; set; }

        [Required]
        public int PacienteID { get; set; }

        [Required]
        public int ServicioID { get; set; }

        [Required]
        public int TerapeutaID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; }

        public virtual PacienteViewModel? Paciente { get; set; }
        public virtual ServicioViewModel? Servicio { get; set; }
        public virtual TerapeutaViewModel? Terapeuta { get; set; }

        [NotMapped]
        public string Estado
        {
            get
            {
                var fechaHora = Fecha.Date + Hora;
                if (DateTime.Now < fechaHora)
                    return "Vigente";
                else if (DateTime.Now >= fechaHora && DateTime.Now < fechaHora.AddMinutes(Servicio?.DuracionMin ?? 0))
                    return "En proceso";
                else
                    return "Finalizado";
            }
        }
    }
}
