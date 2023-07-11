namespace GbsoDev.TechTest.Library.Wal
{
	public static class Utils
	{
		public const string CONNECTION_ROOT_NAME = "techtest.library.database";
		public const string CONNECTION_MIGRATIONS_NAME = "techtest.library.database.migrations";
		public const string FILE_CONFIGURATION_PATH = "assets/appsettings.json";
		public const string DEVELOPMENT_FILE_CONFIGURATION_PATH = "assets/appsettings.Development.json";

		public static IConfigurationRoot BuildConfiguration()
		{
			var configurationResult = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
#if !DEBUG
				.AddJsonFile(FILE_CONFIGURATION_PATH, optional: false, reloadOnChange: true)
#else
				.AddJsonFile(DEVELOPMENT_FILE_CONFIGURATION_PATH, optional: false, reloadOnChange: true)
#endif
				.Build();
			return configurationResult;
		}
	}
}
