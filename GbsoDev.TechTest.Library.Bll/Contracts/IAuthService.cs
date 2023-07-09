using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Bll.Contracts
{
	public interface IAuthService : IEntityBaseService<Usuario, int>
	{
		AuthResponse? ValidateLogin(Usuario user);
	}
}
