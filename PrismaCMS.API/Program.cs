using PrismaCMS.Application;
using PrismaCMS.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",    // React default
                "http://localhost:3001",    // React alternative
                "http://localhost:4200",    // Angular default
                "http://localhost:5173",    // Vite default
                "http://localhost:8080",    // Vue default
                "http://localhost:8081",    // Vue alternative
                "https://localhost:3000",   // HTTPS versions
                "https://localhost:3001",
                "https://localhost:4200",
                "https://localhost:5173",
                "https://localhost:8080",
                "https://localhost:8081"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });

    // For development - allows all origins (use with caution)
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add application and infrastructure services
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Add AutoMapper explicitly at the API level
builder.Services.AddAutoMapper(typeof(PrismaCMS.Application.Common.Mappings.MappingProfile));

var app = builder.Build();

// Seed database
await app.Services.SeedDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS - must be before UseHttpsRedirection and UseAuthorization
app.UseCors(app.Environment.IsDevelopment() ? "AllowAll" : "AllowSpecificOrigins");

app.UseHttpsRedirection();

// Map controllers
app.MapControllers();

app.Run();


