using Microsoft.EntityFrameworkCore;
using Property.API.Application;
using Property.API.Infrastructure;
using Property.API.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PropertyDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("KejaHuntConnection")
));
// Register the infrastructure services
 builder.Services.AddInfrastructureRegistrationServices();
// Register the application services
builder.Services.AddApplicationRegistrationServices();
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
