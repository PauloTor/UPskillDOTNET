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

        [ForeignKey("ClienteID")]
        public long ClienteID { get; set; }
        public Cliente Cliente { get; set; }

        public bool SubAlugado { get; set; }

        public static implicit operator Reserva(ReservaDto v)
        {
            throw new NotImplementedException();
        }
    }
}