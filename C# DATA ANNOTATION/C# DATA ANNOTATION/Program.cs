using RAIZA.Interfaces;
using RAIZA.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Usuario_I, Usuario_R>();
builder.Services.AddScoped<Tematica_I, Tematica_R>();
builder.Services.AddScoped<Tarea_I, Tarea_R>();
builder.Services.AddScoped<ProgresoLeccion_I, ProgresoLeccion_R>();
builder.Services.AddScoped<Progreso_I, Progreso_R>();
builder.Services.AddScoped<PedidoKit_I, PedidoKit_R>();
builder.Services.AddScoped<Notificacion_I, Notificacion_R>();
builder.Services.AddScoped<Modulo_I, Modulo_R>();
builder.Services.AddScoped<Leccion_I, Leccion_R>();
builder.Services.AddScoped<Instructor_I, Instructor_R>();
builder.Services.AddScoped<Estudiante_I, Estudiante_R>();
builder.Services.AddScoped<EntregaTarea_I, EntregaTarea_R>();
builder.Services.AddScoped<Compra_I, Compra_R>();
builder.Services.AddScoped<ClassKit_I, ClassKit_R>();
builder.Services.AddScoped<ClaseParticipante_I, ClaseParticipante_R>();
builder.Services.AddScoped<Certificado_I, Certificado_R>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

