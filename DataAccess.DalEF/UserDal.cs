using DataAccess.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace DataAccess.DalSecurity
{
    public class UserDal : IUserDal
    {
        public UserDto Fetch(string username, string password)
        {
            if (Membership.ValidateUser(username, password))
            {
                return Fetch(username);
            }
            else
            {
                //SI l'utilisateur n'est pas valide, on essaye de savoir pourquoi
                MembershipUser user = Membership.GetUser(username);
                if (user != null)
                {
                    if (user.IsLockedOut)
                        throw new MemberAccessException("UserLockedException");
                    else //si l'utilisateur a été trouvé mais n'est pas valide, c'est que son mot de passe est erroné
                        throw new MembershipPasswordException("WrongPasswordException");
                }
                else
                {
                    throw new DataNotFoundException("UserNotFoundException");
                }
            }
        }

        public UserDto Fetch(string username)
        {
            try
            {
                var u = Membership.GetUser(username);
                var result = new UserDto
                {
                    ProviderUserKey = u.ProviderUserKey,
                    Username = u.UserName,
                    Email = u.Email,
                    CreationDate = u.CreationDate,
                    LastLoginDate = u.LastLoginDate,
                    IsOnline = u.IsOnline,
                    IsLockedOut = u.IsLockedOut,
                    LastActivityDate = u.LastActivityDate
                };
                result.Roles = Roles.Provider.GetRolesForUser(result.Username);
                return result;
            }
            catch (Exception ex)
            {
                throw new DataNotFoundException("User", ex);
            }
        }

        public UserDto Fetch(Guid providerUserKey)
        {
            try
            {
                var u = Membership.GetUser(providerUserKey);
                var result = new UserDto
                {
                    ProviderUserKey = u.ProviderUserKey,
                    Username = u.UserName,
                    Email = u.Email,
                    CreationDate = u.CreationDate,
                    LastLoginDate = u.LastLoginDate,
                    IsOnline = u.IsOnline,
                    IsLockedOut = u.IsLockedOut,
                    LastActivityDate = u.LastActivityDate
                };
                result.Roles = Roles.Provider.GetRolesForUser(result.Username);
                return result;
            }
            catch (Exception ex)
            {
                throw new DataNotFoundException("User", ex);
            }
        }

        public List<UserDto> Fetch()
        {
            List<UserDto> result = new List<UserDto>();

            foreach (MembershipUser u in Membership.GetAllUsers())
            {
                result.Add(new UserDto
                {
                    ProviderUserKey = u.ProviderUserKey,
                    Username = u.UserName,
                    Email = u.Email,
                    CreationDate = u.CreationDate,
                    LastLoginDate = u.LastLoginDate,
                    IsOnline = u.IsOnline,
                    IsLockedOut = u.IsLockedOut,
                    LastActivityDate = u.LastActivityDate,
                    Roles = Roles.Provider.GetRolesForUser(u.UserName)
                });
            }
            return result.OrderBy(x => x.Username).ToList();
        }

        public bool Exists(string username)
        {
            return (Membership.GetUser(username) != null);
        }

        public bool EmailExists(string email)
        {
            return !string.IsNullOrEmpty(Membership.GetUserNameByEmail(email));
        }

        public void UnlockUser(string username)
        {
            var u = Membership.GetUser(username);
            if (u.IsLockedOut)
                u.UnlockUser();
        }

        public void ResetPassword(string username, string newPassword)
        {
            try
            {
                var u = Membership.GetUser(username);
                u.ChangePassword(u.ResetPassword(), newPassword);
            }
            catch (Exception ex)
            {
                throw new DataNotFoundException("User", ex);
            }
        }

        public void Insert(UserDto item)
        {
            try
            {
                //var u = Membership.CreateUser(item.Username, item.Password, item.Email);
                var u = Membership.CreateUser(item.Username, item.Password);
                item.ProviderUserKey = u.ProviderUserKey;
                item.CreationDate = u.CreationDate;
                item.LastActivityDate = u.LastActivityDate;
                item.IsOnline = u.IsOnline;
                item.IsLockedOut = item.IsLockedOut;
            }
            catch (MembershipCreateUserException ex)
            {
                var msg = GetErrorMessage(ex.StatusCode);
                throw new DataNotFoundException("User :" + ex.Message, ex);
            }
        }

        public string GetErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        public void Update(UserDto item)
        {
            try
            {
                //We need to only ensure that two properties are editable and values can be changed.
                var u = Membership.GetUser(item.Username);

                u.Email = item.Email;

                if (u.IsLockedOut)
                    u.UnlockUser();

                Membership.UpdateUser(u);

                item.CreationDate = u.CreationDate;
                item.LastActivityDate = u.LastActivityDate;
                item.IsOnline = u.IsOnline;
                item.IsLockedOut = item.IsLockedOut;

            }
            catch (Exception ex)
            {
                throw new DataNotFoundException("User", ex);
            }
        }

        public void Delete(string UserName)
        {
            Membership.DeleteUser(UserName);
        }

        public void Delete(Guid providerUserKey)
        {
            var u = Membership.GetUser(providerUserKey);
            Delete(u.UserName);
        }
    }
}
