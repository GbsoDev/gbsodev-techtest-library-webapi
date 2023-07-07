# GbsoDev.TechTest.Library

# Herramientas requeridas
	Docker Desktop
	Visual Studio 2022
	dotnet-ef
		- dotnet tool install --global dotnet-ef
# Crear migraciones
	dotnet ef migrations add [migration-name] --startup-project GbsoDev.TechTest.Library.Wal --project GbsoDev.TechTest.Library.Dal
# Actualizar base de datos
	dotnet ef database update [migration-name] --startup-project GbsoDev.TechTest.Library.Wal --project GbsoDev.TechTest.Library.Dal