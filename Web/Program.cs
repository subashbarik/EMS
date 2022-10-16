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


//Add Database services
builder.Services.AddDatabaseServices();
//Add Appplication services such as DI and Others
builder.Services.AddApplicationServices();
// CORS setting to allow Angular calls from client side
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
    });
});
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//call back methods on application start/stop/before stop
// app.Lifetime.ApplicationStarted.Register(OnStarted);
// app.Lifetime.ApplicationStopping.Register(OnStopping);
// app.Lifetime.ApplicationStopped.Register(OnStopped);
app.UseExceptionHandler("/api/error");
//app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//  We can replace the above two milleware with this UseFileServe middleware
// app.UseFileServe();
//app.UseHttpsRedirection();
// For using the default/index.htm file as startup file and it should be before UseStaticFiles middleware
// app.UseDefaultFiles();
// For serving static files from wwwroot folder
app.UseStaticFiles();
//apply CORS policy
app.UseCors("CorsPolicy");

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

