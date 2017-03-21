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
            //builder.Register(x => new ApplicationDbContext())
            //    .As<DbContext>()
            //    .InstancePerRequest();
            builder.Register(x => new ApplicationDbContext())
                .As<IApplicationDbContext>()
                .InstancePerRequest();
            builder.Register(x => new HttpCacheService())
                .As<ICacheService>()
                .InstancePerRequest();
            builder.Register(x => new IdentifierProvider())
                .As<IIdentifierProvider>()
                .InstancePerRequest();
            builder.Register(x => new TripRepository(x.Resolve<IApplicationDbContext>()))
                .As<ITripRepository>()
                .InstancePerRequest();

            builder.Register(x => new NewsRepository(x.Resolve<IApplicationDbContext>()))
                .As<INewsRepository>()
                .InstancePerRequest();

            builder.Register(x => new UserRepository(x.Resolve<IApplicationDbContext>()))
                .As<IApplicationUserRepository>()
                .InstancePerRequest();

            builder.Register(x => new RatingRepository(x.Resolve<IApplicationDbContext>()))
                .As<IRatingsRepository>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(DbRepository<>))
                .As(typeof(IDbRepository<>))
                .InstancePerRequest();

            builder.Register(x => new ApplicationData(x.Resolve<IApplicationDbContext>(), x.Resolve<IDbRepository<News>>(), x.Resolve<IDbRepository<ApplicationUser>>(), x.Resolve<IDbRepository<Trip>>(), x.Resolve<IDbRepository<Rating>>()))
           .As<IApplicationData>()
           .InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<BaseController>().PropertiesAutowired();
        }
    }
}
