using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbsoDev.TechTest.Library.El
{
	public class AppSettings
	{
		public Dictionary<string, string> ConnectionStrings { get; set; }
		public AuthOptions AuthOptions { get; set; }
		public CorsOptions[] AllowCors { get; set; } = new CorsOptions[0];

		public string? GetConnectionString(string connection)
		{
			return ConnectionStrings[connection];
		}
	}
}
