namespace GbsoDev.TechTest.Library.Mol
{
	public class UserModel
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
