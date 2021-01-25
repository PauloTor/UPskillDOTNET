using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParqueAPI.Models
{
    public class Reserva
    {
        public long ReservaID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataReserva { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFim { get; set; }

        public string MetodoPagamentoReserva { get; set; }

        public bool PrePagamento { get; set; }

        public long FaturaID { get; set; }
        [ForeignKey("FaturaID")]
        public Fatura Fatura { get; set; }

        //public long QRCodeApiID { get; set; }
        //[ForeignKey("QRCodeApiID")]
        //public QRCodeApiID { get; set; }

        public long ClienteID { get; set; }
        [ForeignKey("ClienteID")]
        public Cliente Cliente { get; set; }

        public long LugarID { get; set; }
        [ForeignKey("LugarID")]
        public Lugar Lugar { get; set; }
    }
}
