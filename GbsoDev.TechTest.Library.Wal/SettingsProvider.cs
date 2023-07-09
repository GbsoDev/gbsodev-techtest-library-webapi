using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Wal
{
	public static class SettingsProvider
	{
		public static IServiceCollection AddSetting(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<AppSettings>(configuration);
			return services;
		}
	}
}
