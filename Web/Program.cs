using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Extensions.Logging;
using Web;
using Web.Extensions;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Using presentation layer as the service provider for controllers
var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
//applying error handling filter to catch errors in place of middleware
// builder.Services.AddControllers(options => options.Filters.Add<ExceptionHandlingFilterAttribute>())
//                 .AddApplicationPart(presentationAssembly);


var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()
                    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
//Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
//builder.Host.UseSerilog(((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)));


builder.Services.AddControllers()
                .AddApplicationPart(presentationAssembly);


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
// Configures Swagger
builder.Services.AddSwaggerServices(presentationAssembly);

var app = builder.Build();

//call back methods on application start/stop/before stop
// app.Lifetime.ApplicationStarted.Register(OnStarted);
// app.Lifetime.ApplicationStopping.Register(OnStopping);
// app.Lifetime.ApplicationStopped.Register(OnStopped);
app.UseExceptionHandler("/api/error/appexception");
//app.UseSerilogRequestLogging();

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
// If endpoint for a api request not found then we need 
// below to result proper error
app.UseStatusCodePagesWithReExecute("/api/error/apiendpointnotfound/{0}");


app.MapGet("/deployment",() => System.Diagnostics.Process.GetCurrentProcess().ProcessName);
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


