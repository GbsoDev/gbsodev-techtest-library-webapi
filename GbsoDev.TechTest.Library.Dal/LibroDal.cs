using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;
using Microsoft.EntityFrameworkCore;

namespace GbsoDev.TechTest.Library.Dal
{
	internal sealed class LibroDal : BaseDataAccesLayer<Libro, int>, ILibroDal
	{	
		public LibroDal(IServiceProvider serviceProvider) : base(serviceProvider)
		{

		}

		public List<Libro> ListLibrosByAutorId(int id)
		{
			return RootContext.Autores
				.Include(x => x.Libros)
				.Where(x => x.Id == id)
				.SelectMany(x=> x.Libros).ToList();
		}
	}
}
