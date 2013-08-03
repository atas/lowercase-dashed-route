// by Ata Sasmaz http://www.ata.io

using System.Text;
using System.Web.Routing;

namespace LowercaseDashedRouting
{
    public class LowercaseDashedRoute : Route
    {
        public LowercaseDashedRoute (string url, IRouteHandler routeHandler)
			: this (url, null, routeHandler)
		{
		}

		public LowercaseDashedRoute (string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
			: this (url, defaults, null, routeHandler)
		{
		}

		public LowercaseDashedRoute (string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
			: this (url, defaults, constraints, null, routeHandler)
		{
		}

        public LowercaseDashedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler) : base(url, defaults, constraints, dataTokens, routeHandler)
		{
			Url = url;
			Defaults = defaults;
			Constraints = constraints;
			DataTokens = dataTokens;
			RouteHandler = routeHandler;
		}

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData path = base.GetVirtualPath(requestContext, values);

            if (path != null)
            {
                string virtualPath = path.VirtualPath;
                var lastIndexOf = virtualPath.LastIndexOf("?", System.StringComparison.InvariantCulture);

                if (lastIndexOf != 0)
                {
                    if (lastIndexOf > 0)
                    {
                        string leftPart = AddDashesBeforeCapitals(virtualPath.Substring(0, lastIndexOf)).ToLowerInvariant();
                        string queryPart = virtualPath.Substring(lastIndexOf);
                        path.VirtualPath = leftPart + queryPart;
                    }
                    else
                    {
                        path.VirtualPath = AddDashesBeforeCapitals(path.VirtualPath).ToLowerInvariant();
                    }
                }
            }

            return path;
        }

        /// <summary>
        /// Adds dashes before capital letters if the letter is not the first 
        /// and after a letter or digit (so do not add after /)
        /// </summary>
        /// <param name="text">text to add dashes before capitals</param>
        /// <returns>dashed text</returns>
        protected static string AddDashesBeforeCapitals(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);

            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && char.IsLetterOrDigit(text[i-1]))
                    newText.Append('-');

                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}