lowercase-dashed-routing
========================

lowercase-dashed/hyphenated-routing-for-asp-net-mvc/like-this

[NuGet Package](https://www.nuget.org/packages/LowercaseDashedRoute/).
Install with NuGet: `PM> Install-Package LowercaseDashedRoute`


<h3>Example</h3>
<strong>If you use:</strong>
Html.ActionLink("User Profile", "UserProfile", "Account")

**Without** Lowercase Dashed Route the url is:<br />
`yoursite.com/Account/UserProfile`

**<u>With</u>** Lowercase Dashed Route the url is:<br />
`yoursite.com/account/user-profile`

<h3>Initial Configuration</h3>
Open RouteConfig.cs in App_Start folder. Comment out old routes.MapRoute(...) call and instead use:

```c#
routes.Add(new LowercaseDashedRoute("{controller}/{action}/{id}",
  new RouteValueDictionary(
    new { controller = "Home", action = "Index", id = UrlParameter.Optional }),
    new DashedRouteHandler()
  )
);
```

Developers: <br/>
[Ata Sasmaz](http://www.ata.io)
