using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PaqueAPICentral.Authentication
{
    public class ClaimsRequirement : IAuthorizationRequirement
    {
        public ClaimsRequirement(params string[] claims)
        {
            Claims = claims;
        }

        public string[] Claims { get; set; }
    }
}
