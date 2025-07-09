using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace TicketingSystem.API.ActionFilters
{
    public sealed class RoleIdAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly int _roleId;

        public RoleIdAuthorizeAttribute(int roleId)
        {
            _roleId = roleId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var roleIdClaim = user.FindFirst(ClaimTypes.Role);

            if (roleIdClaim == null || !int.TryParse(roleIdClaim.Value, out int roleId) || roleId != _roleId)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}