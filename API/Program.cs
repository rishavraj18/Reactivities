using Microsoft.EntityFrameworkCore;
using Persistence;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. Order of services doesn't matter

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(option => 
{
    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline. Here order matters in case of middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    // app.UseSwaggerUI(options => 
    // {
    //     options.SwaggerEndpoint("/openapi/v1.json", "Demo API");
    // });
}

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod()
                             .WithOrigins("http://localhost:3000", "https://localhost:3000"));
app.MapControllers();

// EF migration code
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
  var logger = services.GetRequiredService<ILogger<Program>>();
  logger.LogError(ex, "An error occured during migration");
}
// upto here EF migration code

app.Run();
