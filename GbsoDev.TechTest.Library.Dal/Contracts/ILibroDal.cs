using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Dal.Contracts
{
	public interface ILibroDal : IBaseDataAccesLayer<Libro, long>
	{
		List<Libro> ListLibrosByAutorId(long id);
	}
}
