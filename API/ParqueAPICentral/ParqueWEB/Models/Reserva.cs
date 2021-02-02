using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParqueAPICentral.Models
{
    public class Reserva
    {
        public long ReservaID { get; set; }

        public DateTime DataReserva { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public DateTime DataSaida { get; set; }

        [ForeignKey("ClienteID")]
        public long ClienteID { get; set; }
        public Cliente Cliente { get; set; }

        [ForeignKey("LugarID")]
        public long LugarID { get; set; }
        public Lugar Lugar { get; set; }

        public bool SubAlugado { get; set; } 
    }
}
