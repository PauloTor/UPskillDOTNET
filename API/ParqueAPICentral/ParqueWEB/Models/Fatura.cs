using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Models
{
    public class Fatura
    {
        public long FaturaID { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFatura { get; set; }

        public float PrecoFatura { get; set; }

        [ForeignKey("ReservaID")]
        public long ReservaID { get; set; }
        public Reserva Reserva { get; set; }

        public Fatura(DateTime dataFatura, float precoFatura, long reservaID)
        {
            DataFatura = dataFatura;
            PrecoFatura = precoFatura;
            ReservaID = reservaID;
        }
    }
}
