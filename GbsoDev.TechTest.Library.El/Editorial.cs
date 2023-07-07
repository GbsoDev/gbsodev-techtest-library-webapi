using GbsoDev.TechTest.Library.El.Contracts;
using System;

namespace GbsoDev.TechTest.Library.El
{
	/// <summary>
	/// Entidad para editoriales
	/// </summary>
	public class Editorial : IEntity<int>
	{
		public int Id { get; set; }
		/// <summary>
		/// Nombre o razón social de la editorial
		/// </summary>
		public string? Nombre { get; set; }
		/// <summary>
		/// Sede o ubicaicón de ubicación de la editorial
		/// </summary>
		public string? Sede { get; set; }
		/// <summary>
		/// Libros de la editorial
		/// </summary>
		public List<Libro> Libros { get; set; }

		public DateTime? CreatedDate { get; set; }

		public Editorial()
		{
			Libros = new List<Libro>();
		}
	}
}
