using Csla;
using Csla.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !SILVERLIGHT
using DataAccess.Dal;
#endif

namespace Library
{
    [Serializable]
    public class UserExistsCommand : CommandBase<UserExistsCommand>
    {
        public static PropertyInfo<string> UsernameProperty = RegisterProperty<string>(c => c.Username);
        private string Username
        {
            get { return ReadProperty(UsernameProperty); }
            set { LoadProperty(UsernameProperty, value); }
        }

        public static PropertyInfo<bool> UserExistsProperty = RegisterProperty<bool>(c => c.UserExists);
        public bool UserExists
        {
            get { return ReadProperty(UserExistsProperty); }
            private set { LoadProperty(UserExistsProperty, value); }
        }

        public UserExistsCommand()
        {
        }

        public UserExistsCommand(string username)
        {
            Username = username;
        }

#if !SILVERLIGHT
        protected override void DataPortal_Execute()
        {
            using (var ctx = DataAccess.Dal.DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IUserDal>();
                UserExists = dal.Exists(Username);
            }
        }
#endif
    }
}
