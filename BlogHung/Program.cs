using BlogHung;
using BlogHung.Application.BackgroudTaskService;
using BlogHung.Application.OrderProcess;
using BlogHung.Infrastructure.Extensions;
using BlogHung.Infrastructure.Hosting.Middlewares;
using BlogHung.Infrastructure.Kafka;
using BlogHung.Infrastructure.Kafka.Consumer;
using BlogHung.Infrastructure.Kafka.Producer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddControllersWithViews();


ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment env = builder.Environment;

Console.WriteLine("Start app...");

builder.Services.AddInfrastructureLayer(configuration);
builder.Services.AddHttpServices();
builder.Services.AddScoped<LogModelDataAttribute>();
builder.Services.AddHttpClientServicesCallApi();

builder.Services.AddSingleton<KafkaConsumerManager>();
builder.Services.AddHostedService<MessageConsumer>();
builder.Services.AddSingleton<IOrderProcess, OrderProcess>();
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();


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
Console.WriteLine("Done start a...");
app.Run();
