using Csla;
using Csla.Serialization;
using Csla.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !SILVERLIGHT
using DataAccess.Dal;
#endif

namespace Library
{
    [Serializable]
    public class PTIdentity : CslaIdentityBase<PTIdentity>
    {
       

        public static void GetPTIdentity(string login, string password, EventHandler<DataPortalResult<PTIdentity>> callback)
        {
            try
            {
                DataPortal.BeginFetch<PTIdentity>(new UsernameCriteria(login, password), callback);
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }
        }

       
        public static event Action UpdateUser;
        private static void OnUpdateUser()
        {
            if (UpdateUser != null)
                UpdateUser();
        }

#if !SILVERLIGHT

        public static PTIdentity GetPTIdentity(string username, string password)
        {
            return DataPortal.Fetch<PTIdentity>(new UsernameCriteria(username, password));
        }

        internal static PTIdentity GetPTIdentity(string username)
        {
            return DataPortal.Fetch<PTIdentity>(username);
        }

        private void DataPortal_Fetch(string username)
        {
            UserDto data = null;
            using (var ctx = DataAccess.Dal.DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IUserDal>();
                try
                {
                    data = dal.Fetch(username);
                }
                catch (DataNotFoundException)
                {
                    data = null;
                }
                LoadUser(data);
            }
        }

        private void DataPortal_Fetch(UsernameCriteria criteria)
        {
            UserDto data = null;
            using (var ctx = DataAccess.Dal.DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IUserDal>();
                try
                {
                    data = dal.Fetch(criteria.Username, criteria.Password);
                    LoadUser(data);
                }
                catch (Exception ex)
                {
                    data = null;
                    LoadUser(data);
                    throw ex;
                }
            }
        }

        private void LoadUser(UserDto data)
        {
            if (data != null)
            {
                base.Name = data.Username;
                base.IsAuthenticated = true;
                base.AuthenticationType = "Membership";
                base.Roles = new Csla.Core.MobileList<string>(data.Roles);

                try
                {
                    var user = UserEdit.GetUser(data.Username);

                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                base.Name = string.Empty;
                base.IsAuthenticated = false;
                base.AuthenticationType = string.Empty;
                base.Roles = new Csla.Core.MobileList<string>();
            }
        }

#endif
    }
}
