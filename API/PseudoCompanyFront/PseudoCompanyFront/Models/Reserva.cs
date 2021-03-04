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

        [Display(Name = "Data de início")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data de fim")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFim { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataReserva = DateTime.Now;

        public Reserva Reservas { get; set; }
    }
}
