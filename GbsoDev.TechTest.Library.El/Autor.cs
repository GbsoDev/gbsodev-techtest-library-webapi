using GbsoDev.TechTest.Library.El.Contracts;
using System.Collections.Generic;

namespace GbsoDev.TechTest.Library.El
{
	/// <summary>
	/// Entidad para autores
	/// </summary>
	public class Autor : IEntity<int>
	{
		public int Id { get; set; }
		/// <summary>
		/// Nombre del autor
		/// </summary>
		public string? Nombre { get; set; }
		/// <summary>
		/// Apellidos del autor
		/// </summary>
		public string? Apellidos { get; set; }
		/// <summary>
		/// Libros escritos por el autor
		/// </summary>
		public List<Libro> Libros { get; set; }
		public DateTime? CreatedDate { get; set; }
		public Autor()
		{
			Libros = new List<Libro>();
		}
	}
}
