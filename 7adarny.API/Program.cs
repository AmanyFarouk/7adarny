using _7adarny.API.Extensions;
using _7adarny.Application.Contracts.Interfaces;
using _7adarny.Infrastructure.Data;
using _7adarny.Infrastructure.Reposiotries;
using _7adarny.Infrastructure.Services;
using EduEnroll.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

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
            //builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.TagActionsBy(api =>
                {
                    var tags = api.ActionDescriptor.EndpointMetadata
                        .OfType<SwaggerOperationAttribute>()
                        .SelectMany(a => a.Tags)
                        .ToList();
                    if (tags.Count > 0)
                        return tags;
                    return new List<string> { "Default" };
                });
            });

            //add dbcontext
            builder.Services.AddDatabase(builder.Configuration);
            //register mediatR                 
            builder.Services.AddMediatRServices();
            //register Dependency Injection
            builder.Services.AddApplicationServices();

            builder.Services.AddScoped<IOtpService, TwilioOtpService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            
            builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
                };
            });

            builder.Services.AddAuthorization();
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

                //Seed data
                await DataSeeder.SeedAsync(DbContext);
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
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "7adarny API V1");
                   options.RoutePrefix = string.Empty;
                }); 
                //app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
