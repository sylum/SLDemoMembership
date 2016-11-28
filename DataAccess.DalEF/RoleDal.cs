using DataAccess.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace DataAccess.DalSecurity
{
    public class RoleDal : IRoleDal
    {

        public List<RoleDto> Fetch()
        {
            List<RoleDto> result = new List<RoleDto>();
            foreach (string roleName in Roles.Provider.GetAllRoles())
            {
                result.Add(new RoleDto
                {
                    RoleName = roleName,
                    RoleLevel = Config.RoleDictionary[roleName]
                });
            }
            return result.OrderBy(r => r.RoleLevel).ToList();
        }

        public List<RoleDto> FetchForUsername(string username)
        {

            List<RoleDto> result = new List<RoleDto>();
            foreach (string roleName in Roles.Provider.GetRolesForUser(username))
            {
                result.Add(new RoleDto
                {
                    RoleName = roleName,
                    RoleLevel = Config.RoleDictionary[roleName]
                });
            }
            return result.OrderBy(r => r.RoleLevel).ToList();
        }

        public void Insert(string username, RoleDto item)
        {
            try
            {
                Roles.AddUserToRole(username, item.RoleName);
            }
            catch (Exception ex)
            {
                throw new DataNotFoundException("Role", ex);
            }
        }

        public void Delete(string username, RoleDto item)
        {
            try
            {
                Roles.RemoveUserFromRole(username, item.RoleName);
            }
            catch (Exception ex)
            {
                throw new DataNotFoundException("Role", ex);
            }
        }
    }
}
