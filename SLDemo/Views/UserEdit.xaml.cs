using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using SilverlightUI.ViewModels;

namespace SilverlightUI.Views
{
    public partial class UserEdit : UserControl
    {
        public UserEdit()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Library.UserEdit user = null;
            Library.UserEdit.NewUser((c, o) => {
                if (o.Error == null)
                {
                    user = o.Object;
                    var vm = new UserEditViewModel(user);
                    this.DataContext = vm;
                }
            });

         
        }
    }
}
