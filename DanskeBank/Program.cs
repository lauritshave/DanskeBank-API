using DanskeBank.Repositories;
using Microsoft.OpenApi.Models;

using System;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 1) Add MVC Controllers
builder.Services.AddControllers();

// 2) Register In‑Memory Repository
builder.Services.AddSingleton<IProductsRepository, InMemoryProductsRepository>();

// 3) Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DanskeBank Products API",
        Version = "v1",
        Description = "A simple in‑memory CRUD API for products"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// 4) Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Always serve Swagger UI at root
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API v1");
    c.RoutePrefix = string.Empty;  // serve Swagger UI at root
});

// Only redirect HTTP requests to HTTPS in Development (avoid redirect loops or unreachable HTTPS in container)
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseMiddleware<DanskeBank.Middleware.ErrorHandlingMiddleware>(); // Middleware error handler
app.UseAuthorization();

// 5) Map API controllers
app.MapControllers();

app.Run();
