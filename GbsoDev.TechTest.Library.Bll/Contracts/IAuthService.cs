using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Bll.Contracts
{
	public interface IAuthService : IEntityBaseService<User, int>
	{
		AuthResponse? ValidateLogin(User user);
	}
}
