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
    public partial class AuthForm : UserControl
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = new AuthFormViewModel();
            this.DataContext = vm;
        }
    }
}
