# GbsoDev.TechTest.Library

# Herramientas requeridas
	Docker Desktop
	Visual Studio 2022
	dotnet-ef
		- dotnet tool install --global dotnet-ef
# Crear migraciones
	- dotnet ef migrations add [migration-name] --startup-project GbsoDev.TechTest.Library.Wal --project GbsoDev.TechTest.Library.Dal
# Actualizar base de datos
	- dotnet ef database update [migration-name] --startup-project GbsoDev.TechTest.Library.Wal --project GbsoDev.TechTest.Library.Dal
# Ejecutar en entorno de desarroll
	Seleccionar el proyecto Docker-Compose como proyecto de inicio en modo Debug, esto un contenedor para la aplicación y otro de sql-server para la base de datos
# Generar Versión Release
	compilar en modo release
	en la carpeta de la solución ejecutar el siguiente comando con power shell, esto compilará la aplicáción y creará la imagen en docker
 	- docker-compose -f "docker-compose.yml" -f "docker-compose.prod.yml" -p gbsodev-techtest-library build
# Publicar imagen docker en docker hub
	- docker push gbsodev/gbsodev.techtest.library.webapi:latest