using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NavyAccountCore.Core.AuditService
{
    public class AuthorizeActionFilterAttribute : ActionFilterAttribute
    {
       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;

            if (controller != null)
            {
                if (session != null && session["authstatus"] == null)
                {
                    filterContext.Result =
                           new RedirectToRouteResult(
                               new RouteValueDictionary{{ "controller", "Login" },
                                          { "action", "Index" }

                                                             });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}