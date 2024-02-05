using Api.Temeprature.Business.Service;
using Api.Temeprature.Business.Service.Contract;
using Api.Temperature.Data.Context;
using Api.Temperature.Data.Context.Contract;
using Api.Temperature.Data.Repository;
using Api.Temperature.Data.Repository.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.Temeprature.IoC.Application
{
    public static class IoCApplication
    {
        /// <summary>
        /// Configuration de l'injection des repository du Web API RestFul
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureInjectionDependencyRepository(this IServiceCollection services)
        {
            // Injections des Dépendances
            // - Repositories

            services.AddScoped<IUniteRepository, UniteRepository>();
            services.AddScoped<IDepartementRepository, DepartementRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            return services;
        }


        /// <summary>
        /// Configure l'injection des services
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureInjectionDependencyService(this IServiceCollection services)
        {
            // Injections des Dépendances
            // - Service

            services.AddScoped<IUniteService, UniteService > ();
            services.AddScoped<IDepartementService, DepartementService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

        /// <summary>
        /// Configuration de la connexion de la base de données
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BddConnection");

            services
                .AddDbContext<IMeteoFranceDBContext, MeteoFranceDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors())
                .AddDbContext<IdentityDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            return services;
;
        }

        /// <summary>
        /// Configure Identity
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<IdentityDBContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });
        }


    }
}
