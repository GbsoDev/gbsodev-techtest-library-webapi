using FluentValidation;
using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.Bll.ValidationRules;
using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.El.Contracts;

namespace GbsoDev.TechTest.Library.Bll
{
	public sealed class LibroService : EntityBaseService<Libro, long, ILibroDal>, ILibroService
	{
		public LibroService(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		/// <summary>
		/// lista los libros de un autor
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public List<Libro> GetByAutorId(long id)
		{
			return this.MainDal.ListLibrosByAutorId(id);
		}
	}
}
