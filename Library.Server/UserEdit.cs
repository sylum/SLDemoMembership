using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using Csla.Serialization;
#if !SILVERLIGHT
using DataAccess.Dal;
#endif

namespace Library
{
    [Serializable]
    public class UserEdit : BusinessBase<UserEdit>
    {
        #region Business Properties

        public static PropertyInfo<Guid> providerUserKeyProperty = RegisterProperty<Guid>(c => c.ProviderUserKey);
        [Display(Name = "providerUserKey")]
        public Guid ProviderUserKey
        {
            get { return GetProperty(providerUserKeyProperty); }
            private set { LoadProperty(providerUserKeyProperty, value); }
        }

        public static PropertyInfo<string> UsernameProperty = RegisterProperty<string>(c => c.Username);
        [Display(Name = "UserName")]
        [Required]
        public string Username
        {
            get { return GetProperty(UsernameProperty); }
            set { SetProperty(UsernameProperty, value); }
        }

        public static PropertyInfo<string> PasswordProperty = RegisterProperty<string>(c => c.Password);
        [Display(Name = "Password")]
        [Required]
        [StringLength(128, MinimumLength = 6)]
        public string Password
        {
            get { return GetProperty(PasswordProperty); }
            set { SetProperty(PasswordProperty, value); }
        }

        public static PropertyInfo<string> EmailProperty = RegisterProperty<string>(c => c.Email);
        [Display(Name = "Email")]
        //[Required]
        //[RegularExpression(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$")]
        public string Email
        {
            get { return GetProperty(EmailProperty); }
            set { SetProperty(EmailProperty, value); }
        }

        public static PropertyInfo<DateTime> CreationDateProperty = RegisterProperty<DateTime>(c => c.CreationDate);
        [Display(Name = "CreationDate")]
        public DateTime CreationDate
        {
            get { return GetProperty(CreationDateProperty); }
            private set { LoadProperty(CreationDateProperty, value); }
        }

        public static PropertyInfo<DateTime> LastActivityDateProperty = RegisterProperty<DateTime>(c => c.LastActivityDate);
        [Display(Name = "LastActivityDate")]
        public DateTime LastActivityDate
        {
            get { return GetProperty(LastActivityDateProperty); }
            private set { LoadProperty(LastActivityDateProperty, value); }
        }

        public static PropertyInfo<bool> IsOnlineProperty = RegisterProperty<bool>(c => c.IsOnline);
        [Display(Name = "IsOnline")]
        public bool IsOnline
        {
            get { return GetProperty(IsOnlineProperty); }
            set { LoadProperty(IsOnlineProperty, value); }
        }

        public static PropertyInfo<bool> IsLockedOutProperty = RegisterProperty<bool>(c => c.IsLockedOut);
        [Display(Name = "IsLockedOut")]
        public bool IsLockedOut
        {
            get { return GetProperty(IsLockedOutProperty); }
            private set { LoadProperty(IsLockedOutProperty, value); }
        }

        //public static readonly PropertyInfo<RoleList> RolesProperty = RegisterProperty<RoleList>(c => c.Roles);
        //public RoleList Roles
        //{
        //    get
        //    {
        //        if (!(FieldManager.FieldExists(RolesProperty)))
        //            LoadProperty(RolesProperty, DataPortal.CreateChild<RoleList>());
        //        return GetProperty(RolesProperty);
        //    }
        //    set { LoadProperty(RolesProperty, value); }
        //}

        public override string ToString()
        {
            return Username;
        }

        #endregion

        #region MethodAuthorization

        #endregion

        #region Business rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new UserExistsRule(UsernameProperty));

            //BusinessRules.AddRule(new NotEmptyPrimarySiteRule() { PrimaryProperty = RolesProperty, Priority = 0 });
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {

        }

        protected override void OnChildChanged(Csla.Core.ChildChangedEventArgs e)
        {
            //if (e.ChildObject is RoleList)
            //{
            //    BusinessRules.CheckRules(RolesProperty);
            //    OnPropertyChanged(RolesProperty);
            //}
            base.OnChildChanged(e);
        }

        public class UserExistsRule : Csla.Rules.BusinessRule
        {
            public UserExistsRule(Csla.Core.IPropertyInfo primaryProperty)
                : base(primaryProperty)
            {
#if SILVERLIGHT
                IsAsync = true;
#endif
                InputProperties = new System.Collections.Generic.List<Csla.Core.IPropertyInfo> { PrimaryProperty };
            }

            protected override void Execute(Csla.Rules.RuleContext context)
            {
                var username = (string)context.InputPropertyValues[PrimaryProperty];

#if !SILVERLIGHT && !NETFX_CORE
                if (UserEdit.Exists(username))
                {
                    context.AddErrorResult("No_Duplicate_Name");
                }
                context.Complete();
#endif

#if SILVERLIGHT
                UserEdit.Exists(username, (result) =>
                {
                    if (result) context.AddErrorResult("No_Duplicate_Name");
                    context.Complete();
                });
#endif
            }
        }

       

//        public class UserEmailExistsRule : Csla.Rules.BusinessRule
//        {
//            public UserEmailExistsRule(Csla.Core.IPropertyInfo primaryProperty)
//                : base(primaryProperty)
//            {
//#if SILVERLIGHT
//                IsAsync = true;
//#endif
//                InputProperties = new System.Collections.Generic.List<Csla.Core.IPropertyInfo> { PrimaryProperty };
//            }

//            protected override void Execute(Csla.Rules.RuleContext context)
//            {
//                var email = (string)context.InputPropertyValues[PrimaryProperty];

//#if !SILVERLIGHT && !NETFX_CORE

//                if (UserEdit.EmailExists(email))
//                {
//                    context.AddErrorResult("No_Duplicate_Email");
//                };
//                context.Complete();

//#endif

//#if SILVERLIGHT
//                UserEdit.EmailExists(email, (result) =>
//                {
//                    if (result) context.AddErrorResult("No_Duplicate_Email");
//                    context.Complete();
//                });
//#endif
//            }
//        }

        //private class NotEmptyPrimarySiteRule : Csla.Rules.BusinessRule
        //{
        //    protected override void Execute(Csla.Rules.RuleContext context)
        //    {
        //        var target = (UserEdit)context.Target;
        //        if (((RoleList)target.Roles).Count() == 0)
        //        {
        //            context.AddErrorResult(Properties.Resources.Generic_Required);
        //        }
        //    }
        //}

        #endregion

        #region Factory

        public static void NewUser(EventHandler<DataPortalResult<UserEdit>> callback)
        {
            UserGetter.CreateNewUser((o, e) =>
            {
                callback(o, new DataPortalResult<UserEdit>(e.Object.User, e.Error, null));
            });
        }

        public static void GetUser(string username, EventHandler<DataPortalResult<UserEdit>> callback)
        {
            UserGetter.GetExistingUser(username, (o, e) =>
            {
                callback(o, new DataPortalResult<UserEdit>(e.Object.User, e.Error, null));
            });
        }

        public static void GetUser(Guid idUser, EventHandler<DataPortalResult<UserEdit>> callback)
        {
            UserGetter.GetExistingUser(idUser, (o, e) =>
            {
                callback(o, new DataPortalResult<UserEdit>(e.Object.User, e.Error, null));
            });
        }

        public static void DeleteUser(string username, EventHandler<DataPortalResult<UserEdit>> callback)
        {
            DataPortal.BeginDelete<UserEdit>(username, callback);
        }

        public static void DeleteUser(Guid userId, EventHandler<DataPortalResult<UserEdit>> callback)
        {
            DataPortal.BeginDelete<UserEdit>(userId, callback);
        }

        public static void Exists(string username, Action<bool> result)
        {
            var cmd = new UserExistsCommand(username);
            DataPortal.BeginExecute<UserExistsCommand>(cmd, (o, e) =>
            {
                if (e.Error != null)
                    throw e.Error;
                else
                    result(e.Object.UserExists);
            });
        }

        //public static void EmailExists(string email, Action<bool> result)
        //{
        //    var cmd = new UserEmailExistsCommand(email);
        //    DataPortal.BeginExecute<UserEmailExistsCommand>(cmd, (o, e) =>
        //    {
        //        if (e.Error != null)
        //            throw e.Error;
        //        else
        //            result(e.Object.EmailExists);
        //    });
        //}

        //public static void Unlock(string username, Action<bool> result)
        //{
        //    var cmd = new UserUnlockCommand(username);
        //    DataPortal.BeginExecute<UserUnlockCommand>(cmd, (o, e) =>
        //    {
        //        if (e.Error != null)
        //            throw e.Error;
        //        else
        //            result(e.Object.Result);
        //    });
        //}

        //public static void ResetPassword(string username, string password, Action<bool> result)
        //{
        //    var cmd = new UserResetPasswordCommand(username, password);
        //    DataPortal.BeginExecute<UserResetPasswordCommand>(cmd, (o, e) =>
        //    {
        //        if (e.Error != null)
        //            throw e.Error;
        //        else
        //            result(e.Object.Result);
        //    });
        //}

#if !SILVERLIGHT

        public static UserEdit NewUser()
        {
            return DataPortal.Create<UserEdit>();
        }

        public static UserEdit GetUser(string username)
        {
            return DataPortal.Fetch<UserEdit>(username);
        }

        public static UserEdit GetUser(Guid idUser)
        {
            return DataPortal.Fetch<UserEdit>(idUser);
        }

        public static void DeleteUser(string username)
        {
            DataPortal.Delete<UserEdit>(username);
        }

        public static void DeleteUser(Guid idUser)
        {
            DataPortal.Delete<UserEdit>(idUser);
        }

        public static bool Exists(string username)
        {
            var cmd = new UserExistsCommand(username);
            cmd = DataPortal.Execute<UserExistsCommand>(cmd);
            return cmd.UserExists;
        }

        //public static bool Unlock(string username)
        //{
        //    var cmd = new UserUnlockCommand(username);
        //    cmd = DataPortal.Execute<UserUnlockCommand>(cmd);
        //    return cmd.Result;
        //}

        //public static bool EmailExists(string email)
        //{
        //    var cmd = new UserEmailExistsCommand(email);
        //    cmd = DataPortal.Execute<UserEmailExistsCommand>(cmd);
        //    return cmd.EmailExists;
        //}

        //public static bool ResetPassword(string username, string password)
        //{
        //    var cmd = new UserResetPasswordCommand(username, password);
        //    cmd = DataPortal.Execute<UserResetPasswordCommand>(cmd);
        //    return cmd.Result;
        //}

#endif

        #endregion

        #region Data
#if !SILVERLIGHT

        [RunLocal]
        protected override void DataPortal_Create()
        {
            base.DataPortal_Create();
            //Roles = new RoleList();
        }

        private void DataPortal_Fetch(string userName)
        {
            using (var ctx = DataAccess.Dal.DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IUserDal>();
                var data = dal.Fetch(userName);
                using (BypassPropertyChecks)
                {
                    ProviderUserKey = (Guid)data.ProviderUserKey;
                    Username = data.Username;
                    Password = data.Password;
                    Email = data.Email;
                    CreationDate = data.CreationDate;
                    LastActivityDate = data.LastActivityDate;
                    IsOnline = data.IsOnline;
                    IsLockedOut = data.IsLockedOut;
                    //Roles = DataPortal.FetchChild<RoleList>(data.Username);
                }
            }
        }


        private void DataPortal_Fetch(Guid idUser)
        {
            using (var ctx = DataAccess.Dal.DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IUserDal>();
                var data = dal.Fetch(idUser);
                using (BypassPropertyChecks)
                {
                    ProviderUserKey = (Guid)data.ProviderUserKey;
                    Username = data.Username;
                    Password = data.Password;
                    Email = data.Email;
                    CreationDate = data.CreationDate;
                    LastActivityDate = data.LastActivityDate;
                    IsOnline = data.IsOnline;
                    IsLockedOut = data.IsLockedOut;
                    //Roles = DataPortal.FetchChild<RoleList>(data.Username);
                }
            }
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DataAccess.Dal.DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IUserDal>();
                using (BypassPropertyChecks)
                {
                    var item = new UserDto
                    {
                        Username = this.Username,
                        Password = this.Password,
                        Email = this.Email,
                    };
                    dal.Insert(item);
                    ProviderUserKey = (Guid)item.ProviderUserKey;
                    CreationDate = item.CreationDate;
                    LastActivityDate = item.LastActivityDate;
                    IsLockedOut = item.IsLockedOut;
                    IsOnline = item.IsOnline;
                }
                FieldManager.UpdateChildren(this);
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DataAccess.Dal.DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IUserDal>();
                using (BypassPropertyChecks)
                {
                    var item = new UserDto
                    {
                        Username = this.Username,
                        Email = this.Email
                    };
                    dal.Update(item);
                    CreationDate = item.CreationDate;
                    LastActivityDate = item.LastActivityDate;
                    IsLockedOut = item.IsLockedOut;
                    IsOnline = item.IsOnline;
                }
                FieldManager.UpdateChildren(this);
            }
        }

        private void DataPortal_Delete(string username)
        {
            using (var ctx = DataAccess.Dal.DalFactory.GetManager())
            {
                FieldManager.UpdateChildren(this);
                var dal = ctx.GetProvider<IUserDal>();
                dal.Delete(username);
            }
        }

        private void DataPortal_Delete(Guid idUser)
        {
            using (var ctx = DataAccess.Dal.DalFactory.GetManager())
            {
                FieldManager.UpdateChildren(this);
                var dal = ctx.GetProvider<IUserDal>();
                dal.Delete(idUser);
            }
        }

#endif
        #endregion        
    }
}

