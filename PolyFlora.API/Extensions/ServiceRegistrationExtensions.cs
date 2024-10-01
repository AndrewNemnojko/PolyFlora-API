

using Microsoft.OpenApi.Models;
using PolyFlora.Application.Interfaces.Auth;
using PolyFlora.Application.Interfaces.Repositories;
using PolyFlora.Application.Interfaces.Utilites;
using PolyFlora.Application.Services.Domain;
using PolyFlora.Application.Services.Utilites;
using PolyFlora.Core.Interfaces;
using PolyFlora.Infrastructure;
using PolyFlora.Infrastructure.Security;
using PolyFlora.Persistence.Repositories;

namespace PolyFlora.API.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {            
            //Repository Services
            services.AddScoped<IFlowerRepository, FlowerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Utilites Services
            services.AddScoped<ICacheService, RedisCacheService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<TransliterationService>();
            services.AddScoped<IImageService, ImageService>();

            //Domain Services
            services.AddScoped<FlowerService>();
            services.AddScoped<AuthService>();
            

            //Swagger exts
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PolyFlora API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below.",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }
    }
}
