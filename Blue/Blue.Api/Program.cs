using System.Security.Claims;
using System.Text;
using Blue.Application.Auth.Commands;
using Blue.Application.Auth.Commands.RegisterUser;
using Blue.Application.Common.Interfaces;
using Blue.Application.Media.Commands;
using Blue.Domain.Users;
using Blue.Infrastructure.Media;
using Blue.Infrastructure.Persistence;
using Blue.Infrastructure.Repositories;
using Blue.Infrastructure.Security;
using Blue.Infrastructure.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ---------------------
// Controllers
// ---------------------
builder.Services.AddControllers();

// ---------------------
// Swagger (CONFIGURADO BIEN)
// ---------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Blue API",
        Version = "v1"
    });

    // JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa: Bearer {tu_token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ---------------------
// Infrastructure
// ---------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

// ---------------------
// Media + Cloudinary
// ---------------------
builder.Services.AddScoped<IMediaRepository, MediaRepository>();
builder.Services.AddScoped<IMediaStorage, CloudinaryMediaStorage>();

builder.Services.AddScoped<UploadMediaCommand>();
builder.Services.AddScoped<GetAllMediaQuery>();
builder.Services.AddScoped<GetMediaByIdQuery>();
builder.Services.AddScoped<DeleteMediaCommand>();
builder.Services.AddScoped<UpdateMediaCommand>();

// ---------------------
// Auth Commands
// ---------------------
builder.Services.AddScoped<RegisterUserCommand>();
builder.Services.AddScoped<LoginUserCommand>();
builder.Services.AddScoped<RefreshTokenCommand>();


// ---------------------
// MySQL + EF Core
// ---------------------
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// ---------------------
// JWT Authentication
// ---------------------
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            ),
            RoleClaimType = ClaimTypes.Role
        };
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});



// ---------------------
// App
// ---------------------
var app = builder.Build();

// ---------------------
// HTTP Pipeline
// ---------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blue API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
await AdminSeed.SeedAsync(app.Services);

app.Run();
