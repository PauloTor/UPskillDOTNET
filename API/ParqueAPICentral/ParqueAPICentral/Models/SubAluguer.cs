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

        public float Preco { get; set; }

        [ForeignKey("ReservaID")]
        public long ReservaID { get; set; }
        public Reserva Reserva { get; set; }

        public bool Reservado { get; set; }
        
        public string NovoCliente { get; set; }

        public SubAluguer(long reservaID, float preco, bool reservado, string novoC)
        {
            Preco = preco;
            ReservaID = reservaID;
            Reservado = reservado;
            NovoCliente = novoC;
        }

        public SubAluguer()
        {
        }

    }
}
