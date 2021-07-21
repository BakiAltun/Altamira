using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Vimo.Infrastructure.ApiDocumentation
{
    public static class ApiDocumentationExtensions
    {
        public static void AddApiDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(swagger =>
                        {
                            swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
                            swagger.CustomSchemaIds(type => type.ToString());

                            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                            {
                                Name = "Authorization",
                                Type = SecuritySchemeType.ApiKey,
                                Scheme = "Bearer",
                                BearerFormat = "JWT",
                                In = ParameterLocation.Header,
                                Description = "Örnek: \"Bearer 12345abcdef\"",
                            });

                            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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

        public static void UseApiDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Api v1"));
        }
    }
}

