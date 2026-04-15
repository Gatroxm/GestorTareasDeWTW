using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la conexión a SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Registrar el Servicio de Negocio (Inyección de Dependencias)
builder.Services.AddScoped<ITaskService, TaskService>();
// 3. Registrar el Servicio de Usuarios
builder.Services.AddScoped<IUserService, UserService>();
// 4. Configurar CORS (Permite que Angular se conecte a la API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200") // URL por defecto de Angular
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 4. Usar la política de CORS definida arriba
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();