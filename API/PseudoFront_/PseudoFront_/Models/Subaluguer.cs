using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PseudoFront_.DTO;

namespace PseudoFront_.Models
{
    public class Subaluguer
    {
        [Display(Name = "Nº do subaluguer")]
        public long SubAluguerID { get; set; }

        [Display(Name = "Preço")]
        public float Preco { get; set; }

        [Display(Name = "Sub-reservado")]
        public bool Reservado { get; set; }

        [Display(Name = "Nº do sub-cliente")]
        public long NovoCliente { get; set; }

        [Display(Name = "Nº da reserva original")]
        public long ReservaID { get; set; }
        //public ReservaPrivateDTO Reserva { get; set; }
    }
}
