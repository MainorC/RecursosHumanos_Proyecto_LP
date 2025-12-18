-- =============================================
-- Script para resetear contraseña de usuario admin
-- Sistema de Gestión de Recursos Humanos (SGRH)
-- =============================================
-- 
-- INSTRUCCIONES:
-- 1. Abre SQL Server Management Studio (SSMS)
-- 2. Conéctate a tu servidor SQL
-- 3. Selecciona la base de datos SGRH
-- 4. Ejecuta este script completo (F5)
-- 5. La contraseña del usuario 'admin' será: admin
-- 
-- =============================================

USE [SGRH]
GO

-- Verificar si el usuario existe
IF EXISTS (SELECT 1 FROM [dbo].[Usuarios] WHERE [NombreUsuario] = 'admin')
BEGIN
    PRINT 'Usuario admin encontrado. Actualizando contraseña...'
    
 -- Actualizar contraseña con hash BCrypt válido para "admin"
    -- Este hash fue generado con BCrypt.Net-Next (work factor 10)
    UPDATE [dbo].[Usuarios]
    SET [Contrasena] = '$2a$10$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy',
        [Activo] = 1
    WHERE [NombreUsuario] = 'admin'
    
    PRINT 'Contraseña actualizada exitosamente.'
    PRINT 'Usuario: admin'
    PRINT 'Contraseña: admin'
END
ELSE
BEGIN
    PRINT 'Usuario admin NO encontrado. Creando nuevo usuario...'
    
 -- Crear usuario admin si no existe
    INSERT INTO [dbo].[Usuarios] ([NombreUsuario], [Contrasena], [NombreCompleto], [Rol], [Activo])
    VALUES ('admin', '$2a$10$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy', 'Administrador del Sistema', 'Administrador', 1)
    
    PRINT 'Usuario admin creado exitosamente.'
    PRINT 'Usuario: admin'
    PRINT 'Contraseña: admin'
END
GO

-- Verificar el resultado
SELECT 
    [Id],
    [NombreUsuario],
    [NombreCompleto],
    [Rol],
    [Activo],
    LEFT([Contrasena], 20) + '...' AS [Hash_Preview]
FROM [dbo].[Usuarios]
WHERE [NombreUsuario] = 'admin'
GO

PRINT ''
PRINT '============================================='
PRINT 'Script ejecutado correctamente'
PRINT 'Ahora puedes iniciar sesión con:'
PRINT '   Usuario: admin'
PRINT '   Contraseña: admin'
PRINT '============================================='
