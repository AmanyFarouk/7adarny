using _7adarny.API.EndPoints.Students;
using _7adarny.Application.Contracts;
using _7adarny.Application.Contracts.Interfaces;
using _7adarny.Application.DependencyInjection;
using _7adarny.Application.Features.Students.Queries.GetGroupStudents;
using _7adarny.Infrastructure.Persistence;
using _7adarny.Infrastructure.Reposiotries;

namespace _7adarny.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped(typeof(IDbConnectionFactory), typeof(DbConnectionFactory));

            //        Services.AddScoped< IGetGroupStudentsHandlerContract<
            //               GetGroupStudentsHandlerInput,
            //               GetGroupStudentsHandlerOutput >,
            //GetGroupStudentsHandler > ();
            Services.AddAllHandlers();
            //Services.AddAutoMapper(typeof(GetGroupStudentsMapper).Assembly);
            //Services.AddAutoMapper(typeof(IBusinessHandler).Assembly);
            Services.AddAutoMapper(
                    typeof(Program).Assembly,           // API Project
                    typeof(IBusinessHandler).Assembly);  // Application Project

            return Services;
        }
    }
}
