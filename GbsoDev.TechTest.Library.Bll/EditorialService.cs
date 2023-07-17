using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Bll
{
	public sealed class EditorialService : EntityBaseService<Editorial, int, IEditorialDal>, IEditorialService
	{
		public EditorialService(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
	}
}
