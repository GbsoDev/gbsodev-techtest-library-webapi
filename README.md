# GbsoDev.TechTest.Library

## Herramientas de desarrollo
- Visual Studio 2022+
- dotnet-ef
```
dotnet tool install --global dotnet-ef
```
- Docker Desktop
  - para abilitar la virtualización de contenedores, consulte la sección [Docker Descktop](#docker-desktop)
  
## Comandos de desarrollo
### Crear migraciones
```
dotnet ef migrations add [migration-name] --startup-project GbsoDev.TechTest.Library.Wal --project GbsoDev.TechTest.Library.Dal
```

### Actualizar base de datos
```
dotnet ef database update [new-migration-name] --startup-project GbsoDev.TechTest.Library.Wal --project GbsoDev.TechTest.Library.Dal
```

## Poner en marcha en entorno de desarrollo
- Seleccionar el proyecto Docker-Compose como proyecto de principal en modo Debug, esto pondra en marcha un contenedor de .net core para la aplicación y otro de sql-server para la base de datos
- Alternativamente puede seleccionar el proyecto GbsoDev.TechTest.Library.Wal como proyecto principal, esto iniciará la depuración de la aplicación sin docker y en otro puerto, en cuyo caso deberá ajustar el puerto de la api en la configuración de la plicación front y conectar a otra base de datos o poner en marcha el contenedor de la base de datos por otros medios.

## Generar Versión Release
- An la carpeta de la solución ejecutar el siguiente comando con PowerShell, esto compilará la aplicáción y creará la imagen en docker
```
docker-compose --pull -f "docker-compose.yml" build
```
- Alternativamente puede usar el siguiente comando
```
docker build --pull --rm -f "GbsoDev.TechTest.Library.Wal/Dockerfile" -t gbsodev/gbsodev-techtest-library-web:release "."
```

## Publicar imagen de versión en docker hub 
**NOTA:** esto requiere iniciar sesión en docker hub con docker desktop
```
docker push gbsodev/gbsodev-techtest-library-webapi:release
```

## Puesta en marcha en entorno productivo
para efectos de la prueba, las instrucciones a continuación, estan dirgidas a un entorno on-premise
### Requisitos del servidor
- Habilitar la virtualización de contenedores
  - consulte la sección [Docker Descktop](#docker-desktop)
### Puesta en marcha de la aplcación
1. Duscar ruta donde se almacenarán los assets de la aplicación, ejemplo `C:\publish\`
1. Decomprimir el contenido del archivo assets.zip que se le envió por correo en la ruta, ejemplo: `C:\publish\assets`, o copiarlos de la ruta `./assets` que se encuentra la raíz del proyecto en el repositorio Git [assets](https://github.com/GbsoDev/gbsodev-techtest-library-webapi/tree/master/assets). 
1. Ingresar a la ruta donde copió o descomprimió los archivos, y abrir una terminal de PowerShell combinando `Shift+Click Derecho` y seleccionando la opción `"Abrir la ventana de PowerShell aquí"`
1. Ya en la ventana de PowerShell debería ver algo como `PS C:\publish\assets>`
1. Ahora ejecute los comandos que se muestran a continuación en orden
```
docker-compose -f "docker-compose.prod.yml" pull
```
```
docker-compose -f "docker-compose.prod.yml" -p gbsodev-techtest-library up -d
```
**NOTA:** para instalar actualización, basta con volver a ejecutar mismo comando además de, de ser necesario, acutalizar la base de datos
## Restaurar base de datos
- si es la primera vez que instala la aplicación, deberá restaurar el backup de la base de datos proporcionado dentro de la ruta de los assets previamente mencionados, para lo cual se requiere un software para conectarse a la base de datos a preferencia del usuario, se recomienda usar [Azure Data Studio](https://learn.microsoft.com/es-es/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver16&tabs=redhat-install,redhat-uninstall)
1. Conectar con el servidor de base de datos
   * Servidor: localhost:1433
   * Usuario: sa
   * Contraseña: pass@library-database93
1. Restaurar el backup que se encuentra dentro del servidor de base de datos en la ruta /deb-backup
   1. Click derecho en servidor
   1. Manage
   1. Restore
   1. Restore from : backup file
   1. Backup file path : /deb-backup/techtest_library_database.bak
   1. Click en boton restore

**Eso es todo!** Ahora se puede acceder a la aplicación atravez del navegador web con la ruta [localhost:8080](http://localhost:8080)
* Usuario: admin
* Contraseña: admin
# Docker Desktop:
Para correr contenedores en el equipo, configure el equipo o servidor en siguiente orden
  1. Habilite la virtualización del procesador `disponible en la bios `
  1. Habilite la característica de Windows Hyper-V. opción `activar o desactivar caracteristicas de Windows`
  1. Instale WSL mediante el comando de PowerShell
     ```
     wsl --install
     ```
  1. Configure la versión 2 de WSL con el comando de PowerShell
     ```
     wsl --set-version 2
     ```
  1 . Instalar Docker Desktop para Windows [Aquí](https://www.docker.com/products/docker-desktop/)
