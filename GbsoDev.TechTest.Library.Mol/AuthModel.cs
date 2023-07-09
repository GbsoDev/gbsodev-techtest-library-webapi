using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbsoDev.TechTest.Library.Mol
{
	public class AuthModel
	{
		public string? Nombre { get; set; }
		/// <summary>
		/// Nombre de usuario del libro
		/// </summary>
		public string? UserName { get; set; }
		/// <summary>
		/// password del usuario
		/// </summary>
		public string? Password { get; set; }
	}
}
