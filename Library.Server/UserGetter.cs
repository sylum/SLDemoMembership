using Csla;
using Csla.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class UserGetter : ReadOnlyBase<UserGetter>
    {
        #region Business Properties

        public static PropertyInfo<UserEdit> UserProperty = RegisterProperty<UserEdit>(c => c.User);
        public UserEdit User
        {
            get { return GetProperty(UserProperty); }
            private set { LoadProperty(UserProperty, value); }
        }

        //public static PropertyInfo<RoleList> RoleListProperty = RegisterProperty<RoleList>(c => c.RoleList);
        //public RoleList RoleList
        //{
        //    get { return GetProperty(RoleListProperty); }
        //    private set { LoadProperty(RoleListProperty, value); }
        //}

        #endregion

        #region Factory

        public static void CreateNewUser(EventHandler<DataPortalResult<UserGetter>> callback)
        {

            DataPortal.BeginFetch<UserGetter>(new Criteria { Username = string.Empty, IdUser = Guid.Empty }, (o, e) =>
            {
                if (e.Error != null)
                    throw e.Error;
                callback(o, e);
            });
        }

        public static void GetExistingUser(string username, EventHandler<DataPortalResult<UserGetter>> callback)
        {
            DataPortal.BeginFetch<UserGetter>(new Criteria { Username = username, IdUser = Guid.Empty }, (o, e) =>
            {
                if (e.Error != null)
                    throw e.Error;
                callback(o, e);
            });
        }

        public static void GetExistingUser(Guid idUser, EventHandler<DataPortalResult<UserGetter>> callback)
        {
            DataPortal.BeginFetch<UserGetter>(new Criteria { Username = string.Empty, IdUser = idUser }, (o, e) =>
            {
                if (e.Error != null)
                    throw e.Error;
                callback(o, e);
            });
        }

#if !SILVERLIGHT && !NETFX_CORE

        public static UserGetter CreateNewUser()
        {
            return DataPortal.Fetch<UserGetter>(new Criteria { Username = string.Empty, IdUser = Guid.Empty });
        }

        public static UserGetter GetExistingUser(string username)
        {
            return DataPortal.Fetch<UserGetter>(new Criteria { Username = username, IdUser = Guid.Empty });
        }

        public static UserGetter GetExistingUser(Guid idUser)
        {
            return DataPortal.Fetch<UserGetter>(new Criteria { Username = string.Empty, IdUser = idUser });
        }

#endif

        #endregion

        #region Data

#if !SILVERLIGHT && !NETFX_CORE
        private void DataPortal_Fetch(Criteria criteria)
        {
            //RoleList = RoleList.GetList(true);

            if ((criteria.Username == string.Empty) && (criteria.IdUser == Guid.Empty))
            {
                User = UserEdit.NewUser();
                return;
            }

            if ((criteria.Username != string.Empty) && (criteria.IdUser == Guid.Empty))
            {
                User = UserEdit.GetUser(criteria.Username);
                return;
            }

            if ((criteria.Username == string.Empty) && (criteria.IdUser != Guid.Empty))
            {
                User = UserEdit.GetUser(criteria.IdUser);
                return;
            }
        }
#endif

        #endregion

        #region Criteria
        [Serializable]
        public class Criteria : CriteriaBase<Criteria>
        {
            public static readonly PropertyInfo<string> UsernameProperty = RegisterProperty<string>(c => c.Username);
            public string Username
            {
                get { return ReadProperty(UsernameProperty); }
                set { LoadProperty(UsernameProperty, value); }
            }

            public static readonly PropertyInfo<Guid> IdUserProperty = RegisterProperty<Guid>(c => c.IdUser);
            public Guid IdUser
            {
                get { return ReadProperty(IdUserProperty); }
                set { LoadProperty(IdUserProperty, value); }
            }
        }
        #endregion
    }
}
