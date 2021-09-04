using System.Security.Claims;
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
		public AuthOperator(POSMDbContext dbContext, IPOSMHasher posmHasher, IOptions<TokenSettings> tokenSettings, ITokenValidator tokenValidator) : base(dbContext, posmHasher, tokenSettings, tokenValidator)
		{
		}

		public TokenResponseModel Login(LoginModel loginInput)
		{
			TokenResponseModel result = new TokenResponseModel { ResponseMessage = "Success" };

			if (string.IsNullOrEmpty(loginInput.Email)
			|| string.IsNullOrEmpty(loginInput.Passowrd))
			{
				result.ResponseMessage = "Invalid Credentials";
				return result;
			}

			User user = dbContext.Users.Where(_ => _.EmailAddress == loginInput.Email).FirstOrDefault();
			if (user == null)
			{
				result.ResponseMessage = "Invalid Credentials";
				return result;
			}

			if (!posmHasher.ValidatePasswordHash(loginInput.Passowrd, user.Password))
			{
				result.ResponseMessage = "Invalid Credentials";
				return result;
			}

			List<UserRole> roles = dbContext.UserRoles.Where(_ => _.UserId == user.UserId).ToList();

			result.AccessToken = tokenValidator.GetJWTAuthKey(user, roles);
			result.RefreshToken = tokenValidator.GenerateRefreshToken();

			user.RefreshToken = result.RefreshToken;
			user.RefershTokenExpiration = DateTime.Now.AddDays(7);
			dbContext.SaveChanges();

			return result;
		}

		public string Register(UserModel registerInput)
		{
			string errorMessage = ResigstrationValidations(registerInput);
			if (!string.IsNullOrEmpty(errorMessage))
			{
				return errorMessage;
			}

			User newUser = new User
			{
				EmailAddress = registerInput.EmailAddress,
				FirstName = registerInput.FirstName,
				LastName = registerInput.LastName,
				Password = posmHasher.PasswordHash(registerInput.ConfirmPassword)
			};

			dbContext.Users.Add(newUser);
			dbContext.SaveChanges();

			// IsharaK[/28/08/2021] : Default role on registration
			UserRole newUserRoles = new UserRole
			{
				Name = "admin",
				UserId = newUser.UserId
			};

			dbContext.UserRoles.Add(newUserRoles);
			dbContext.SaveChanges();

			return "Registration success";
		}

		public TokenResponseModel RenewAccessToken(RenewTokenInputType renewToken)
		{
			TokenResponseModel result = new TokenResponseModel { ResponseMessage = "Success" };

			ClaimsPrincipal principal = tokenValidator.GetClaimsFromExpiredToken(renewToken.AccessToken);

			if (principal == null)
			{
				result.ResponseMessage = "Invalid Token";
				return result;
			}

			string email = principal.Claims.Where(_ => _.Type == "Email").Select(_ => _.Value).FirstOrDefault();
			if (string.IsNullOrEmpty(email))
			{
				result.ResponseMessage = "Invalid Token";
				return result;
			}

			User user = dbContext.Users.Where(_ => _.EmailAddress == email && _.RefreshToken == renewToken.RefreshToken && _.RefershTokenExpiration > DateTime.Now).FirstOrDefault();
			if (user == null)
			{
				result.ResponseMessage = "Invalid Token";
				return result;
			}

			List<UserRole> userRoles = dbContext.UserRoles.Where(_ => _.UserId == user.UserId).ToList();

			result.AccessToken = tokenValidator.GetJWTAuthKey(user, userRoles);
			result.RefreshToken = tokenValidator.GenerateRefreshToken();

			user.RefreshToken = result.RefreshToken;
			user.RefershTokenExpiration = DateTime.Now.AddDays(7);

			dbContext.SaveChanges();

			return result;

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
