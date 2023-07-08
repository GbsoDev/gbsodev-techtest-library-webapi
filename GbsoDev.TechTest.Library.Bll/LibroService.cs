using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Bll
{
	internal sealed class LibroService : EntityBaseService<Libro, long, ILibroDal>, ILibroService
	{
		public ILibroDal LibroDal { get=> libroDal.Value; }
		public Lazy<ILibroDal> libroDal;

		public LibroService(IServiceProvider serviceProvider, Lazy<ILibroDal> noteDal) : base(serviceProvider)
		{
			this.libroDal = noteDal;
		}

		public List<Libro> GetByAutorId(long id)
		{
			return this.LibroDal.ListLibrosByAutorId(id);
		}
	}
}
