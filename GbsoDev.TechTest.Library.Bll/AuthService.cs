using FluentValidation;
using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Security.Cryptography;
using System.Text;

namespace GbsoDev.TechTest.Library.Bll
{
	internal sealed class AuthService : EntityBaseService<User, int, IUserDal>, IAuthService
	{
		private AppSettings AppSettings { get; }

		public AuthService(IServiceProvider serviceProvider, IOptions<AppSettings> appSettings) : base(serviceProvider)
		{
			AppSettings = appSettings.Value;
		}

		public AuthResponse? ValidateLogin(User user)
		{
			if (!string.IsNullOrWhiteSpace(user.UserName) && !string.IsNullOrWhiteSpace(user.Password))
			{
				var encript = GetSHA1(user.Password);
				if(MainDal.ValidateUser(user.UserName, encript)) {
					var startDateTime = DateTime.UtcNow;
					var expireDateTime = startDateTime.Add(TimeSpan.FromHours(5));
					var token = GenerateToken(startDateTime, user, expireDateTime);
					return new AuthResponse
					{
						Token = token,
						ExpireAt = expireDateTime
					};
				}
			}
			return null;
		}

		private string GenerateToken(DateTime startDateTime, User user, DateTime expireDateTime)
		{

			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(
					JwtRegisteredClaimNames.Iat,
					new DateTimeOffset(startDateTime).ToUniversalTime().ToUnixTimeSeconds().ToString(),
					ClaimValueTypes.Integer64
				)
            };
			claims.AddRange(AppSettings.AuthOptions.Roles.Select(x => new Claim("Roles", x)));
			var signingCredentials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.AuthOptions.SigningKey)),
				SecurityAlgorithms.HmacSha256Signature
			);

			var jwt = new JwtSecurityToken(
				issuer: AppSettings.AuthOptions.Issuer,
				audience: AppSettings.AuthOptions.Audience,
				claims: claims,
				notBefore: startDateTime,
				expires: expireDateTime,
				signingCredentials: signingCredentials
			);

			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
			return encodedJwt;
		}

		public static string GetSHA1(String texto)
		{
			SHA1 sha1 = SHA1.Create();
			byte[] textOriginal = Encoding.Default.GetBytes(texto);
			byte[] hash = sha1.ComputeHash(textOriginal);
			StringBuilder cadena = new StringBuilder();
			foreach (byte i in hash)
			{
				cadena.AppendFormat("{0:x2}", i);
			}
			return cadena.ToString();
		}

	}
}
