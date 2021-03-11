using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoFront_.Models
{
    public class LoginModel
    {
        
            [Required(ErrorMessage = "O Email é necessário")]
            public string Email { get; set; }

            [Required(ErrorMessage = "A Password é necessária")]
            public string Password { get; set; }
        
    }
}
