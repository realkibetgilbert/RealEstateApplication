using Microsoft.EntityFrameworkCore;
using Tenant.API.Application;
using Tenant.API.Application.MappingProfile;
using Tenant.API.Infrastructure;
using Tenant.API.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TenantDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("KejaHuntConnection")
));

builder.Services.AddStackExchangeRedisCache(options =>
{
    var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection")
        ?? builder.Configuration.GetConnectionString("Redis")
        ?? "localhost:6379";
    options.Configuration = redisConnectionString;
}); 
builder.Services.AddApplicationRegistrationServices();
builder.Services.AddInfrastructureRegistrationServices();
builder.Services.AddAutoMapper(typeof(TenantMappingProfile));
builder.Services.AddControllers();
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
