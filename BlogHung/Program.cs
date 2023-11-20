using BlogHung;
using BlogHung.Application.BackgroudTaskService;
using BlogHung.Infrastructure.Extensions;
using BlogHung.Infrastructure.Hosting.Middlewares;
using EasyNetQ;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddControllersWithViews();


ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment env = builder.Environment;


builder.Services.AddInfrastructureLayer(configuration);
builder.Services.AddHttpServices();
builder.Services.AddScoped<LogModelDataAttribute>();
builder.Services.AddHttpClientServicesCallApi();

builder.Services.AddHostedService<MessageConsumer>();

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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<LoggingMiddleware>();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
