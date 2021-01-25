using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPI.Models
{
    public class Fatura
    {
        public long FaturaID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFatura { get; set; }

       public string MetodoPagamentoFatura { get; set; }

        public float PrecoFatura { get; set; }

        public long ReservaID { get; set; }
        [ForeignKey("ReservaID")]
        public Reserva Reserva { get; set; }

    }
}
