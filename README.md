# Technical-Test-COTO

API en **.NET 8** con **Clean Architecture** y **SQL Server** en Docker.

---

## Requisitos

- **.NET SDK 8**
- **Docker Desktop**

---

## Variables de entorno y configuración

> En la raíz del repo hay un **`.env.example`**. Copialo a **`.env`** y completá los valores.  
> Si ejecutás desde **Visual Studio**, poné la *connection string* en **`appsettings.Development.json`**.

### Variables a definir en `.env`

```bash
# SQL Server
MSSQL_SA_PASSWORD=YourStrong!Passw0rd
MSSQL_PID=Developer

# DB
DB_HOST=sqlserver        # 'sqlserver' cuando corrés todo con Docker; 'localhost' si solo levantas la DB
DB_NAME=TechnicalTestCoto
DB_USER=SA
DB_PASSWORD=${MSSQL_SA_PASSWORD}
```

> El `docker-compose.yml` usa estas variables para construir e inyectar la conexión de la API como `ConnectionStrings__DefaultConnection`.

### `appsettings.Development.json` (para ejecución local/Visual Studio)

Si corrés la API **local** (F5 en VS o `dotnet run`), configurá la connection string ahí (usando los mismos valores del `.env`, con `DB_HOST=localhost`):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=TechnicalTestCoto;User Id=SA;Password=YourStrong!Passw0rd;Encrypt=True;TrustServerCertificate=True"
  }
}
```

> La API prioriza la variable de entorno `ConnectionStrings__DefaultConnection` (inyectada por Docker); si está vacío, toma `ConnectionStrings:DefaultConnection` de *appsettings*.

---

## Cómo levantar el proyecto

### Opción A — Todo con Docker (API + DB)
Levanta **SQL Server** y la **API compilada dentro de un contenedor**.

```bash
docker compose up -d
```

- **Connection string usada por la API en el contenedor** (derivada de las variables del `.env`)

- **Rebuild** (si cambiaste código):
  ```bash
  docker compose up -d --build
  ```

### Opción B — Solo Base de Datos en Docker (API local)
1) Levantar **solo** la base:
   ```bash
   docker compose up -d sqlserver
   ```
2) Restaurar paquetes NuGet y compilar/correr la API local:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project src/Technical-Test-COTO
   ```
3) **Connection string local** (en `appsettings.Development.json`)

---

## Migraciones / Base de datos

- Aplicar migraciones:
  
  ```bash
  dotnet ef database update -p src/Infrastructure -s src/Technical-Test-COTO
  ```

- Ejecución de migración e inicialización de datos:
  Ejecutar el **endpoint de DbInitializer** para correr migraciones (si no están aplicadas) y sembrar datos base.  
  Ejemplo:
  
  ```bash
  curl -X POST http://localhost:<PUERTO>/api/DbInitializer
  ```

---

## Arquitectura

```
/src
  /Domain
  /Application
  /Infrastructure
  /Technical-Test-COTO
/tests
  /Application.Tests
```

---

## Tecnologías y paquetes utilizados

**Tecnologías**
- .NET 8 (ASP.NET Core Web API)
- Docker & Docker Compose
- SQL Server 2022 (Docker image oficial)
- Entity Framework Core
- AutoMapper
- Swagger
- xUnit
