using GbsoDev.TechTest.Library.El.Contracts;

namespace GbsoDev.TechTest.Library.El
{
	/// <summary>
	/// Entidad de relación de muchos a muchos
	/// </summary>
	public class AutorHasLibro : IEntity<long>
	{
		public long Id { get; set; }
		public int AutorId { get; set; }
		public Autor? Autor { get; set; }
		public long LibroId { get; set; }
		public Libro? Libro { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
