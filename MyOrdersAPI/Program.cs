using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MMyOrdersAPI.Manager.IManagers;
using MyOrdersAPI.DataService;
using MyOrdersAPI.Manager;
using MyOrdersAPI.Manager.IManagers;
using MyOrdersAPI.Manager.Managers;
using MyOrdersAPI.Repositories;
using MyOrdersAPI.Repositories.IRepositories;
using MyOrdersAPI.Repositories.IRepository;
using MyOrdersAPI.Repositories.Repository;
using MyOrdersAPI.Services;

var builder = WebApplication.CreateBuilder(args);

//  appsettings.json for sensitive configs like Firebase
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Policy allow all 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Dependency Injection setup
builder.Services.AddScoped<IDataService>(provider =>
    new DataService(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerManager, CustomerManager>();
builder.Services.AddScoped<IOrderManager, OrderManager>();

builder.Services.AddScoped<IPushNotificationRepository, PushNotificationRepository>();
builder.Services.AddScoped<IPushNotificationManager, PushNotificationManager>();
builder.Services.AddScoped<IPushNotificationService, PushNotificationService>();

// HttpClientFactory for Firebase PushNotificationService
builder.Services.AddHttpClient();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


// Middleware
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
