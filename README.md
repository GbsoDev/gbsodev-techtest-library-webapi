##### GbsoDev.TechTest.Library

##### Herramientas requeridas
Docker Desktop
Visual Studio 2022
dotnet-ef
run `dotnet tool install --global dotnet-ef`

##### Crear migraciones
run dotnet ef migrations add [migration-name] --startup-project GbsoDev.TechTest.Library.Wal --project GbsoDev.TechTest.Library.Dal

##### Actualizar base de datos
run dotnet ef database update [migration-name] --startup-project GbsoDev.TechTest.Library.Wal --project GbsoDev.TechTest.Library.Dal

##### Ejecutar en entorno de desarrollo
Seleccionar el proyecto Docker-Compose como proyecto de inicio en modo Debug, esto un contenedor para la aplicación y otro de sql-server para la base de datos

##### Generar Versión Release
#en la carpeta de la solución ejecutar el siguiente comando con power shell, esto compilará la aplicáción y creará la imagen en docker
run `docker-compose -f "docker-compose.yml" -f "docker-compose.prod.yml" -p gbsodev-techtest-library build`

##### Construir imagen docker (esto no es necesario si se usó el parámetro build en el comando de generar versión)
run `docker build --pull --rm -f "Dockerfile" -t gbsodev/gbsodev-techtest-library-web:release "."`

##### Publicar imagen docker en docker hub
run `docker push gbsodev/gbsodev-techtest-library-webapi:release`

#### Una ves publicadas las imagenes de los contenedores se puede proceder a la instalación en el servidor
run `docker-compose -f "docker-compose.prod.yml" -p gbsodev-techtest-library up`
#el  fichero docker-compose.prod.yml se encuentra en la ruta principal del código fuente de la api
#### Construir imagen de base de datos release 
# descargar imagen sql
run `docker pull mcr.microsoft.com/mssql/server:2022-latest`
# 
run `docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=pass@library-database93" -p 1433:1433 --name library-sql-container --hostname library-database -d -it mcr.microsoft.com/mssql/server:2022-latest`
