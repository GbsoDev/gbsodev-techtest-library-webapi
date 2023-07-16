using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Dal
{
	public sealed class UserDal : BaseDataAccesLayer<Usuario, int>, IUserDal
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
