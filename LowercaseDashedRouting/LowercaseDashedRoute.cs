using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

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
			var originalAction = values["action"] as string;
			var originalController = values["controller"] as string;
			string dashedAction = originalAction, dashedController = originalController;

			// FIX: for when the 'action' is not mentioned and the default value which is stored in the RouteData is about to be used!
			var currentValues = requestContext.RouteData.Values;
			object current;

			// Dashing the 'Action'
			if (originalAction != null)
			{
				values["action"] = dashedAction = AddDashesBeforeCapitals(originalAction).ToLowerInvariant();

				if (currentValues.TryGetValue("action", out current) &&
					(current as string) == originalAction)
				{
					currentValues["action"] = dashedAction;
				}
			}
			else
			{
				// Applying dash to route values
				if (currentValues.TryGetValue("action", out current))
				{
					currentValues["action"] = AddDashesBeforeCapitals(current as string).ToLowerInvariant();
				}
			}

			// Aash the 'Controller'
			if (originalController != null)
			{
				values["controller"] = dashedController = AddDashesBeforeCapitals(originalController).ToLowerInvariant();

				if (currentValues.TryGetValue("controller", out current) &&
					(current as string) == originalController)
				{
					currentValues["controller"] = dashedController;
				}
			}
			else
			{
				// Applying dash to route values
				if (currentValues.TryGetValue("controller", out current))
				{
					currentValues["controller"] = AddDashesBeforeCapitals(current as string).ToLowerInvariant();
				}
			}



			if (DataTokens["LowercaseDashedRoute"] == null)
			{
				Url = AddDashesForMatchingUrl(Url);
				DataTokens["LowercaseDashedRoute"] = true;
			}

			var virtualUrl = base.GetVirtualPath(requestContext, values);
			return virtualUrl;
		}

		/// <summary>
		/// Adds dashes before capital letters if the letter is not the first 
		/// and after a letter or digit (so do not add after /)
		/// </summary>
		/// <param name="text">text to add dashes before capitals</param>
		/// <returns>dashed text</returns>
		protected static string AddDashesBeforeCapitals(string text)
		{
			// bugfix for using @Url.RouteUrl
			if (text == null)
				return null;

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

		protected static string AddDashesForMatchingUrl(string url)
		{
			var newText = new StringBuilder(url.Length * 2);
			newText.Append(char.ToLower(url[0]));

			bool skip = false;
			for (int i = 1; i < url.Length; i++)
			{
				var c = url[i];
				if (c == '{')
				{
					skip = true;
					newText.Append(c);
					continue;
				}
				else if (c == '}')
				{
					skip = false;
					newText.Append(c);
					continue;
				}
				if (skip)
				{
					newText.Append(c);
					continue;
				}

				if (char.IsUpper(c) && char.IsLetterOrDigit(url[i - 1]))
					newText.Append('-');

				newText.Append(char.ToLower(c));
			}
			return newText.ToString();
		}
	}
}
