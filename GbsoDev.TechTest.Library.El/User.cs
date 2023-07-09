using GbsoDev.TechTest.Library.El.Contracts;

namespace GbsoDev.TechTest.Library.El
{
	/// <summary>
	/// Entidad para Usuarios
	/// </summary>
	public class User : IEntity<int>
	{
		public int Id { get; set; }
		/// <summary>
		/// ´Nombre del libro
		/// </summary>
		public string? Nombre { get; set; }
		/// <summary>
		/// Nombre de usuario del libro
		/// </summary>
		public string? UserName { get; set; }
		/// <summary>
		/// password del usuario
		/// </summary>
		public string? Password { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
