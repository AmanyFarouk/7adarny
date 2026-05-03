using MediatR;

namespace _7adarny.API.Extensions
{
    public static class MediatRServicesExtension
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection Services)
        {

            Services.AddMediatR(typeof(Application.Common.AssemblyReference).Assembly);
            return Services;
        }
    }
}
