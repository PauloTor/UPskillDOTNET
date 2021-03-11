using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoFront_.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "FirstName é necessário")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName é necessário")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName é necessário")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email é necessário")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\\d{9}$", ErrorMessage = "NIF length must be 9 numbers")]
        public int Nif { get; set; }

        public float Credito { get; set; }

        [Required(ErrorMessage = "Metodo Pagamento é necessário")]
        public string MetodoPagamento { get; set; }

        [Required(ErrorMessage = "Password é necessária")]
        public string Password { get; set; }

    }
}


