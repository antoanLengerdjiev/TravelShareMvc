﻿namespace TravelShare.Web
{
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using App_Start;
    using Forloop.HtmlHelpers;

#pragma warning disable SA1649 // File name must match first type name

    public class MvcApplication : HttpApplication
#pragma warning restore SA1649 // File name must match first type name
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            DatabaseConfig.Config();
            AutofacConfig.RegisterAutofac();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ScriptContext.ScriptPathResolver = System.Web.Optimization.Scripts.Render;
            AutoMapperConfig.Config(Assembly.GetExecutingAssembly());
        }
    }
}
