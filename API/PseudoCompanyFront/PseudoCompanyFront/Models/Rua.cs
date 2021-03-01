using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoCompanyFront.Models
{
    public class RuaDTO
    {
        [Key]
        public long RuaID { get; set; }

        public string NomeRua { get; set; }

        public string CodigoPostal { get; set; }

        public int Lotacao { get; set; } 
    }
}
