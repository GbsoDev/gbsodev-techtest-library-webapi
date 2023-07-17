using GbsoDev.TechTest.Library.El;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using GbsoDev.TechTest.Library.Mol;

namespace GbsoDev.TechTest.Library.Wal
{
	public static class Provider
	{
		public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration, out AppSettings appSettings)
		{
			appSettings = new AppSettings();
			var builder = new ConfigurationBuilder();
			var combinedConfiguration = builder.AddConfiguration(configuration)
				.AddJsonFile(Utils.DEVELOPMENT_FILE_CONFIGURATION_PATH, true, true)
				.AddJsonFile(Utils.FILE_CONFIGURATION_PATH, false, true)
				.Build();
			combinedConfiguration.Bind(appSettings);
			services.AddSingleton(appSettings);
			return services;
		}

		public static IServiceCollection AddAuthentication(this IServiceCollection services, AppSettings appSettings)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
			{
				o.Audience = appSettings.AuthOptions.Audience;
				o.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = appSettings.AuthOptions.Issuer,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.AuthOptions.SigningKey))
				};
			});
			return services;
		}
		
		public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
		{
			return services.AddSingleton(
				new MapperConfiguration(mc => mc.AddProfile(typeof(AutoMapperConfiguration))).CreateMapper()
			);
		}

	}
}
