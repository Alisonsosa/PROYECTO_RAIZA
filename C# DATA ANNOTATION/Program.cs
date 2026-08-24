using RAIZA.Interfaces;
using RAIZA.Repositories;

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

