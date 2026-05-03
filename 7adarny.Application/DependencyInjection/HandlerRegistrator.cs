using _7adarny.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.DependencyInjection
{
    public static class HandlerRegistrator
    {
        public static IServiceCollection AddAllHandlers(
        this IServiceCollection services)
        {
            var assembly = typeof(IBusinessHandler).Assembly;

            var allTypes = assembly.GetTypes();

            var classTypes = allTypes
                .Where(t => t.IsClass && !t.IsAbstract);

            var handlerTypes = classTypes
                .Where(t =>
                    t.GetInterfaces()
                     .Any(i =>
                        i.IsGenericType &&
                        typeof(IBusinessHandler).IsAssignableFrom(i)
                     )
                );

            foreach (var handlerType in handlerTypes)
            {
                var interfaceType = handlerType
                    .GetInterfaces()
                    .First(i =>
                        i.IsGenericType &&
                        typeof(IBusinessHandler).IsAssignableFrom(i) &&
                        i != typeof(IBusinessHandler)
                    );

                services.AddScoped(interfaceType, handlerType);
            }

            return services;
        }
    }

}
