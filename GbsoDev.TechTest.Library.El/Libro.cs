using GbsoDev.TechTest.Library.El.Contracts;

namespace GbsoDev.TechTest.Library.El
{
	/// <summary>
	/// Entidad para libros
	/// </summary>
	public class Libro : IEntity<long>
	{
		public long Id { get; set; }
		/// <summary>
		/// Clave de identidad de la editorial
		/// </summary>
		public int? EditorialId { get; set; }
		/// <summary>
		/// Editorial del libro
		/// </summary>
		public Editorial? Editorial { get; set; }
		/// <summary>
		/// ´Título del libro
		/// </summary>
		public string? Titulo { get; set; }
		/// <summary>
		/// Sinopsis del libro
		/// </summary>
		public string? Sinopsis { get; set; }
		/// <summary>
		/// Páginas del libro
		/// </summary>
		public string? NPaginas { get; set; }
		///// <summary>
		///// LibroHasAutores del libro
		///// </summary>
		public List<AutorHasLibro> LibroHasAutores { get; set; }
		public DateTime? CreatedDate { get; set; }
		public Libro()
		{
			LibroHasAutores = new List<AutorHasLibro>();
		}
	}
}
