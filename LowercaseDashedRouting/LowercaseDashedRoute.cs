using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace LowercaseDashedRouting
{
	public class LowercaseDashedRoute : Route
	{
		public LowercaseDashedRoute(string url, IRouteHandler routeHandler)
			: this(url, null, null, null, routeHandler, null, null, null)
		{
		}

		public LowercaseDashedRoute(string url, IRouteHandler routeHandler, string[] namespaces)
			: this(url, null, null, null, routeHandler, null, null, namespaces)
		{
		}

		public LowercaseDashedRoute(string url, IRouteHandler routeHandler, AreaRegistration area, AreaRegistrationContext areaContext)
			: this(url, null, null, null, routeHandler, area, areaContext, null)
		{
		}

		public LowercaseDashedRoute(string url, IRouteHandler routeHandler, AreaRegistration area, AreaRegistrationContext areaContext, string[] namespaces)
			: this(url, null, null, null, routeHandler, area, areaContext, namespaces)
		{
		}

		public LowercaseDashedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
			: this(url, defaults, null, null, routeHandler, null, null, null)
		{
		}

		public LowercaseDashedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler, AreaRegistration area, AreaRegistrationContext areaContext)
			: this(url, defaults, null, null, routeHandler, area, areaContext, null)
		{
		}

		public LowercaseDashedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler, string[] namespaces)
			: this(url, defaults, null, null, routeHandler, null, null, namespaces)
		{
		}

		public LowercaseDashedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler, AreaRegistration area, AreaRegistrationContext areaContext, string[] namespaces)
			: this(url, defaults, null, null, routeHandler, area, areaContext, namespaces)
		{
		}

		public LowercaseDashedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
			: this(url, defaults, constraints, null, routeHandler, null, null, null)
		{
		}

		public LowercaseDashedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler, AreaRegistration area, AreaRegistrationContext areaContext)
			: this(url, defaults, constraints, null, routeHandler, area, areaContext, null)
		{
		}

		public LowercaseDashedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler, string[] namespaces)
			: this(url, defaults, constraints, null, routeHandler, null, null, namespaces)
		{
		}
		public LowercaseDashedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler, AreaRegistration area, AreaRegistrationContext areaContext, string[] namespaces)
			: this(url, defaults, constraints, null, routeHandler, area, areaContext, namespaces)
		{
		}

		public LowercaseDashedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
			: this(url, defaults, constraints, dataTokens, routeHandler, null, null, null)
		{
		}

		public LowercaseDashedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler, AreaRegistration area, AreaRegistrationContext areaContext)
			: this(url, defaults, constraints, dataTokens, routeHandler, area, areaContext, null)
		{
		}

		public LowercaseDashedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler, string[] namespaces)
			: this(url, defaults, constraints, dataTokens, routeHandler, null, null, namespaces)
		{
		}

		public LowercaseDashedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
			RouteValueDictionary dataTokens, IRouteHandler routeHandler, AreaRegistration area, AreaRegistrationContext areaContext, string[] namespaces)
			: base(url, defaults, constraints, dataTokens, routeHandler)
		{
			Url = url;
			Defaults = defaults;
			Constraints = constraints;
			DataTokens = dataTokens;
			RouteHandler = routeHandler;
			if (DataTokens == null)
				DataTokens = new RouteValueDictionary();
			if (area != null)
			{
				DataTokens["area"] = area.AreaName;
				if (namespaces == null || namespaces.Length == 0)
				{
					namespaces = areaContext.Namespaces.ToArray();
				}
				DataTokens["UseNamespaceFallback"] = (namespaces == null ? true : (int)namespaces.Length == 0);
			}
			if (namespaces != null && namespaces.Length > 0)
			{
				DataTokens["Namespaces"] = namespaces;
			}
		}

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			var action = values["action"] as string;
			var controller = values["controller"] as string;
			if (!string.IsNullOrEmpty(action))
			{
				values["action"] = AddDashesBeforeCapitals(action).ToLowerInvariant();
			}

			if (!string.IsNullOrEmpty(controller))
			{
				values["controller"] = AddDashesBeforeCapitals(controller).ToLowerInvariant();
			}
			return base.GetVirtualPath(requestContext, values);
		}

		/// <summary>
		/// Adds dashes before capital letters if the letter is not the first 
		/// and after a letter or digit (so do not add after /)
		/// </summary>
		/// <param name="text">text to add dashes before capitals</param>
		/// <returns>dashed text</returns>
		protected static string AddDashesBeforeCapitals(string text)
		{
			var newText = new StringBuilder(text.Length * 2);
			newText.Append(text[0]);

			for (int i = 1; i < text.Length; i++)
			{
				if (char.IsUpper(text[i]) && char.IsLetterOrDigit(text[i - 1]))
					newText.Append('-');

				newText.Append(text[i]);
			}
			return newText.ToString();
		}
	}
}
