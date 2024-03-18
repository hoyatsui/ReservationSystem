using DotnetCoding.Core.Interfaces;
using DotnetCoding.Infrastructure;
using DotnetCoding.Infrastructure.Repositories;
using DotnetCoding.Infrastructure.ServiceExtension;
using DotnetCoding.Services;
using DotnetCoding.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddScoped<IProviderService, ProviderService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IAppointmentSlotService, AppointmentSlotService>();

builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAppointmentSlotRepository, AppointmentSlotRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

string redisConnection = builder.Configuration.GetValue<string>("RedisConnection");

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));

builder.Services.AddHostedService<DailyCleanupService>();
builder.Services.AddHostedService<RedisKeyExpirationListener>();


builder.Services.AddAutoMapper(typeof(Program), typeof(MappingProfile));

builder.Services.AddControllers();

/*builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});*/

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
