using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Bll.Contracts
{
	public interface ILibroService : IEntityBaseService<Libro, long>
	{
		List<Libro> GetByAutorId(long id);
	}
}
