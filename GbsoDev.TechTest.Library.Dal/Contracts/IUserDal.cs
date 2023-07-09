using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Dal.Contracts
{
	public interface IUserDal : IBaseDataAccesLayer<Usuario, int>
	{
		bool ValidateUser(string userName, string encript);
	}
}
