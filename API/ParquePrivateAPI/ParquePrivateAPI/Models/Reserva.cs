using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParquePrivateAPI.Models
{
    public class Reserva
    {
        public long ReservaID { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataReserva { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [ForeignKey("LugarID")]
        public long LugarID { get; set; }        
        public Lugar Lugar { get; set; }
    }
}
