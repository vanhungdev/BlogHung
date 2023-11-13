using BlogHung;
using BlogHung.Infrastructure.Extensions;
using BlogHung.Infrastructure.Hosting.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Host.UseSerilog();


ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment env = builder.Environment;

builder.Services.AddHttpServices();
builder.Services.AddScoped<LogModelDataAttribute>();
builder.Services.AddInfrastructureLayer(configuration);
builder.Services.AddHttpClientServicesCallApi();

builder.Services.AddCors(options =>
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());
    });
});


var app = builder.Build();
app.AddCoreInfrastructureLayer(env);
app.AddInfrastructureLayer(env);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.AddCoreInfrastructureLayer(env);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<LoggingMiddleware>();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
