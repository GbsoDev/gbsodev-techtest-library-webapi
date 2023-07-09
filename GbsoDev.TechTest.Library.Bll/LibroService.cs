using FluentValidation;
using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.Bll.ValidationRules;
using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.El.Contracts;

namespace GbsoDev.TechTest.Library.Bll
{
	internal sealed class LibroService : EntityBaseService<Libro, long, ILibroDal>, ILibroService
	{
		public LibroService(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		public List<Libro> GetByAutorId(long id)
		{
			return this.MainDal.ListLibrosByAutorId(id);
		}
	}
}
