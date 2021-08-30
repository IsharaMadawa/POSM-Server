using System;
using System.Collections.Generic;

#nullable disable

namespace POSM.Core.Data.Db.Models
{
    public partial class UserRole
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
