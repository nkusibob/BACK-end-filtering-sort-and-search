using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class CustomClaimsPrincipalFactory :
      UserClaimsPrincipalFactory<ApplicationUser,
      IdentityRole>
    {
        public CustomClaimsPrincipalFactory(
       UserManager<ApplicationUser> userManager
       , RoleManager<IdentityRole> roleManager
       , IOptions<IdentityOptions> optionsAccessor)
   : base(userManager, roleManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
        new Claim(ClaimTypes.Email, user.Email)
    });
            }

            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
         new Claim(ClaimTypes.Email, user.PasswordHash),
    });
            }

            return principal;
        }
    }
}