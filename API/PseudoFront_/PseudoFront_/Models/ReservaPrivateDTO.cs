using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PseudoFront_.Models;


namespace PseudoFront_.DTO
{
    public class ReservaPrivateDTO
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

        [Display(Name = "Para subaluguer")]
        public bool ParaSubAluguer { get; set; }
    }
}
