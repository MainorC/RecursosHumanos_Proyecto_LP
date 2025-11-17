# Sistema de Gestión de Recursos Humanos (SGRH)

Sistema completo de gestión de recursos humanos desarrollado en C# con Windows Forms y SQL Server.

**Universidad**: Universidad Nacional de la Amazonía Peruana  
**Curso**: Lenguaje de Programación III  
**Grupo**: Grupo 1

**Desarrollado por**: 
- Mainor Luis Cumari Panduro
- Escalante Layango Brandon
- Reyna Teixeira Randall Dereth
- Gonzáles Murrieta Jesús Leonardo

**Repositorio**: [https://github.com/MainorC/RecursosHumanos_Proyecto_LP](https://github.com/MainorC/RecursosHumanos_Proyecto_LP)

## Características

- **Gestión de Empleados**: Registro, edición y consulta de información de empleados
- **Gestión de Áreas**: Administración de departamentos y áreas organizacionales
- **Control de Asistencia**: Registro de asistencia con cálculo automático de horas trabajadas
- **Gestión de Vacaciones**: Solicitud y aprobación de vacaciones
- **Evaluaciones de Desempeño**: Registro y seguimiento de evaluaciones
- **Generación de Nómina**: Preparación y gestión de nóminas por período
- **Reportes**: Exportación de reportes en formato Excel
- **Administración de Usuarios**: Gestión de usuarios y roles
- **Comunicados**: Publicación de anuncios y comunicados
- **Incorporación**: Gestión de procesos de onboarding y offboarding

## Estructura del Proyecto

```
RecursosHumanos/
├── Domain/
│   ├── Models/          # Entidades del dominio
│   └── Interfaces/      # Interfaces de repositorio
├── Data/
│   ├── Access/          # Implementaciones DAL (Data Access Layer)
│   ├── Context/         # DbContext de Entity Framework Core
│   └── Database/        # Scripts de base de datos
├── Business/
│   ├── Services/        # Servicios de negocio (Business Logic Layer)
│   └── Interfaces/      # Interfaces de servicios
├── Presentation/
│   ├── Views/           # Formularios Windows Forms
│   ├── Presenters/      # Presenters (Patrón MVP)
│   └── Interfaces/      # Interfaces de vistas
├── Common/
│   └── Helpers/         # Utilidades y helpers
└── Program.cs           # Punto de entrada
```

## Arquitectura

El sistema sigue una **arquitectura en capas (n-capas)** con separación clara de responsabilidades:

### Capas:

1. **Domain Layer** (`Domain/`)
   - Entidades del dominio (Models)
   - Interfaces de repositorio
   - No tiene dependencias de otras capas

2. **Data Layer** (`Data/`)
   - Implementaciones de repositorios usando Entity Framework Core
   - DbContext configurado con todas las entidades
   - Acceso a base de datos SQL Server

3. **Business Layer** (`Business/`)
   - Lógica de negocio y validaciones
   - Servicios que implementan interfaces
   - Inyección de dependencias mediante interfaces

4. **Presentation Layer** (`Presentation/`)
   - Formularios Windows Forms (Views)
   - Presenters siguiendo patrón MVP
   - Interfaces de vistas para desacoplamiento

5. **Common/Infrastructure** (`Common/`)
   - Utilidades y helpers compartidos
   - UIHelper para estilos consistentes

### Patrón MVP (Model-View-Presenter):

- **Model**: Entidades del dominio y lógica de negocio (BLL)
- **View**: Formularios Windows Forms
- **Presenter**: Coordina entre View y Model, maneja lógica de presentación

### Tecnologías:

- **.NET 8.0** - Framework
- **Windows Forms** - Interfaz de usuario
- **Entity Framework Core 8.0** - ORM para acceso a datos
- **SQL Server** - Base de datos
- **EPPlus** - Exportación a Excel
- **BCrypt.Net-Next** - Hashing seguro de contraseñas

## Funcionalidades Principales

### Panel Principal (Dashboard)
- Visualización de KPIs (Total de empleados, empleados activos, vacaciones pendientes, salario promedio)
- Anuncios de la compañía
- Acciones pendientes

### Gestión de Empleados
- CRUD completo de empleados
- Búsqueda por nombre o DNI
- Soft delete (marcar como inactivo)
- Validación de DNI único y email válido

### Control de Asistencia
- Registro de entrada y salida
- Cálculo automático de horas trabajadas
- Determinación de estado (Presente, Tarde, Ausente)
- Historial por mes

### Gestión de Vacaciones
- Solicitud de vacaciones
- Aprobación/Rechazo por administradores
- Cálculo automático de días

### Evaluaciones de Desempeño
- Registro de evaluaciones con puntaje 0-100
- Comentarios y observaciones
- Historial por empleado

### Generación de Nómina
- Preparación de nómina por período
- Cálculo de salario bruto, bonificaciones y deducciones
- Cálculo automático de salario neto
- Estados: Borrador, Pagada

### Reportes
- Exportación a Excel (formato .xlsx) de:
  - Lista de empleados activos (con todos los campos)
  - Asistencia mensual (con filtros por rango de fechas)
  - Vacaciones (con filtros por rango de fechas)
  - Evaluaciones de desempeño (con filtros por rango de fechas)
  - Nómina del período
- Formato profesional con encabezados estilizados, filtros automáticos y columnas ajustadas

## Validaciones Implementadas

- DNI único por empleado
- Email con formato válido
- Salario base >= 0
- Puntaje de evaluación entre 0-100
- Fechas válidas (fin > inicio)
- Un registro de asistencia por empleado por día

## Notas Técnicas

- **Soft Delete**: El sistema utiliza soft delete para todas las entidades (marca como inactivo en lugar de eliminar físicamente)
- **Entity Framework Core**: Acceso a datos mediante EF Core con DbContext configurado
- **Inyección de Dependencias**: Los servicios BLL usan interfaces de repositorio para desacoplamiento
- **Patrón Repository**: Implementado en la capa de datos
- **Patrón MVP**: Implementado parcialmente (EmpleadoPresenter como ejemplo)
- **Validaciones**: Validaciones completas en la capa de negocio
- **Seguridad**: Las contraseñas se almacenan usando BCrypt con hashing seguro
- **Conexión**: La cadena de conexión se configura en `Data/Access/DatabaseConnection.cs`

## Ejecución del Proyecto

### Requisitos Previos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

- .NET 8.0 SDK o Runtime
- SQL Server (LocalDB, Express o versión completa)
- Visual Studio 2022 (recomendado) o Visual Studio Code

### Pasos para Ejecutar

1. **Clonar o descargar el repositorio**

2. **Crear la base de datos**:
   - Abrir SQL Server Management Studio
   - Ejecutar el script `Data/Database/Database.sql` para crear la base de datos y las tablas

3. **Configurar la cadena de conexión**:
   - Editar el archivo `Data/Access/DatabaseConnection.cs`
   - Modificar la propiedad `ConnectionString` según tu configuración de SQL Server
   - Por defecto: `Server=LOCALHOST\\SQLEXPRESS;Database=SGRH;Integrated Security=true;TrustServerCertificate=true;`
   - Cambia `LOCALHOST\\SQLEXPRESS` por el nombre de tu instancia de SQL Server

4. **Restaurar paquetes NuGet**:
   ```bash
   dotnet restore
   ```

5. **Compilar el proyecto**:
   ```bash
   dotnet build
   ```

6. **Ejecutar la aplicación**:
   ```bash
   dotnet run
   ```
   
   O desde Visual Studio:
   - Presionar F5 o hacer clic en "Iniciar"

### Credenciales por Defecto

- **Usuario**: admin
- **Contraseña**: admin

### Solución de Problemas

**Error de conexión a la base de datos:**
- Verifica que SQL Server esté ejecutándose
- Revisa la cadena de conexión en `DatabaseConnection.cs`
- Asegúrate de que la base de datos SGRH exista

**Error al compilar:**
- Verifica que tengas .NET 8.0 instalado: `dotnet --version`
- Restaura los paquetes NuGet: `dotnet restore`
- Limpia y reconstruye: `dotnet clean` y luego `dotnet build`

## Documentación

Para consultar el manual de usuario completo con instrucciones detalladas sobre cómo utilizar todas las funcionalidades del sistema, visita:

**[Manual de Usuario](Docs/Manual_Usuario.md)**

## Licencia

Este proyecto es de uso educativo y demostrativo. Desarrollado como parte del curso de Lenguaje de Programación III de la Universidad Nacional de la Amazonía Peruana.
