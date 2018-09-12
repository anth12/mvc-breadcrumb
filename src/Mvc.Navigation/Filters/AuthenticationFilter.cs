using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Mvc.Navigation.Filters
{
    public class AuthenticationFilter : INavigationFilter
    {
        private IHttpContextAccessor _httpContext;

        public AuthenticationFilter(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public bool IsNodeHidden(TreeElement element)
        {
            var user = _httpContext.HttpContext.User;

            if (element.AuthenticatedOnly && !user.Identity.IsAuthenticated)
                return true;

            if (element.AnonymousOnly && user.Identity.IsAuthenticated)
                return true;

            if (element.RequiredRoles?.Length > 0)
            {
                if (element.RequiredRoles.All(r => !user.IsInRole(r)))
                {
                    // User not in any eligible role
                    return true;
                }
            }

            return false;
        }
    }
}
