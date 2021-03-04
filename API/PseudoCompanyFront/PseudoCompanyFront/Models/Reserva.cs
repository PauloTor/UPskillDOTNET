using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoCompanyFront.Models
{
    public class Reserva
    {
        [Display(Name = "Nº da reserva")]
        public long ReservaID { get; set; }

        [Display(Name = "Nº do parque")]
        public long ParqueID { get; set; }

        [Display(Name = "Nº do lugar")]
        public long LugarID { get; set; }

        [Display(Name = "Nº do cliente")]
        public long ClienteID { get; set; }

        [Display(Name = "Data de início")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data de fim")]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Display(Name = "Data de reserva")]
        [DataType(DataType.Date)]
        public DateTime DataReserva = DateTime.Now;

        public Reserva Reservas { get; set; }
    }
}
