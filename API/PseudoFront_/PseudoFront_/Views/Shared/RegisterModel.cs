using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoFront_.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Primeiro Nome é necessário")]
        [Display(Name = "Primeiro Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Sobrenome é necessário")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName é necessário")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email é necessário")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\\d{9}$", ErrorMessage = "NIF deve ter 9 números")]
        public int Nif { get; set; }

        public float Credito { get; set; }

        [Required(ErrorMessage = "Metodo Pagamento é necessário")]
        [Display(Name = "Método de Pagamento")]
        public string MetodoPagamento { get; set; }

        [Required(ErrorMessage = "Password é necessária")]
        public string Password { get; set; }

    }
}


