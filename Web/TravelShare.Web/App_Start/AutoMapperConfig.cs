using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using TravelShare.Web.Mappings;

namespace TravelShare.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void Config(params Assembly[] assemblies)
        {
            Mapper.Initialize(c => RegisterMappings(c, assemblies));
        }

        public static void RegisterMappings(IMapperConfigurationExpression configExpr, params Assembly[] assemblies)
        {
            configExpr.ConstructServicesUsing(t => DependencyResolver.Current.GetService(t));

            var types = new List<Type>();
            foreach (var assembly in assemblies)
            {
                types.AddRange(assembly.GetExportedTypes());
            }

            LoadStandardMappings(configExpr, types);
            LoadCustomMappings(configExpr, types);
        }

        private static void LoadStandardMappings(IMapperConfigurationExpression configExpr, IEnumerable<Type> types)
        {
            var maps = types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
                .Where(
                    type =>
                        type.i.IsGenericType && type.i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                        !type.t.IsAbstract
                        && !type.t.IsInterface)
                .Select(type => new { Source = type.i.GetGenericArguments()[0], Destination = type.t });

            foreach (var map in maps)
            {
                configExpr.CreateMap(map.Source, map.Destination);
                configExpr.CreateMap(map.Destination, map.Source);
            }
        }

        private static void LoadCustomMappings(IMapperConfigurationExpression configExpr, IEnumerable<Type> types)
        {
            var maps =
                types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
                    .Where(
                        type =>
                            typeof(IHaveCustomMappings).IsAssignableFrom(type.t) && !type.t.IsAbstract &&
                            !type.t.IsInterface)
                    .Select(type => (IHaveCustomMappings)Activator.CreateInstance(type.t));

            foreach (var map in maps)
            {
                map.CreateMappings(configExpr);
            }
        }
    }
}