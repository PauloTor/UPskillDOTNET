using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PseudoFront_.DTO;

namespace PseudoFront_.Models
{
    public class Fatura
    {
        [Display(Name = "Nº da fatura")]
        public long FaturaID { get; set; }

        [Display(Name = "Valor pago")]
        public float PrecoFatura { get; set; }

        [Display(Name = "Nº da reserva")]
        public long ReservaID { get; set; }
        public ReservaPrivateDTO Reserva { get; set; }

        [Display(Name = "Data da fatura")]
        [DataType(DataType.Date)]
        public DateTime DataFatura { get; set; }
    }
}
