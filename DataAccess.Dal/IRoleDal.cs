using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dal
{
    public interface IRoleDal
    {
        List<RoleDto> Fetch();
        List<RoleDto> FetchForUsername(string username);
        void Insert(string username, RoleDto item);
        void Delete(string username, RoleDto item);
    }
}
