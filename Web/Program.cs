using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Web;
using Web.Extensions;
using Web.Filters;
using Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Using presentation layer as the service provider for controllers
var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
//applying error handling filter to catch errors in place of middleware
// builder.Services.AddControllers(options => options.Filters.Add<ExceptionHandlingFilterAttribute>())
//                 .AddApplicationPart(presentationAssembly);

builder.Services.AddControllers()
                .AddApplicationPart(presentationAssembly);

// API versioning configuration
builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});
builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
// End API versioning 
//Add Appplication services such as DI and Others
builder.Services.AddApplicationServices();
//Add Database services
builder.Services.AddDatabaseServices();

// CORS setting to allow Angular calls from client side
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200","http://localhost:4300");
    });
});
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Description = "Contains API Infomation for Employee Management System version1",
        TermsOfService =  new Uri("https://example.com/terms"),
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
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

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

var app = builder.Build();

//call back methods on application start/stop/before stop
// app.Lifetime.ApplicationStarted.Register(OnStarted);
// app.Lifetime.ApplicationStopping.Register(OnStopping);
// app.Lifetime.ApplicationStopped.Register(OnStopped);
app.UseExceptionHandler("/api/error");
//app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EMS Api v1");
//        c.SwaggerEndpoint("/swagger/v2/swagger.json", "EMS Api v2");
//    });
//}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EMS Api v1");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "EMS Api v2");
    // When we deploy the API , swagger page will be show by default 
    c.RoutePrefix = string.Empty;
});



//  We can replace the above two milleware with this UseFileServe middleware
// app.UseFileServe();
//app.UseHttpsRedirection();
// For using the default/index.htm file as startup file and it should be before UseStaticFiles middleware
// app.UseDefaultFiles();
// For serving static files from wwwroot folder
app.UseStaticFiles();
//apply CORS policy
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//Create database and seed data
await app.UseEMSDbSetupAsync(); 
app.Run();

// void OnStarted()
// {
//     Console.WriteLine("EMS Started");
// }
// void OnStopping()
// {
// Console.WriteLine("EMS Stopping");
// }
// void OnStopped()
// {
// Console.WriteLine("EMS Stopped");
// }

