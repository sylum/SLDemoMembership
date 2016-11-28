using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dal
{
    public class UserDto
    {
        public virtual object ProviderUserKey { get; set; } // Guid userGuid = (Guid)Membership.GetUser().ProviderUserKey;
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsOnline { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime LastActivityDate { get; set; }
        public string[] Roles { get; set; }
    }
}
