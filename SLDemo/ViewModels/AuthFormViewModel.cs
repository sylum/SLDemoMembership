using Csla.Xaml;
using System;
using System.Windows;

namespace SilverlightUI.ViewModels
{
    public class AuthFormViewModel : DependencyObject
    {
        public static readonly DependencyProperty UsernameProperty = DependencyProperty.Register("Username", typeof(string), typeof(AuthFormViewModel), null);
        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(AuthFormViewModel), null);
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public AuthFormViewModel()
        {
            Username = string.Empty;
            Password = string.Empty;
            //Shell.Instance.ShowStatus(new Status { Text = "AuthRequired", IsBusy = false });
        }

        public void CheckAuth(object sender, ExecuteEventArgs e)
        {
            Library.PTPrincipal.NewUser += new Action(CustomPrincipal_NewUser);
            Library.PTPrincipal.FailLoggon += new Library.PTPrincipal.FailLoggonHandler(CustomPrincipal_FailLoggon);
            //Bxf.Shell.Instance.ShowStatus(new Bxf.Status { IsBusy = true, Text = "CheckingAuthMessage" });
            Library.PTPrincipal.BeginLogin(Username, Password);
        }

        public void CancelAuth()
        {
            Username = string.Empty;
            Password = string.Empty;
            //Shell.Instance.ShowStatus(new Status { IsBusy = false, Text = "AuthForm_Cancel" });
        }

        void CustomPrincipal_NewUser()
        {

            Library.PTPrincipal.NewUser -= new Action(CustomPrincipal_NewUser);
            Library.PTPrincipal.FailLoggon -= new Library.PTPrincipal.FailLoggonHandler(CustomPrincipal_FailLoggon);

            if (Csla.ApplicationContext.User.Identity.IsAuthenticated)
            {
                //Bxf.Shell.Instance.ShowStatus(new Bxf.Status { IsBusy = false, Text = String.Format("WelcomeUser", Csla.ApplicationContext.User) });
                //InitializeViews();
            }
        }

        void CustomPrincipal_FailLoggon(Exception ex)
        {
            Library.PTPrincipal.NewUser -= new Action(CustomPrincipal_NewUser);
            Library.PTPrincipal.FailLoggon -= new Library.PTPrincipal.FailLoggonHandler(CustomPrincipal_FailLoggon);

            //Bxf.Shell.Instance.ShowStatus(new Bxf.Status { IsBusy = false, Text = "InvalidAuth" });
            //Bxf.Shell.Instance.ShowError(message, "InvalidAuthTitle");
            Library.PTPrincipal.Logout();
        }

    }
}
