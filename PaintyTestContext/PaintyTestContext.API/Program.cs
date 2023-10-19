using Microsoft.AspNetCore.Identity;
using PaintyTestContext.Application;
using PaintyTestContext.Application.DTOs;
using PaintyTestContext.Domain;
using PaintyTestContext.Middlewares.ExceptionMiddleware;
using PaintyTestContext.Persistence;
using PaintyTestContext.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");

builder.Services.AddPersistence(connectionString);

var jwtOptions = new JwtOptionsDto
{
    RefreshTokenExpires = TimeSpan.FromDays(30),
    EXPIRES = TimeSpan.FromMinutes(30),
    ISSUER = builder.Configuration["JWT:Issuer"]!,
    AUDIENCE = builder.Configuration["JWT:Audience"]!,
    KEY = builder.Configuration["JWT:SecretKey"]!
};    

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<DBContext>();

builder.Services.AddApplication(jwtOptions);

builder.Services.AddSwaggerService();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy("AllowAllOrigins", policyBuilder =>
{
    policyBuilder.AllowAnyHeader();
    policyBuilder.AllowAnyMethod();
    policyBuilder.AllowAnyOrigin();
}));

var app = builder.Build();

app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<DBContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception e)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, $"Произошла ошибка при инициализации базы данных: {e.Message}");
    }
}

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();