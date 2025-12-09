using Microsoft.EntityFrameworkCore;
using TutorCenterBackend.Application;
using TutorCenterBackend.Infrastructure;
using TutorCenterBackend.Infrastructure.DataAccess;
using TutorCenterBackend.Presentation.Middlewares;
using TutorCenterBackend.Presentation.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Memory Cache for permissions
builder.Services.AddMemoryCache();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// JWT Authentication Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    var secretKey = jwtSettings["Key"];
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
        ClockSkew = TimeSpan.Zero
    };
});

// Register Infrastructure layer (Repositories & Data Access Services)
builder.Services.AddInfrastructure(builder.Configuration);

// Register Application layer (Business Logic Services, AutoMapper & FluentValidation)
builder.Services.AddApplication();

// Add controllers with automatic FluentValidation
builder.Services.AddControllers(options =>
{
    // Register global filter for automatic validation
    options.Filters.Add<FluentValidationFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use global exception handler middleware
app.UseMiddleware<GlobalExceptionHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// IMPORTANT: Order matters!
// 1. Authentication must come first
app.UseAuthentication();

// 2. Authorization middleware
app.UseAuthorization();

// 3. Permission middleware - checks role & permission
app.UseMiddleware<PermissionMiddleware>();

app.MapControllers();

app.Run();
