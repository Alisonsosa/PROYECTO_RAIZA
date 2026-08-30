using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RAIZA.Data;
using RAIZA.Interfaces;
using RAIZA.Models;
using RAIZA.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUsuarioI, Usuario_R>();
builder.Services.AddScoped<ITematicaI, Tematica_R>();
builder.Services.AddScoped<ITarea_I, Tarea_R>();
builder.Services.AddScoped<IProgresoLeccionI, ProgresoLeccion_R>();
builder.Services.AddScoped<IProgresoI, Progreso_R>();
builder.Services.AddScoped<IPedidoKit_I, PedidoKit_R>();
builder.Services.AddScoped<INotificacionI, Notificacion_R>();
builder.Services.AddScoped<IModuloI, Modulo_R>();
builder.Services.AddScoped<ILeccionI, Leccion_R>();
builder.Services.AddScoped<IInstructor_I, Instructor_R>();
builder.Services.AddScoped<IEstudiante_I, Estudiante_R>();
builder.Services.AddScoped<IEntregaTareaI, EntregaTarea_R>();
builder.Services.AddScoped<CompraI, Compra_R>();
builder.Services.AddScoped<IClassKitI, ClassKit_R>();
builder.Services.AddScoped<ClaseParticipanteI, ClaseParticipante_R>();
builder.Services.AddScoped<CertificadoI, Certificado_R>();
builder.Services.AddScoped<AdministradorI, Administrador_R>();
builder.Services.AddScoped<IClasesEnVivoI, ClasesEnVivo_R>();

// Conexión a la base de datos
builder.Services.AddDbContext<DatabaseService>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnectionStrings")));

// Configuración de autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();