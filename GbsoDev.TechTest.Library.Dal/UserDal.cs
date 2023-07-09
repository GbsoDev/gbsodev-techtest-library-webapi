using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;
using Microsoft.EntityFrameworkCore;

namespace GbsoDev.TechTest.Library.Dal
{
	internal sealed class UserDal : BaseDataAccesLayer<User, int>, IUserDal
	{

		public UserDal(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		public bool ValidateUser(string userName, string encript)
		{
			return base.RootContext.Usuarios.Any(x => x.UserName == userName && x.Password == encript);
		}
	}
}
