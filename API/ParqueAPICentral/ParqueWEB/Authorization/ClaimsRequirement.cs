using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Authorization
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
