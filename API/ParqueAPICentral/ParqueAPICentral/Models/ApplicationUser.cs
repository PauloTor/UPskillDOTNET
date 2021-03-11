using ParqueAPICentral.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public float Credito { get; set; }
        public string MetodoPagamento { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
