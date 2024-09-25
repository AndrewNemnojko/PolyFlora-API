

using Microsoft.OpenApi.Models;
using PolyFlora.Application.Interfaces.Auth;
using PolyFlora.Application.Interfaces.Repositories;
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
            services.AddScoped<IFlowerRepository, FlowerRepository>();
            services.AddScoped<ICacheService, RedisCacheService>(); 
            services.AddScoped<IJwtProvider, JwtProvider>();

            //Swagger exts
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JWT Auth Sample",
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
