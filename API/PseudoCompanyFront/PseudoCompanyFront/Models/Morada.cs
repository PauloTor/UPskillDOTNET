using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoCompanyFront.Models
{
    public class MoradaDTO
    {
        [Key]
        public long MoradaID { get; set; }

        public string Rua { get; set; }

        public string CodigoPostal { get; set; }

        public MoradaDTO(string rua, string codigoPostal)
        {
            Rua = rua;
            CodigoPostal = codigoPostal;
        }
    }
}
