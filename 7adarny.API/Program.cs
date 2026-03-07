using _7adarny.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace _7adarny.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            #region Update-Database
            using var Scope = app.Services.CreateScope();
            //Group of serivces lifeTime Scoped
            var Services = Scope.ServiceProvider;
            //services its self
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var DbContext = Services.GetRequiredService<Context>();
                //ASK CLR For Creating Object From DbContext Explicitly
                await DbContext.Database.MigrateAsync();//update-database
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Occured During Appling The Migration");
            }
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "7adarny API V1");
                }); 
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
