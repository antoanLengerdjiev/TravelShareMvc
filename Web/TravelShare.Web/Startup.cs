﻿using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(TravelShare.Web.Startup))]

namespace TravelShare.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}