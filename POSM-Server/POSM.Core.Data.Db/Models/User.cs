using System;
using System.Collections.Generic;

#nullable disable

namespace POSM.Core.Data.Db.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefershTokenExpiration { get; set; }
    }
}
