using Microsoft.EntityFrameworkCore;
using RAIZA.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAIZA.Data
{
    public class DatabaseService : DbContext
    {
        public DatabaseService(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tematica> Tematica { get; set; }
        public DbSet<Tarea> Tarea { get; set; }
        public DbSet<ProgresoLeccion> ProgresoLeccion { get; set; }
        public DbSet<Progreso> Progreso { get; set; }
        public DbSet<PedidoKit> PedidoKit { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<Leccion> Leccion { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<EntregaTarea> EntregaTarea { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<Class_Kit> Class_Kit { get; set; }
        public DbSet<ClasesEnVivo> ClasesEnVivo { get; set; }
        public DbSet<ClaseParticipante> ClaseParticipante { get; set; }
        public DbSet<Certificado> Certificado { get; set; }
        public DbSet<Administrador> Administrador { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityConfiguration(modelBuilder);
        }

        private void EntityConfiguration(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
            modelBuilder.Entity<Usuario>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Usuario>().Property(u => u.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<Usuario>().Property(u => u.Correo).HasColumnName("Correo");
            modelBuilder.Entity<Usuario>().Property(u => u.ContrasenaHash).HasColumnName("Contrasena_hash");
            modelBuilder.Entity<Usuario>().Property(u => u.Rol).HasColumnName("Rol");
            modelBuilder.Entity<Usuario>().Property(u => u.Estado).HasColumnName("Estado");


            modelBuilder.Entity<Tematica>().ToTable("Tematica");
            modelBuilder.Entity<Tematica>().HasKey(t => t.idtematica);
            modelBuilder.Entity<Tematica>().Property(t => t.idtematica).HasColumnName("idtematica").ValueGeneratedOnAdd();
            modelBuilder.Entity<Tematica>().Property(t => t.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<Tematica>().Property(t => t.ImagenPortada).HasColumnName("ImagenPortada");

            modelBuilder.Entity<Tarea>().ToTable("Tarea");
            modelBuilder.Entity<Tarea>().HasKey(t => t.idtarea);
            modelBuilder.Entity<Tarea>().Property(t => t.idtarea).HasColumnName("idtarea").ValueGeneratedOnAdd();
            modelBuilder.Entity<Tarea>().Property(t => t.Titulo).HasColumnName("Titulo");
            modelBuilder.Entity<Tarea>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Tarea>().Property(t => t.FechaEntrega).HasColumnName("FechaEntrega");
            modelBuilder.Entity<Tarea>().Property(t => t.idmodulo).HasColumnName("idmodulo");

            modelBuilder.Entity<ProgresoLeccion>().ToTable("ProgresoLeccion");
            modelBuilder.Entity<ProgresoLeccion>().HasKey(p => p.Idprogresoleccion);
            modelBuilder.Entity<ProgresoLeccion>().Property(p => p.Idprogresoleccion).HasColumnName("Idprogresoleccion").ValueGeneratedOnAdd();
            modelBuilder.Entity<ProgresoLeccion>().Property(p => p.completado).HasColumnName("completado");
            modelBuilder.Entity<ProgresoLeccion>().Property(p => p.fecha).HasColumnName("fecha");
            modelBuilder.Entity<ProgresoLeccion>().Property(p => p.Idestudiante).HasColumnName("Idestudiante");
            modelBuilder.Entity<ProgresoLeccion>().Property(p => p.Idleccion).HasColumnName("Idleccion");

            modelBuilder.Entity<Progreso>().ToTable("Progreso");
            modelBuilder.Entity<Progreso>().HasKey(p => p.Idprogreso);
            modelBuilder.Entity<Progreso>().Property(p => p.Idprogreso).HasColumnName("idprogreso").ValueGeneratedOnAdd();
            modelBuilder.Entity<Progreso>().Property(p => p.Completado).HasColumnName("Completado");
            modelBuilder.Entity<Progreso>().Property(p => p.Porcentaje).HasColumnName("Porcentaje");
            modelBuilder.Entity<Progreso>().Property(p => p.FechaCompletado).HasColumnName("FechaCompletado");
            modelBuilder.Entity<Progreso>().Property(p => p.idestudiante).HasColumnName("idestudiante");
            modelBuilder.Entity<Progreso>().Property(p => p.Idmodulo).HasColumnName("idmodulo");

            modelBuilder.Entity<PedidoKit>().ToTable("PedidoKit");
            modelBuilder.Entity<PedidoKit>().HasKey(p => p.idPedidoKit);
            modelBuilder.Entity<PedidoKit>().Property(p => p.idPedidoKit).HasColumnName("idPedidoKit").ValueGeneratedOnAdd();
            modelBuilder.Entity<PedidoKit>().Property(p => p.Cantidad).HasColumnName("Cantidad");
            modelBuilder.Entity<PedidoKit>().Property(p => p.Estado).HasColumnName("Estado");
            modelBuilder.Entity<PedidoKit>().Property(p => p.Direccionenvio).HasColumnName("Direccionenvio");
            modelBuilder.Entity<PedidoKit>().Property(p => p.Fechapedido).HasColumnName("Fechapedido");
            modelBuilder.Entity<PedidoKit>().Property(p => p.Idestudiante).HasColumnName("Idestudiante");
            modelBuilder.Entity<PedidoKit>().Property(p => p.idclasskit).HasColumnName("idclasskit");
            modelBuilder.Entity<PedidoKit>().Property(p => p.idcompra).HasColumnName("idcompra");

            modelBuilder.Entity<Notificacion>().ToTable("Notificacion");
            modelBuilder.Entity<Notificacion>().HasKey(n => n.Idnotificacion);
            modelBuilder.Entity<Notificacion>().Property(n => n.Idnotificacion).HasColumnName("Idnotificacion").ValueGeneratedOnAdd();
            modelBuilder.Entity<Notificacion>().Property(n => n.tiponotificacion).HasColumnName("tiponotificacion");
            modelBuilder.Entity<Notificacion>().Property(n => n.mensaje).HasColumnName("mensaje");
            modelBuilder.Entity<Notificacion>().Property(n => n.estadoleido).HasColumnName("estadoleido");
            modelBuilder.Entity<Notificacion>().Property(n => n.fechaenvivo).HasColumnName("fechaenvivo");
            modelBuilder.Entity<Notificacion>().Property(n => n.idusuario).HasColumnName("idusuario");

            modelBuilder.Entity<Modulo>().ToTable("Modulo");
            modelBuilder.Entity<Modulo>().HasKey(m => m.idmodulo);
            modelBuilder.Entity<Modulo>().Property(m => m.idmodulo).HasColumnName("idmodulo").ValueGeneratedOnAdd();
            modelBuilder.Entity<Modulo>().Property(m => m.Nivel).HasColumnName("Nivel");
            modelBuilder.Entity<Modulo>().Property(m => m.Precio).HasColumnName("Precio");
            modelBuilder.Entity<Modulo>().Property(m => m.IncluyeKit).HasColumnName("IncluyeKit");
            modelBuilder.Entity<Modulo>().Property(m => m.idtematica).HasColumnName("idtematica");
            modelBuilder.Entity<Modulo>().Property(m => m.idinstructor).HasColumnName("idinstructor");

            modelBuilder.Entity<Leccion>().ToTable("Leccion");
            modelBuilder.Entity<Leccion>().HasKey(l => l.idleccion);
            modelBuilder.Entity<Leccion>().Property(l => l.idleccion).HasColumnName("idleccion").ValueGeneratedOnAdd();
            modelBuilder.Entity<Leccion>().Property(l => l.Titulo).HasColumnName("Titulo");
            modelBuilder.Entity<Leccion>().Property(l => l.Tipo).HasColumnName("Tipo");
            modelBuilder.Entity<Leccion>().Property(l => l.Orden).HasColumnName("Orden");
            modelBuilder.Entity<Leccion>().Property(l => l.idmodulo).HasColumnName("idmodulo");

            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<Instructor>().HasKey(i => i.idinstructor);
            modelBuilder.Entity<Instructor>().Property(i => i.idinstructor).HasColumnName("idinstructor").ValueGeneratedOnAdd();
            modelBuilder.Entity<Instructor>().Property(i => i.Especialidad).HasColumnName("Especialidad");
            modelBuilder.Entity<Instructor>().Property(i => i.Biografia).HasColumnName("Biografia");

            modelBuilder.Entity<Estudiante>().ToTable("Estudiante");
            modelBuilder.Entity<Estudiante>().HasKey(e => e.idestudiante);
            modelBuilder.Entity<Estudiante>().Property(e => e.idestudiante).HasColumnName("idestudiante").ValueGeneratedOnAdd();
            modelBuilder.Entity<Estudiante>().Property(e => e.Espremium).HasColumnName("Espremium");
            modelBuilder.Entity<Estudiante>().Property(e => e.FechaAcceso).HasColumnName("FechaAcceso");

            modelBuilder.Entity<EntregaTarea>().ToTable("EntregaTarea");
            modelBuilder.Entity<EntregaTarea>().HasKey(e => e.Identregatarea);
            modelBuilder.Entity<EntregaTarea>().Property(e => e.Identregatarea).HasColumnName("Identregatarea").ValueGeneratedOnAdd();
            modelBuilder.Entity<EntregaTarea>().Property(e => e.UrlArchivo).HasColumnName("UrlArchivo");
            modelBuilder.Entity<EntregaTarea>().Property(e => e.FechaEntrega).HasColumnName("FechaEntrega");
            modelBuilder.Entity<EntregaTarea>().Property(e => e.Calificacion).HasColumnName("Calificacion");
            modelBuilder.Entity<EntregaTarea>().Property(e => e.Comentario).HasColumnName("Comentario");
            modelBuilder.Entity<EntregaTarea>().Property(e => e.idtarea).HasColumnName("idtarea");
            modelBuilder.Entity<EntregaTarea>().Property(e => e.idestudiante).HasColumnName("idestudiante");
            modelBuilder.Entity<EntregaTarea>().Property(e => e.idinstructorcalifica).HasColumnName("idinstructorcalifica");

            modelBuilder.Entity<Compra>().ToTable("Compra");
            modelBuilder.Entity<Compra>().HasKey(c => c.idcompra);
            modelBuilder.Entity<Compra>().Property(c => c.idcompra).HasColumnName("idcompra").ValueGeneratedOnAdd();
            modelBuilder.Entity<Compra>().Property(c => c.Monto).HasColumnName("Monto");
            modelBuilder.Entity<Compra>().Property(c => c.MetodoPago).HasColumnName("MetodoPago");
            modelBuilder.Entity<Compra>().Property(c => c.Estado).HasColumnName("Estado");
            modelBuilder.Entity<Compra>().Property(c => c.ReferenciaWompi).HasColumnName("ReferenciaWompi");
            modelBuilder.Entity<Compra>().Property(c => c.FechaCompra).HasColumnName("FechaCompra");
            modelBuilder.Entity<Compra>().Property(c => c.idestudiante).HasColumnName("idestudiante");
            modelBuilder.Entity<Compra>().Property(c => c.idmodulo).HasColumnName("idmodulo");

            modelBuilder.Entity<Class_Kit>().ToTable("Class_Kit");
            modelBuilder.Entity<Class_Kit>().HasKey(c => c.idclass_kit);
            modelBuilder.Entity<Class_Kit>().Property(c => c.idclass_kit).HasColumnName("idclass_kit").ValueGeneratedOnAdd();
            modelBuilder.Entity<Class_Kit>().Property(c => c.name).HasColumnName("name");
            modelBuilder.Entity<Class_Kit>().Property(c => c.description).HasColumnName("description");
            modelBuilder.Entity<Class_Kit>().Property(c => c.precio).HasColumnName("precio");
            modelBuilder.Entity<Class_Kit>().Property(c => c.stockdisponible).HasColumnName("stockdisponible");
            modelBuilder.Entity<Class_Kit>().Property(c => c.tipo).HasColumnName("tipo");
            modelBuilder.Entity<Class_Kit>().Property(c => c.idmodulo).HasColumnName("idmodulo");

            modelBuilder.Entity<ClasesEnVivo>().ToTable("ClasesEnVivo");
            modelBuilder.Entity<ClasesEnVivo>().HasKey(c => c.idclaasesenvivo);
            modelBuilder.Entity<ClasesEnVivo>().Property(c => c.idclaasesenvivo).HasColumnName("idclaasesenvivo").ValueGeneratedOnAdd();
            modelBuilder.Entity<ClasesEnVivo>().Property(c => c.FechaHora).HasColumnName("FechaHora");
            modelBuilder.Entity<ClasesEnVivo>().Property(c => c.UrlSala).HasColumnName("UrlSala");
            modelBuilder.Entity<ClasesEnVivo>().Property(c => c.Estado).HasColumnName("Estado");
            modelBuilder.Entity<ClasesEnVivo>().Property(c => c.idmodulo).HasColumnName("idmodulo");
            modelBuilder.Entity<ClasesEnVivo>().Property(c => c.idinstructor).HasColumnName("idinstructor");

            modelBuilder.Entity<ClaseParticipante>().ToTable("ClaseParticipante");
            modelBuilder.Entity<ClaseParticipante>().HasKey(cp => new { cp.idclase, cp.idestudiante });
            modelBuilder.Entity<ClaseParticipante>().Property(cp => cp.idclase).HasColumnName("idclase");
            modelBuilder.Entity<ClaseParticipante>().Property(cp => cp.idestudiante).HasColumnName("idestudiante");
            modelBuilder.Entity<ClaseParticipante>().Property(cp => cp.FechaIngreso).HasColumnName("FechaIngreso");

            modelBuilder.Entity<Certificado>().ToTable("Certificado");
            modelBuilder.Entity<Certificado>().HasKey(c => c.idcertificado);
            modelBuilder.Entity<Certificado>().Property(c => c.idcertificado).HasColumnName("idcertificado").ValueGeneratedOnAdd();
            modelBuilder.Entity<Certificado>().Property(c => c.FechaEmision).HasColumnName("FechaEmision");
            modelBuilder.Entity<Certificado>().Property(c => c.UrlPdf).HasColumnName("UrlPdf");
            modelBuilder.Entity<Certificado>().Property(c => c.CodigoVerificacion).HasColumnName("CodigoVerificacion");
            modelBuilder.Entity<Certificado>().Property(c => c.idestudiante).HasColumnName("idestudiante");
            modelBuilder.Entity<Certificado>().Property(c => c.idmodulo).HasColumnName("idmodulo");

            modelBuilder.Entity<Administrador>().ToTable("Administrador");
            modelBuilder.Entity<Administrador>().HasKey(a => a.idadministrador);
            modelBuilder.Entity<Administrador>().Property(a => a.idadministrador).HasColumnName("idadministrador").ValueGeneratedOnAdd();
            modelBuilder.Entity<Administrador>().Property(a => a.NivelAcceso).HasColumnName("NivelAcceso");
        }


        public async Task<bool> SaveAsync()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}