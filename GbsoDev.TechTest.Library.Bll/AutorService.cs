using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Bll
{
	public sealed class AutorService : EntityBaseService<Autor, int, IAutorDal>, IAutorService
	{
		public AutorService(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
	}
}
