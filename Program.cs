using Civir.Infrastructure;
using Civir.Infrastructure.Persistence;
using Civir.Auth.Application;
using Civir.Auth.Application.Middlewares;
using Microsoft.EntityFrameworkCore;
using Civir.Utils.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);


builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();


// Add cadena de conexion .
builder.Services.AddDbContext<CivirDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(CivirDbContext).Assembly.FullName)));

// Add services to the container.

builder.Services.AddControllers();
// Using Swashbuckle for OpenAPI/Swagger


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myAllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins("*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddSwaggerGen(c =>
{ 
    c.EnableAnnotations();
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "My API Explorer";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        options.InjectStylesheet("/swagger-ui/custom.css");
    });

}





app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = scope.ServiceProvider.GetRequiredService<CivirDbContext>();
        context.Database.Migrate();

    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurrio un error durante la migracion de la base de datos");
    }
}

app.UseCors("myAllowSpecificOrigins");

app.Run();
