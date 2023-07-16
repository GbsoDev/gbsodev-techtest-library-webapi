using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Dal
{
	public sealed class AutorDal : BaseDataAccesLayer<Autor, int>, IAutorDal
	{	
		public AutorDal(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
	}
}