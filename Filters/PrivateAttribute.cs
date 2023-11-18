using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcosystemApp.Filters
{
    public class PrivateAttribute : ActionFilterAttribute
    {
        public string? Role { get; set; }
        public string? TokenJWT { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userName = context.HttpContext.Session.GetString("username");
            var password = context.HttpContext.Session.GetString("password");
            var userRole = context.HttpContext.Session.GetString("role");
            var token = context.HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(token))
            {
                if (string.IsNullOrEmpty(Role) || string.IsNullOrEmpty(TokenJWT)) base.OnActionExecuting(context);
                else if (Role.Contains(userRole)) base.OnActionExecuting(context);
                else context.Result = new RedirectToActionResult("Unauthorized", "Home", null);
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
        }
    }
}
