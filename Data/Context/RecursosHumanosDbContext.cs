using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Data.Context
{
    /// <summary>
    /// Contexto de base de datos para Entity Framework Core
    /// </summary>
    public class RecursosHumanosDbContext : DbContext
    {
        public RecursosHumanosDbContext()
        {
        }

        public RecursosHumanosDbContext(DbContextOptions<RecursosHumanosDbContext> options)
            : base(options)
        {
        }

        // DbSets para cada entidad
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Asistencia> Asistencia { get; set; }
        public DbSet<Vacacion> Vacaciones { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public DbSet<Nomina> Nomina { get; set; }
        public DbSet<Comunicado> Comunicados { get; set; }
        public DbSet<Incorporacion> Incorporacion { get; set; }
        public DbSet<TareaIncorporacion> TareasIncorporacion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Usar la cadena de conexión desde DatabaseConnection
                var connectionString = Access.DatabaseConnection.ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.NombreUsuario).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.NombreUsuario).IsUnique();
                entity.Property(e => e.Contrasena).IsRequired().HasMaxLength(255);
                entity.Property(e => e.NombreCompleto).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Rol).IsRequired().HasMaxLength(20).HasDefaultValue("Empleado");
                entity.Property(e => e.Activo).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.FechaCreacion).IsRequired().HasDefaultValueSql("GETDATE()");
            });

            // Configuración de Area
            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Areas");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Nombre).IsUnique();
                entity.Property(e => e.Descripcion).HasMaxLength(500);
                entity.Property(e => e.Activo).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.FechaCreacion).IsRequired().HasDefaultValueSql("GETDATE()");
            });

            // Configuración de Empleado
            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("Empleados");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.DNI).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.DNI).IsUnique();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Telefono).HasMaxLength(20);
                entity.Property(e => e.Direccion).HasMaxLength(255);
                entity.Property(e => e.Puesto).HasMaxLength(100);
                entity.Property(e => e.TipoContrato).HasMaxLength(50);
                entity.Property(e => e.SistemaPension).HasMaxLength(20);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20).HasDefaultValue("Activo");
                entity.Property(e => e.SalarioBase).HasColumnType("decimal(10,2)").HasDefaultValue(0);
                entity.Property(e => e.Activo).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.FechaCreacion).IsRequired().HasDefaultValueSql("GETDATE()");

                // Relación con Area
                entity.HasOne(e => e.Area)
                    .WithMany(a => a.Empleados)
                    .HasForeignKey(e => e.AreaId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configuración de Asistencia
            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.ToTable("Asistencia");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20).HasDefaultValue("Presente");
                entity.Property(e => e.FechaCreacion).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.HorasTrabajadas).HasColumnType("decimal(5,2)");

                // Relación con Empleado
                entity.HasOne(e => e.Empleado)
                    .WithMany(emp => emp.Asistencias)
                    .HasForeignKey(e => e.EmpleadoId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Índice único para EmpleadoId y Fecha
                entity.HasIndex(e => new { e.EmpleadoId, e.Fecha }).IsUnique();
            });

            // Configuración de Vacacion
            modelBuilder.Entity<Vacacion>(entity =>
            {
                entity.ToTable("Vacaciones");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20).HasDefaultValue("Pendiente");
                entity.Property(e => e.FechaCreacion).IsRequired().HasDefaultValueSql("GETDATE()");

                // Relación con Empleado
                entity.HasOne(e => e.Empleado)
                    .WithMany(emp => emp.Vacaciones)
                    .HasForeignKey(e => e.EmpleadoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Evaluacion
            modelBuilder.Entity<Evaluacion>(entity =>
            {
                entity.ToTable("Evaluaciones");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.FechaCreacion).IsRequired().HasDefaultValueSql("GETDATE()");

                // Relación con Empleado
                entity.HasOne(e => e.Empleado)
                    .WithMany(emp => emp.Evaluaciones)
                    .HasForeignKey(e => e.EmpleadoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Nomina
            modelBuilder.Entity<Nomina>(entity =>
            {
                entity.ToTable("Nomina");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Periodo).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20).HasDefaultValue("Borrador");
                entity.Property(e => e.SalarioBruto).HasColumnType("decimal(10,2)").HasDefaultValue(0);
                entity.Property(e => e.Bonificaciones).HasColumnType("decimal(10,2)").HasDefaultValue(0);
                entity.Property(e => e.Deducciones).HasColumnType("decimal(10,2)").HasDefaultValue(0);
                entity.Property(e => e.SalarioNeto).HasColumnType("decimal(10,2)").HasDefaultValue(0);
                entity.Property(e => e.FechaCreacion).IsRequired().HasDefaultValueSql("GETDATE()");

                // Relación con Empleado
                entity.HasOne(e => e.Empleado)
                    .WithMany(emp => emp.Nominas)
                    .HasForeignKey(e => e.EmpleadoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Comunicado
            modelBuilder.Entity<Comunicado>(entity =>
            {
                entity.ToTable("Comunicados");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Titulo).IsRequired();
                entity.Property(e => e.Contenido).IsRequired();
                entity.Property(e => e.FechaPublicacion).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Activo).IsRequired().HasDefaultValue(true);
            });

            // Configuración de Incorporacion
            modelBuilder.Entity<Incorporacion>(entity =>
            {
                entity.ToTable("Incorporacion");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.NombreEmpleado).IsRequired().HasMaxLength(200);
                entity.Property(e => e.TipoProceso).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20).HasDefaultValue("En Proceso");
                entity.Property(e => e.FechaCreacion).IsRequired().HasDefaultValueSql("GETDATE()");

                // Relación con TareasIncorporacion
                entity.HasMany(e => e.Tareas)
                    .WithOne(t => t.Incorporacion)
                    .HasForeignKey(t => t.IncorporacionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de TareaIncorporacion
            modelBuilder.Entity<TareaIncorporacion>(entity =>
            {
                entity.ToTable("TareasIncorporacion");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Descripcion).IsRequired();
                entity.Property(e => e.Completada).IsRequired().HasDefaultValue(false);

                // Relación con Incorporacion
                entity.HasOne(e => e.Incorporacion)
                    .WithMany(inc => inc.Tareas)
                    .HasForeignKey(e => e.IncorporacionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
