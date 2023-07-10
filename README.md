# GbsoDev.TechTest.Library.Webapi

### Herramientas requeridas
Docker Desktop
Visual Studio 2022
dotnet-ef
  `dotnet tool install --global dotnet-ef`
  
### Crear migraciones
  `dotnet ef migrations add [migration-name] --startup-project GbsoDev.TechTest.Library.Wal --project GbsoDev.TechTest.Library.Dal`

### Actualizar base de datos
  `dotnet ef database update [migration-name] --startup-project GbsoDev.TechTest.Library.Wal --project GbsoDev.TechTest.Library.Dal`

### Ejecutar en entorno de desarrollo
Seleccionar el proyecto Docker-Compose como proyecto de inicio en modo Debug, esto un contenedor para la aplicación y otro de sql-server para la base de datos

### Generar Versión Release

  en la carpeta de la solución ejecutar el siguiente comando con power shell, esto compilará la aplicáción y creará la imagen en docker
  `docker-compose -f "docker-compose.yml" -p gbsodev-techtest-library build`

### Construir imagen docker (esto no es necesario si se usó el parámetro build en el comando de generar versión)
  `docker --pull --rm -f "GbsoDev.TechTest.Library.Wal/Dockerfile" -t gbsodev/gbsodev-techtest-library-web:release "."`

### Publicar imagen docker en docker hub
  `docker push gbsodev/gbsodev-techtest-library-webapi:release`

# Puesta en marcha
#### Una ves publicadas las imagenes de los contenedores se puede proceder a la instalación en el servidor

### Requisitos

#### fichero docker-compose
  ./docker-compose.prod.yml

#### ruta con certificados https (modificar contraseña en el docker compose)
  ./https/aspnetapp.pfx

#### ruta con backup de base de datos
  ./backup/techtest_library_database.bak

### Restaurar base de datos
  user:sa
  password:pass@library-database93
  server:localhost
en el mismo

#### por último correr el script con power shell en la ruta del fichero docker y los assets

  `docker-compose -f "docker-compose.prod.yml" -p gbsodev-techtest-library up -d`
### si es el primer despliegue a produccción debe restaurar el backup de la base de datos que encontrará en la carpeta assets del proyecto api
