using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbsoDev.TechTest.Library.El
{
	public class AuthResponse
	{
		public string? Token { get; set; }
		public DateTime? ExpireAt { get; set; }
	}
}
