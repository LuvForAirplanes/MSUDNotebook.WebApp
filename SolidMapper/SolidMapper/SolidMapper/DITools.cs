using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace SolidMapper
{
    public static class IServiceCollectionExtensions
    {
        public static void AddSolidMapper(this IServiceCollection services, Assembly assembly)
        {
            AddSolidMapper(services, new Assembly[] { assembly });
        }

        public static void AddSolidMapper(this IServiceCollection services, Assembly[] assemblies)
        {
            var mappers = assemblies.SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Any(i => i.Namespace == "SolidMapper"))).ToList();

            foreach (var mapper in mappers)
            {
                services.AddScoped(Type.GetType(mapper.AssemblyQualifiedName));
            }
        }
    }
}