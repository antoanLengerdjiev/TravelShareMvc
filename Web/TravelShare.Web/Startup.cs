using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;

using Owin;
using TravelShare.Data;
using TravelShare.Data.Common;
using TravelShare.Data.Common.Contracts;
using TravelShare.Data.Models;
using TravelShare.Services.Data;
using TravelShare.Services.Data.Common.Contracts;

[assembly: OwinStartupAttribute(typeof(TravelShare.Web.Startup))]

namespace TravelShare.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            this.RegisterSignalRHubs(app);
        }

        private void RegisterSignalRHubs(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // STANDARD SIGNALR SETUP:

            // Get your HubConfiguration. In OWIN, you'll create one
            // rather than using GlobalHost.
            var config = new HubConfiguration();

            // Register your SignalR hubs.
            builder.RegisterHubs(Assembly.GetExecutingAssembly());

            builder.Register(x => new ApplicationDbContext())
                .As<IApplicationDbContext>()
                .As<IApplicationDbContextSaveChanges>()
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfDbRepository<>))
               .As(typeof(IEfDbRepository<>))
               .InstancePerLifetimeScope();
            builder.Register(x => new TripService(x.Resolve<IEfDbRepository<Trip>>(), x.Resolve<IApplicationDbContextSaveChanges>(), x.Resolve<IEfDbRepository<ApplicationUser>>()))
                .As<ITripService>()
                .InstancePerLifetimeScope();

            builder.Register(x => new MessageService(x.Resolve<IEfDbRepository<Message>>(), x.Resolve<IApplicationDbContextSaveChanges>())).As<IMessageService>()
                .InstancePerLifetimeScope();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.Resolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            // OWIN SIGNALR SETUP:

            // Register the Autofac middleware FIRST, then the standard SignalR middleware.
            app.UseAutofacMiddleware(container);
            app.MapSignalR("/signalr", config);
        }
    }
}
