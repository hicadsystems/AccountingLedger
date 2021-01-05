using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace NavyAccountCore.Core.AuditService
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var getSessionattribute = filterContext.HttpContext.Session.GetString("fundtypedescription");
            if (getSessionattribute == null)
            {
                filterContext.Result = new RedirectResult("~/Authentication/Logout");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}