using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using POSM.Core.Business.Operations.Interfaces;
using POSM.Core.Bussines.Model.Login;
using POSM.Core.Bussines.Model.User;
using POSM.Core.Data.Db.Models;
using POSM.Fx.Cryptography.Interfaces;
using POSM.FX.Security.Interfaces;
using POSM.FX.Security.OpenIDConnect;

namespace POSM.Core.Business.Operations.Auth
{
	public class AuthOperator : OperatorBase, IAuthOperator
	{
		public AuthOperator(POSMDbContext context, IPOSMHasher posmHasher, IOptions<TokenSettings> tokenSettings, ITokenValidator tokenValidator) : base(context, posmHasher, tokenSettings, tokenValidator)
		{
		}

		public string Login(LoginModel loginInput)
		{
			if (string.IsNullOrEmpty(loginInput.Email)
			|| string.IsNullOrEmpty(loginInput.Passowrd))
			{
				return "Invalid Credentials";
			}

			var user = context.Users.Where(_ => _.EmailAddress == loginInput.Email).FirstOrDefault();
			if (user == null)
			{
				return "Invalid Credentials";
			}

			if (!posmHasher.ValidatePasswordHash(loginInput.Passowrd, user.Password))
			{
				return "Invalid Credentials";
			}

			var roles = context.UserRoles.Where(_ => _.UserId == user.UserId).ToList();

			return tokenValidator.GetJWTAuthKey(user, roles);
		}

		public string Register(UserModel registerInput)
		{
			string errorMessage = ResigstrationValidations(registerInput);
			if (!string.IsNullOrEmpty(errorMessage))
			{
				return errorMessage;
			}

			var newUser = new User
			{
				EmailAddress = registerInput.EmailAddress,
				FirstName = registerInput.FirstName,
				LastName = registerInput.LastName,
				Password = posmHasher.PasswordHash(registerInput.ConfirmPassword)
			};

			context.Users.Add(newUser);
			context.SaveChanges();

			// IsharaK[/28/08/2021] : Default role on registration
			var newUserRoles = new UserRole
			{
				Name = "admin",
				UserId = newUser.UserId
			};

			context.UserRoles.Add(newUserRoles);
			context.SaveChanges();

			return "Registration success";
		}

		private string ResigstrationValidations(UserModel registerInput)
		{
			if (string.IsNullOrEmpty(registerInput.FirstName))
			{
				return "First name can't be empty";
			}

			if (string.IsNullOrEmpty(registerInput.LastName))
			{
				return "Last name can't be empty";
			}

			if (string.IsNullOrEmpty(registerInput.Password)
				|| string.IsNullOrEmpty(registerInput.ConfirmPassword))
			{
				return "Password Or ConfirmPasswor Can't be empty";
			}

			if (registerInput.Password != registerInput.ConfirmPassword)
			{
				return "Invalid confirm password";
			}

			string emailRules = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
			if (!Regex.IsMatch(registerInput.EmailAddress, emailRules))
			{
				return "Not a valid email";
			}

			// IsharaK[/28/08/2021] : 
			// atleast one lower case letter
			// atleast one upper case letter
			// atleast one special character
			// atleast one number
			// atleast 8 character length
			string passwordRules = @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
			if (!Regex.IsMatch(registerInput.Password, passwordRules))
			{
				return "Not a valid password";
			}

			return string.Empty;
		}
	}
}
