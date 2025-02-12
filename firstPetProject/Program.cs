using firstPetProject.Core.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using System.Configuration;
using firstPetProject.Core.Auth.Repositories;
using firstPetProject.Core.Auth.BusinessLogic;
using firstPetProject.Core.Auth.Interfaces;
using firstPetProject.Core.Auth;
using firstPetProject.Core.Auth.Endpoints;

/*  Система управления задачами (Task Manager)
Описание: Разработай приложение для управления задачами, с возможностью создания, 
редактирования и удаления задач. Добавь категории задач, метки, приоритеты и дедлайны. 
Это может быть полезным инструментом для личного использования.

*/


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<JwtService>();    // Регистрация Jwt сервиса и его настроек
builder.Services.Configure<AuthSettings>(
    builder.Configuration.GetSection("AuthSettings"));

builder.Services.AddAuth(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(DataBaseMapping));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        o.RoutePrefix = "swagger";
    });
}

app.MapUsersEndpoints();
app.MapControllers();
app.MapRazorPages();

app.Run();
