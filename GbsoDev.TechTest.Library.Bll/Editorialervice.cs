using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Bll
{
	internal sealed class Editorialervice : EntityBaseService<Editorial, int, IEditorialDal>, IEditorialService
	{
		public Editorialervice(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
	}
}
