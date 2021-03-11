using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.DTO
{
    public class LugarReserva:LugarDTO
    {
        public bool subReservado { get; set; }

        public long parqueId { get; set; }
        public long subAluguerId { get; internal set; }

    }
}
