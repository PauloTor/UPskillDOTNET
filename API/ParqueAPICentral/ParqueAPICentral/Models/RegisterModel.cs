using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\\d{9}$", ErrorMessage = "NIF deve ter 9 números")]
        //[Index(nameof(Nif), IsUnique = true)]
        public int Nif { get; set; }

        public float Credito { get; set; }
       
        [Required]
        public string MetodoPagamento { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
