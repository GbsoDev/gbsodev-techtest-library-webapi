using GbsoDev.TechTest.Library.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GbsoDev.TechTest.Library.Wal
{
	public class DbContextFactory : IDesignTimeDbContextFactory<RootContext>
	{
		/// <summary>
		/// Con la consola del administrador de paquetes, podremos ejecutar Add-Migration "Drop-Database ,Get-DbContext ,Scaffold-DbContext
		///Script-Migrations ,Update-Database", Recibe variables de entorno indicando en optionsbuilder Environment.GetEnvironmentVariable(""),
		///o solo pasar la cadena de conexion local estrutura ->"Server=localhost;Port=5432;Database=namedatabase;User Id=user;Password=password;"
		/// Get-Help about_EntityFrameworkCore -> comando para visualizar las ayudas
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public RootContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<RootContext>();
			var rooConnectionString = Utils.BuildConfiguration().GetConnectionString(Utils.CONNECTION_MIGRATIONS_NAME);
			optionsBuilder.UseSqlServer(rooConnectionString);
			return new RootContext(optionsBuilder.Options);
		}
	}
}

