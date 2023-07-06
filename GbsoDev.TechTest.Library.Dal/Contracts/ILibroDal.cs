using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Dal.Contracts
{
	public interface ILibroDal : IBaseDataAccesLayer<Libro, int>
	{
		List<Libro> ListLibrosByAutorId(int id);
	}
}
