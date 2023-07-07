using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Bll.Contracts
{
	public interface ILibroService : IEntityBaseService<Libro, int>
	{
		List<Libro> GetByAutorId(int id);
	}
}
