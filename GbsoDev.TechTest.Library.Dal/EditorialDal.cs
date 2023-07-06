using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Dal
{
	internal sealed class EditorialDal : BaseDataAccesLayer<Editorial, int>, IEditorialDal
	{	
		public EditorialDal(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
	}
}