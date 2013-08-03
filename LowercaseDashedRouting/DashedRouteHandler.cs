// by Ata Sasmaz http://www.ata.io

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LowercaseDashedRouting
{
    public class DashedRouteHandler : MvcRouteHandler
    {
        /// <summary>
        /// The route handler which ignores dashes.
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var values = requestContext.RouteData.Values;

            values["action"] = values["action"].ToString().Replace("-", "");
            values["controller"] = values["controller"].ToString().Replace("-", "");

            return base.GetHttpHandler(requestContext);
        }
    }
}