using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using POSM.Core.Business.Operations.Interfaces;
using POSM.Core.Bussines.Model.User;
using POSM.Core.Data.Db;
using POSM.Core.Data.Db.Models;

namespace POSM.Core.Business.Operations.Auth
{
	public class AuthOperator : OperatorBase, IAuthOperator
	{
		public AuthOperator(POSMDbContext context) : base(context)
		{
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
				Password = PasswordHash(registerInput.ConfirmPassword)
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

		private string PasswordHash(string password)
		{
			byte[] salt;
			salt = GenerateSaltNewInstance(16);

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
			byte[] hash = pbkdf2.GetBytes(20);

			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);

			return Convert.ToBase64String(hashBytes);
		}

		private static byte[] GenerateSaltNewInstance(int size)
		{
			using (var generator = RandomNumberGenerator.Create())
			{
				var salt = new byte[size];
				generator.GetBytes(salt);
				return salt;
			}
		}
	}
}
