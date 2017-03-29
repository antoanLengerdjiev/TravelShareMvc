namespace TravelShare.Web
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;

    using Controllers;

    using Data;
    using Data.Common;
    using Data.Common.Contracts;
    using Data.Models;
    using Services.Data;
    using Services.Data.Common.Contracts;
    using Services.Web;

    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.Register(x => new ApplicationDbContext())
                .As<IApplicationDbContext>()
                .InstancePerRequest();
            builder.Register(x => new HttpCacheService())
                .As<ICacheService>()
                .InstancePerRequest();
            builder.Register(x => new IdentifierProvider())
                .As<IIdentifierProvider>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(EfDbRepository<>))
                .As(typeof(IEfDbRepository<>))
                .InstancePerRequest();

            builder.Register(x => new TripService(x.Resolve<IEfDbRepository<Trip>>()))
                .As<ITripService>()
                .InstancePerRequest();

            builder.Register(x => new UserService(x.Resolve<IEfDbRepository<ApplicationUser>>()))
                .As<IUserService>()
                .InstancePerRequest();

            builder.Register(x => new ApplicationData(x.Resolve<IApplicationDbContext>(), x.Resolve<IEfDbRepository<News>>(), x.Resolve<IEfDbRepository<ApplicationUser>>(), x.Resolve<IEfDbRepository<Trip>>(), x.Resolve<IEfDbRepository<Rating>>()))
           .As<IApplicationData>()
           .InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<BaseController>().PropertiesAutowired();
        }
    }
}
