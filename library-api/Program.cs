using System.Text.Json;
using Library_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");

if (string.IsNullOrEmpty(connectionString))
{
    // Fallback for local development if not set
    connectionString = "Server=localhost;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;";
}

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library API",
        Version = "1.0.0",
        Description = "A contract-first REST API for managing books in a library system."
    });
});

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    context.Database.EnsureCreated();
}

// Serve static files for the client app
app.UseDefaultFiles();
app.UseStaticFiles();

// Swagger UI at /docs
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1");
    options.RoutePrefix = "docs";
});

// Raw YAML spec
app.MapGet("/openapi.yaml", async context =>
{
    context.Response.ContentType = "text/yaml";
    await context.Response.SendFileAsync(Path.Combine(app.Environment.ContentRootPath, "openapi.yaml"));
});

// Temporary JSON route
app.MapGet("/openapi.json", async context =>
{
    var yamlText = await File.ReadAllTextAsync(
        Path.Combine(app.Environment.ContentRootPath, "openapi.yaml")
    );

    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(JsonSerializer.Serialize(new
    {
        openapi = yamlText
    }));
});

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();