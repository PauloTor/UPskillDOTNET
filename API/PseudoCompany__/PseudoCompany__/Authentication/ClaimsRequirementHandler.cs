using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PaqueAPICentral.Authentication
{
    public class ClaimsRequirementHandler : AuthorizationHandler<ClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsRequirement requirement)
        {

            if (requirement.Claims.Any(c => context.User.HasClaim(uc => uc.Type == c)))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
