using POSM.Core.Bussines.Model.Login;
using POSM.Core.Bussines.Model.User;
using POSM.Core.Data.Db;

namespace POSM.Core.Business.Operations.Interfaces
{
	public interface IAuthOperator
	{
		string Register(UserModel registerInput);

		string Login(LoginModel loginInput);
	}
}
