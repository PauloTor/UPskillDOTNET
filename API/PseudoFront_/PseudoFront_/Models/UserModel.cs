using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoFront_.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Nif { get; set; }
        public float Credito { get; set; }
        public override string Email { get; set; }
        public string MetodoPagamento { get; set; }
        public int PaymentMethodId { get; set; }
    }
}