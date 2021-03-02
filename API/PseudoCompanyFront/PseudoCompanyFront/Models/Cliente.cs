using PseudoCompanyFront.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoCompanyFront.Models
{
    public class Cliente
    {
        [Key]
        public long ClienteID { get; set; }

        public string NomeCliente { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailCliente { get; set; }

        [Required]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "NIF length must be 9 numbers")] 
        public int NifCliente { get; set; }

        public string MetodoPagamento { get; set; }

        public float Credito { get; set; }

        [ForeignKey("Id")]
        public long Id { get; set; }
        public User User { get; set; }
    }
}
