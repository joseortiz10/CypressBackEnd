using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace apsys.project.web.Infraestructure
{
    public static class IPrincipalExtender
    {
        public static string GetUserId(this IPrincipal principal)
        {
            ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
            var subClaim = identity.FindFirst("sub");
            if(subClaim !=null){
                return subClaim.Value;
            }
            else
            {
                return string.Empty;
            }
        }

        public static IEnumerable<Claim> GetClaims(this IPrincipal principal)
        {
            ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
            return identity.Claims;
        }
    }
}