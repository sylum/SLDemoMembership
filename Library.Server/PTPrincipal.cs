using System;
using System.Security.Principal;
using Csla.Security;
using Csla.Serialization;

namespace Library
{
    [Serializable]
    public class PTPrincipal : CslaPrincipal
    {
        public static object PTIdentity { get; private set; }

        public PTPrincipal()
        { }

        protected PTPrincipal(IIdentity identity)
            : base(identity)
        { }

        public static void BeginLogin(string username, string password)
        {

            Library.PTIdentity.GetPTIdentity(username, password, (o, e) =>
            {
                if (e.Error == null && e.Object != null)
                    SetPrincipal(e.Object);
                else
                {
                    OnErrorLoggon(e.Error);
                }
            });
        }

#if !SILVERLIGHT
        public static bool Login(string username, string password)
        {
            

            var identity = Library.PTIdentity.GetPTIdentity(username, password);
            return SetPrincipal(identity);
        }

        public static bool Load(string username)
        {
            var identity = Library.PTIdentity.GetPTIdentity(username);
            return SetPrincipal(identity);
        }
#endif
        private static bool SetPrincipal(IIdentity identity)
        {
            if (identity.IsAuthenticated)
            {
                PTPrincipal principal = new PTPrincipal(identity);
                Csla.ApplicationContext.User = principal;
            }
            OnNewUser();
            return identity.IsAuthenticated;
        }

        public delegate void FailLoggonHandler(Exception data);

        public static event FailLoggonHandler FailLoggon;

        public static void OnErrorLoggon(Exception ex)
        {
            Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
            if (FailLoggon != null)
                FailLoggon(ex);
        }

        public static void Logout()
        {
            Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
            OnNewUser();
        }

        public static event Action NewUser;
        private static void OnNewUser()
        {
            if (NewUser != null)
                NewUser();
        }
    }
}
