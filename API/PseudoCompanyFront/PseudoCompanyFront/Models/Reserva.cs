using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoCompanyFront.Models
{
    public class Reserva
    {
        [Display(Name = "Nº Reserva")]
        public long ReservaID { get; set; }

        [Display(Name = "Nº Parque")]
        public long ParqueID { get; set; }

        [Display(Name = "Nº Lugar")]
        public long LugarID { get; set; }

        [Display(Name = "Nº Cliente")]
        public long ClienteID { get; set; }

        public Reserva Reservas { get; }
    }
}
