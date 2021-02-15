using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ParqueAPICentral.Models
{
    public class SubAluguer
    {
        public long SubAluguerID { get; set; }

        public float PrecoHora { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataSubAluguer { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFim { get; set; }

        [ForeignKey("ReservaID")]
        public long ReservaID { get; set; }
        public Reserva Reserva { get; set; }


        public SubAluguer(float precoHora, DateTime dataSubAluguer, DateTime dataInicio, DateTime dataFim, long reservaID)
        {
            PrecoHora = precoHora;
            DataSubAluguer = dataSubAluguer;
            DataInicio = dataInicio;
            DataFim = dataFim;
            ReservaID = reservaID;
        }

    }
}
