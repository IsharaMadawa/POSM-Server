using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSM.Core.Bussines.Model.User
{
	public class UserModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
