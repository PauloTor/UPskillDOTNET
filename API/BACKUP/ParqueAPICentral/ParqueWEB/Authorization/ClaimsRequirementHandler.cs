using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Authorization
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
