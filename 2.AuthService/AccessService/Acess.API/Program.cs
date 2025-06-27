using Access.API.Application;
using Access.API.Domain.Entities;
using Access.API.Infrastructure;
using Access.API.Infrastructure.Data.Seed;
using Access.API.Infrastructure.Data.Seed.Models;
using Access.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("KejaHuntConnection")
)); 
builder.Services.Configure<SeederSettings>(
    builder.Configuration.GetSection("DatabaseSeeder"));
// Application layer services
builder.Services.AddApplicationServices();
// Infrastructure layer services (e.g., repositories)
builder.Services.AddInfrastructureServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityCore<AuthUser>()
    .AddRoles<IdentityRole<long>>()
    .AddTokenProvider<DataProtectorTokenProvider<AuthUser>>("KejaHunt")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 1;
}
); 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();
//migrate on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var config = services.GetRequiredService<IConfiguration>();

    try
    {
        var dbContext = services.GetRequiredService<AuthDbContext>();

        await dbContext.Database.MigrateAsync();

        var userManager = services.GetRequiredService<UserManager<AuthUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<long>>>();
        var seederOptions = services.GetRequiredService<IOptions<SeederSettings>>();

        await DatabaseSeeder.SeedAsync(dbContext, userManager, roleManager, seederOptions);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration.");
        throw;
    }
}
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
