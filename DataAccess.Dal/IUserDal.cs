using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dal
{
    public interface IUserDal
    {
        UserDto Fetch(string username, string password);
        UserDto Fetch(string username);
        UserDto Fetch(Guid iduser);
        List<UserDto> Fetch();
        bool Exists(string username);
        bool EmailExists(string email);
        void UnlockUser(string username);
        void ResetPassword(string username, string newPassword);
        void Insert(UserDto item);
        void Update(UserDto item);
        void Delete(string username);
        void Delete(Guid iduser);
    }
}
