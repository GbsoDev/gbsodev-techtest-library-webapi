using GbsoDev.TechTest.Library.El;

namespace GbsoDev.TechTest.Library.Dal.Contracts
{
	public interface IUserDal : IBaseDataAccesLayer<User, int>
	{
		bool ValidateUser(string userName, string encript);
	}
}
