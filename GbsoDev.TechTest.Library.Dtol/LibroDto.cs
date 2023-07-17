namespace GbsoDev.TechTest.Library.Dtol
{
	public class LibroDto
	{
		public long Isbn { get; set; }
		public string? Titulo { get; set; }
		public int EditorialId { get; set; }
		public string? EditorialNombre { get; set; }
		public string? Sinopsis { get; set; }
		public string? NPaginas { get; set; }
		public List<AutorDto> Autores { get; set; }

		public LibroDto()
		{
			Autores = new List<AutorDto>();
		}
	}
}
