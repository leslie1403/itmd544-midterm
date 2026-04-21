using System.Text.Json;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

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

app.Run();