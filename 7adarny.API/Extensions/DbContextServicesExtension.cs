using _7adarny.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EduEnroll.API.Extensions
{
    public static class DbContextServicesExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection Services , IConfiguration Configuration)
        {
            Services.AddDbContext<Context>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                    .EnableSensitiveDataLogging();
            });

            return Services;
        }
    }
}
