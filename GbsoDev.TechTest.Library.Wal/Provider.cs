using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.El;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GbsoDev.TechTest.Library.Wal
{
	public static class Provider
	{
		public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration, out AppSettings appSettings)
		{
			appSettings = new AppSettings();
			configuration.Bind(appSettings);
			services.AddSingleton(appSettings);
			return services;
		}

		public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration, AppSettings appSettings)
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
	}
}
