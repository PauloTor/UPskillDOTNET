//using PseudoFront_Front.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoFront_.Models
{
    public class Cliente
    {
        [Key]
        public long ClienteID { get; set; }

        public string NomeCliente { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "O Email não é valido")]
        public string EmailCliente { get; set; }

        [Required]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "O NIF deve ser composto por 9 números")] 
        public int NifCliente { get; set; }

        public string MetodoPagamento { get; set; }

        public float Credito { get; set; }

        [ForeignKey("Id")]
        public long Id { get; set; }
        public User User { get; set; }
    }
}


//[Required]
//[RegularExpression(@"^\\d{9}$", ErrorMessage = "NIF length must be 9 numbers")] 