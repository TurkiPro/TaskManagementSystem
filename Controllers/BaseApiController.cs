using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskManagementSystem.Controllers
{
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Get login user primary Id
        /// </summary>
        protected int UserID => int.Parse(FindClaim(ClaimTypes.NameIdentifier));
        /// <summary>
        /// Get claim by name
        /// </summary>
        /// <param name="claimName"></param>
        /// <returns></returns>
        private string FindClaim(string claimName)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(claimName);

            if (claim == null)
            {
                return null;
            }

            return claim.Value;
        }
    }
}
