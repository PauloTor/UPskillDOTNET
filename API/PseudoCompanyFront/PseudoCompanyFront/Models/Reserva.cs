using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoCompanyFront.Models
{
    public class Reserva
    {
        public long ReservaID { get; set; }

        public long ParqueID { get; set; }

        public long LugarID { get; set; }

        public long ClienteID { get; set; }

        public Reserva Reservas { get; }
    }
}
