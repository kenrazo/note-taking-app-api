using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NoteTakingApi.Common.Models;
using NoteTakingApi.DataAccess.DataContexts;
using NoteTakingApi.DataAccess.Repositories;
using NoteTakingApi.Providers;
using System;
using System.Text;

namespace NoteTakingApi.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection InjectDbContext(this IServiceCollection services)
        {
            return services.AddDbContext<NoteTakingDbContext>(options => options.UseInMemoryDatabase(databaseName: "NoteTaking"));
        }

        public static IServiceCollection InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();

            return services;
        }

        public static IServiceCollection InjectAuthService(this IServiceCollection services)
        {
            services.AddScoped<IJwtOptionsProvider, JwtOptionsProvider>();
            services.AddScoped<Service.Services.IUserAuthService, Service.Services.UserAuthService>();
            services.AddScoped<Service.Services.INoteService, Service.Services.NoteService>();

            return services;
        }

        public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            var jwtOptionsProvider = services.BuildServiceProvider().GetService<IJwtOptionsProvider>();

            var jwtSettings = jwtOptionsProvider.GetJwtOptions();

            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            /* TODO if we will implement policy based authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("email", policy => policy.RequireClaim("scope", "email"));
            });
            */

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    LifetimeValidator = ValidateTokenLifeTime
                };
            });
        }

        internal static bool ValidateTokenLifeTime(DateTime? notBefore, DateTime? expires,
            SecurityToken token, TokenValidationParameters @params)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }

            return false;
        }
    }
}
