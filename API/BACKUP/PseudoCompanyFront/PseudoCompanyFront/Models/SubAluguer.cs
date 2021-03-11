using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoCompanyFront.Models
{
    public class SubAluguer
    {
        public long SubAluguerID { get; set; }

        public float Preco { get; set; }

        public bool Reservado { get; set; }

        public long NovoCliente { get; set; }

        public long ReservaID { get; set; }
        public Reserva Reserva { get; set; }

    }
}
