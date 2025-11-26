using Microsoft.EntityFrameworkCore;
using TutorCenterBackend.Application;
using TutorCenterBackend.Infrastructure;
using TutorCenterBackend.Infrastructure.DataAccess;
using TutorCenterBackend.Presentation.Middlewares;
using TutorCenterBackend.Presentation.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Memory Cache for permissions
builder.Services.AddMemoryCache();

// Register Infrastructure layer (Repositories & Data Access Services)
builder.Services.AddInfrastructure();

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
