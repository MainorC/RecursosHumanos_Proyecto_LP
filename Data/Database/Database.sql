-- Sistema de Gestión de Recursos Humanos
-- Script de creación de base de datos


USE master;
GO


-- Crear base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SGRH')
BEGIN
    CREATE DATABASE SGRH;
END
GO


USE SGRH;
GO


-- Tabla de Usuarios
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Usuarios] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [NombreUsuario] NVARCHAR(50) NOT NULL UNIQUE,
        [Contrasena] NVARCHAR(255) NOT NULL,
        [NombreCompleto] NVARCHAR(100) NOT NULL,
        [Rol] NVARCHAR(20) NOT NULL DEFAULT 'Empleado',
        [Activo] BIT NOT NULL DEFAULT 1,
        [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO


-- Tabla de Áreas
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Areas]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Areas] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [Nombre] NVARCHAR(100) NOT NULL UNIQUE,
        [Descripcion] NVARCHAR(500) NULL,
        [Activo] BIT NOT NULL DEFAULT 1,
        [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO


-- Tabla de Empleados
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Empleados]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Empleados] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [DNI] NVARCHAR(20) NOT NULL UNIQUE,
        [Nombre] NVARCHAR(100) NOT NULL,
        [Apellido] NVARCHAR(100) NOT NULL,
        [Email] NVARCHAR(100) NOT NULL,
        [Telefono] NVARCHAR(20) NULL,
        [FechaNacimiento] DATE NULL,
        [Direccion] NVARCHAR(255) NULL,
        [AreaId] INT NULL,
        [Puesto] NVARCHAR(100) NULL,
        [TipoContrato] NVARCHAR(50) NULL,
        [FechaContrato] DATE NULL,
        [SalarioBase] DECIMAL(10,2) NOT NULL DEFAULT 0,
        [SistemaPension] NVARCHAR(20) NULL,
        [Estado] NVARCHAR(20) NOT NULL DEFAULT 'Activo',
        [Activo] BIT NOT NULL DEFAULT 1,
        [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY ([AreaId]) REFERENCES [dbo].[Areas]([Id])
    );
END
GO


-- Tabla de Asistencia
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Asistencia]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Asistencia] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [EmpleadoId] INT NOT NULL,
        [Fecha] DATE NOT NULL,
        [HoraEntrada] TIME NULL,
        [HoraSalida] TIME NULL,
        [HorasTrabajadas] DECIMAL(5,2) NULL,
        [Estado] NVARCHAR(20) NOT NULL DEFAULT 'Presente',
        [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY ([EmpleadoId]) REFERENCES [dbo].[Empleados]([Id]),
        UNIQUE([EmpleadoId], [Fecha])
    );
END
GO


-- Tabla de Vacaciones
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Vacaciones]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Vacaciones] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [EmpleadoId] INT NOT NULL,
        [FechaInicio] DATE NOT NULL,
        [FechaFin] DATE NOT NULL,
        [DiasTotales] INT NOT NULL,
        [Estado] NVARCHAR(20) NOT NULL DEFAULT 'Pendiente',
        [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE(),
        [FechaAprobacion] DATETIME NULL,
        FOREIGN KEY ([EmpleadoId]) REFERENCES [dbo].[Empleados]([Id])
    );
END
GO


-- Tabla de Evaluaciones
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Evaluaciones]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Evaluaciones] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [EmpleadoId] INT NOT NULL,
        [Fecha] DATE NOT NULL,
        [Puntaje] INT NOT NULL CHECK ([Puntaje] >= 0 AND [Puntaje] <= 100),
        [Fortalezas] NVARCHAR(MAX) NULL,
        [OportunidadesMejora] NVARCHAR(MAX) NULL,
        [Comentarios] NVARCHAR(MAX) NULL,
        [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY ([EmpleadoId]) REFERENCES [dbo].[Empleados]([Id])
    );
END
GO


-- Tabla de Nómina
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Nomina]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Nomina] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [EmpleadoId] INT NOT NULL,
        [Periodo] NVARCHAR(20) NOT NULL,
        [SalarioBruto] DECIMAL(10,2) NOT NULL DEFAULT 0,
        [Bonificaciones] DECIMAL(10,2) NOT NULL DEFAULT 0,
        [Deducciones] DECIMAL(10,2) NOT NULL DEFAULT 0,
        [SalarioNeto] DECIMAL(10,2) NOT NULL DEFAULT 0,
        [Estado] NVARCHAR(20) NOT NULL DEFAULT 'Borrador',
        [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE(),
        [FechaPago] DATETIME NULL,
        FOREIGN KEY ([EmpleadoId]) REFERENCES [dbo].[Empleados]([Id])
    );
END
GO


-- Tabla de Comunicados
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comunicados]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Comunicados] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [Titulo] NVARCHAR(200) NOT NULL,
        [Contenido] NVARCHAR(MAX) NOT NULL,
        [FechaPublicacion] DATETIME NOT NULL DEFAULT GETDATE(),
        [Activo] BIT NOT NULL DEFAULT 1
    );
END
GO


-- Tabla de Incorporación (Onboarding/Offboarding)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Incorporacion]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Incorporacion] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [EmpleadoId] INT NULL,
        [NombreEmpleado] NVARCHAR(200) NOT NULL,
        [TipoProceso] NVARCHAR(20) NOT NULL,
        [FechaInicio] DATE NOT NULL,
        [FechaFin] DATE NULL,
        [Estado] NVARCHAR(20) NOT NULL DEFAULT 'En Proceso',
        [TareasCompletadas] INT NOT NULL DEFAULT 0,
        [TotalTareas] INT NOT NULL DEFAULT 0,
        [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY ([EmpleadoId]) REFERENCES [dbo].[Empleados]([Id])
    );
END
GO


-- Tabla de Tareas de Incorporación
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TareasIncorporacion]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[TareasIncorporacion] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [IncorporacionId] INT NOT NULL,
        [Descripcion] NVARCHAR(500) NOT NULL,
        [Completada] BIT NOT NULL DEFAULT 0,
        [FechaCompletada] DATETIME NULL,
        FOREIGN KEY ([IncorporacionId]) REFERENCES [dbo].[Incorporacion]([Id]) ON DELETE CASCADE
    );
END
GO


-- Insertar datos iniciales


-- Usuario administrador por defecto (contraseña: admin - hash BCrypt seguro)
-- El hash BCrypt incluye el salt automáticamente, por lo que es seguro almacenarlo
IF NOT EXISTS (SELECT * FROM [dbo].[Usuarios] WHERE [NombreUsuario] = 'admin')
BEGIN
    -- Hash BCrypt para la contraseña "admin" (work factor 10)
    -- Este hash se genera automáticamente con salt único por BCrypt
    INSERT INTO [dbo].[Usuarios] ([NombreUsuario], [Contrasena], [NombreCompleto], [Rol], [Activo])
    VALUES ('admin', '$2a$10$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy', 'Administrador del Sistema', 'Administrador', 1);
END
GO


-- Insertar áreas por defecto
IF NOT EXISTS (SELECT * FROM [dbo].[Areas] WHERE [Nombre] = 'Finanzas')
BEGIN
    INSERT INTO [dbo].[Areas] ([Nombre], [Descripcion], [Activo])
    VALUES 
        ('Finanzas', 'Departamento de contabilidad y finanzas', 1),
        ('Tecnología', 'Departamento de TI y desarrollo de software', 1),
        ('Recursos Humanos', 'Departamento de gestión de personal y bienestar', 1),
        ('Operaciones', 'Departamento de operaciones y logística', 1),
        ('Comercial', 'Departamento de ventas y marketing', 1),
        ('Administración', 'Departamento administrativo y gestión general', 1);
END
GO


-- Insertar empleados de ejemplo
IF NOT EXISTS (SELECT * FROM [dbo].[Empleados] WHERE [DNI] = '72456123')
BEGIN
    DECLARE @AreaFinanzas INT = (SELECT Id FROM [dbo].[Areas] WHERE Nombre = 'Finanzas');
    DECLARE @AreaTecnologia INT = (SELECT Id FROM [dbo].[Areas] WHERE Nombre = 'Tecnología');
    DECLARE @AreaRRHH INT = (SELECT Id FROM [dbo].[Areas] WHERE Nombre = 'Recursos Humanos');
    DECLARE @AreaOperaciones INT = (SELECT Id FROM [dbo].[Areas] WHERE Nombre = 'Operaciones');
    DECLARE @AreaComercial INT = (SELECT Id FROM [dbo].[Areas] WHERE Nombre = 'Comercial');
    DECLARE @AreaAdmin INT = (SELECT Id FROM [dbo].[Areas] WHERE Nombre = 'Administración');
    
    INSERT INTO [dbo].[Empleados] ([DNI], [Nombre], [Apellido], [Email], [Telefono], [FechaNacimiento], [Direccion], [AreaId], [Puesto], [TipoContrato], [FechaContrato], [SalarioBase], [SistemaPension], [Estado], [Activo])
    VALUES 
        -- Área de Tecnología
        ('72456123', 'Luis', 'Quispe Flores', 'luis.quispe@empresa.com.pe', '987654321', '1995-03-15', 'Av. Arequipa 1234, Lima', @AreaTecnologia, 'Desarrollador Full Stack Senior', 'Indefinido', '2022-01-10', 6200.00, 'AFP', 'Activo', 1),
        ('73521456', 'María', 'García Sánchez', 'maria.garcia@empresa.com.pe', '965432178', '1998-07-22', 'Jr. Ucayali 567, Lima', @AreaTecnologia, 'Desarrollador Junior', 'Indefinido', '2024-06-01', 2800.00, 'AFP', 'Activo', 1),
        ('70123789', 'Carlos', 'Rodríguez Huamán', 'carlos.rodriguez@empresa.com.pe', '945678123', '1990-11-08', 'Calle Los Pinos 890, San Isidro', @AreaTecnologia, 'Arquitecto de Software', 'Indefinido', '2021-03-15', 7500.00, 'AFP', 'Activo', 1),
        
        -- Área de Finanzas
        ('71234567', 'Ana', 'Flores Rojas', 'ana.flores@empresa.com.pe', '923456789', '1992-05-18', 'Av. Javier Prado 2345, San Borja', @AreaFinanzas, 'Contador Senior', 'Indefinido', '2020-08-20', 4500.00, 'AFP', 'Activo', 1),
        ('74567890', 'Diego', 'Mamani Vásquez', 'diego.mamani@empresa.com.pe', '956789012', '1996-09-30', 'Jr. Camaná 123, Cercado de Lima', @AreaFinanzas, 'Analista Financiero', 'Indefinido', '2023-02-14', 3800.00, 'AFP', 'Activo', 1),
        
        -- Área de Recursos Humanos
        ('72345678', 'Gabriela', 'López Pérez', 'gabriela.lopez@empresa.com.pe', '934567890', '1991-12-05', 'Av. La Marina 456, Pueblo Libre', @AreaRRHH, 'Jefe de Recursos Humanos', 'Indefinido', '2019-05-10', 5200.00, 'AFP', 'Activo', 1),
        
        -- Área de Operaciones
        ('73456789', 'Juan', 'Torres Ramírez', 'juan.torres@empresa.com.pe', '978901234', '1988-04-25', 'Calle Las Begonias 789, San Isidro', @AreaOperaciones, 'Supervisor de Operaciones', 'Indefinido', '2018-11-03', 4200.00, 'AFP', 'Activo', 1),
        ('75678901', 'Camila', 'Díaz Mendoza', 'camila.diaz@empresa.com.pe', '912345678', '1999-01-14', 'Jr. Lampa 234, Cercado de Lima', @AreaOperaciones, 'Asistente de Operaciones', 'Plazo Fijo', '2024-09-01', 2100.00, 'AFP', 'Activo', 1),
        
        -- Área Comercial
        ('76789012', 'Santiago', 'Chávez Gonzáles', 'santiago.chavez@empresa.com.pe', '967890123', '1993-06-20', 'Av. Benavides 1567, Miraflores', @AreaComercial, 'Ejecutivo de Ventas', 'Indefinido', '2022-07-18', 3500.00, 'AFP', 'Activo', 1),
        
        -- Área de Administración
        ('77890123', 'Valentina', 'Castillo Espinoza', 'valentina.castillo@empresa.com.pe', '989012345', '1994-10-12', 'Calle Schell 678, Miraflores', @AreaAdmin, 'Asistente Administrativo', 'Indefinido', '2023-04-22', 2500.00, 'ONP', 'Activo', 1);
END
GO


-- Insertar comunicados de ejemplo
IF NOT EXISTS (SELECT * FROM [dbo].[Comunicados] WHERE [Titulo] = 'Implementación de Nueva Plataforma PAST')
BEGIN
    INSERT INTO [dbo].[Comunicados] ([Titulo], [Contenido], [FechaPublicacion], [Activo])
    VALUES 
        ('Implementación de Nueva Plataforma PAST', 'Informamos que a partir de 2027 se implementará la Plataforma de Afiliación Segura y Transparente (PAST) para la gestión del sistema de pensiones. Todos los colaboradores recibirán capacitación sobre este nuevo sistema.', '2025-11-10', 1),
        ('Reunión de Cierre Anual 2025', 'Recordatorio: La reunión de cierre anual se llevará a cabo el viernes 20 de diciembre a las 10:00 AM en el auditorio principal. Se compartirán los resultados del año y la proyección para 2026.', '2025-11-08', 1),
        ('Actualización de Políticas de Trabajo Híbrido', 'Se han actualizado las políticas de trabajo híbrido para el 2026. A partir de enero, el esquema será de 3 días presenciales y 2 días remotos. Consulten el documento completo en la intranet corporativa.', '2025-11-01', 1),
        ('Fiestas Patrias - Vacaciones de Medio Año', 'Les recordamos que por fiestas patrias tendremos descanso del 28 al 30 de julio. Se retoman las actividades normalmente el 31 de julio.', '2025-10-25', 1);
END
GO


PRINT 'Base de datos SGRH creada exitosamente con datos iniciales actualizados para Perú 2025.';
GO
