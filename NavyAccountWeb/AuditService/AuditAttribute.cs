using ChasteSchoolDAL;
using ChasteSchoolDomain.AuditModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ChasteSchool.Repository.Interface;
using ChasteSchool.Repository.InterfaceImplementation;

namespace NavyAccountCore.Core.AuditService
{
    public class AuditAttribute : ActionFilterAttribute
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        private AuditTrailInt auditInterface;
        public AuditAttribute(AuditAction activity)

        {

            //AllowMultiple = false;
            auditInterface = new AuditTrailImpl();
            Activity = activity;

        }
        public AuditAction Activity { get; set; }



        public override void OnActionExecuting(ActionExecutingContext filterContext)

        {

            var request = filterContext.HttpContext.Request;

            AuditTrail au = new AuditTrail()
            {
                AuditId = Guid.NewGuid(),
                SessionID = "NA",

                IpAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? (request.UserHostAddress == "::1" ? "127.0.0.1" : request.UserHostAddress),

                ActivityTime = DateTime.Now,

                Browser = request.Browser.Type,

                UrlAccessed = request.RawUrl,

                UserId = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",

                Action = Enum.GetName(typeof(AuditAction), Activity),

                Data = SerializeRequest(request)

            };

            auditInterface.addtrail(au);
            //finish executing the action as normal

            base.OnActionExecuting(filterContext);

        }

        private string SerializeRequest(HttpRequestBase request)

        {

            try

            {

                XDocument doc = new XDocument();

                doc.Declaration = new XDeclaration("1.0", "utf-16", "yes");

                doc.Add(new XElement("request"));



                var items = request.Form.AllKeys.SelectMany(request.Params.GetValues, (k, v) => new { key = k, value = v });

                foreach (var item in items)

                {

                    doc.Root.Add(new XElement(item.key.Replace(" ", "").Replace(":", ""), item.value));

                }



                items = request.QueryString.AllKeys.SelectMany(request.Params.GetValues, (k, v) => new { key = k, value = v });

                foreach (var item in items)

                {

                    doc.Root.Add(new XElement(item.key.Replace(" ", ""), item.value));

                }



                if (doc.Root.HasElements)

                    return doc.ToString(SaveOptions.DisableFormatting);

                else

                    return null;

            }

            catch (Exception ex)

            {
                _logger.Debug(ex);
                return null;

            }

        }
    }
}