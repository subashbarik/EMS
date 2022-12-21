using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Web.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services, Assembly presentationAssembly)
        {
            // API versioning configuration
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver"));
            });
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
            // End API versioning 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Description = "Contains API Infomation for Employee Management System version1",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Codeventurer Pvt. Ltd.",
                        Email = "subash.barik@gmail.com",
                        Url = new Uri("https://example.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under OpenApiLicense",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Description = "Contains API Infomation for Employee Management System version2",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Codeventurer Pvt. Ltd.",
                        Email = "barik.subash@gmail.com",
                        Url = new Uri("https://example.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under OpenApiLicense",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                //Configuration needed for the swagger to accept JWT token in the Swagger UI
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                   
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                 var securityRequirement = new OpenApiSecurityRequirement{{securitySchema, new[] {"Bearer"}}};
                c.AddSecurityRequirement(securityRequirement);

                /*
                    // Basic authentication
                    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme  
                            {  
                                Name = "Authorization",  
                                Type = SecuritySchemeType.Http,  
                                Scheme = "basic",  
                                In = ParameterLocation.Header,  
                                Description = "Basic Authorization header using the Bearer scheme."  
                            });  
                */

                // Sets up global security requirement - not need for now may be needed later
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {

                //            Reference = new OpenApiReference
                //            {
                //                Type=ReferenceType.SecurityScheme,
                //                Id="Bearer"
                //            }
                //        },new string[]{}
                //    }
                //});
                /*
                // Basic security requirement
                c.AddSecurityRequirement(new OpenApiSecurityRequirement  
                            {  
                                {  
                                      new OpenApiSecurityScheme  
                                        {  
                                            Reference = new OpenApiReference  
                                            {  
                                                Type = ReferenceType.SecurityScheme,  
                                                Id = "basic"  
                                            }  
                                        },  
                                        new string[] {}  
                                }  
                            }); 
                */
                // Set the comments path for the Swagger JSON and UI.    
                var xmlFile = $"{presentationAssembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}
