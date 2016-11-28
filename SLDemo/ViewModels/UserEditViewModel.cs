using Csla.Xaml;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightUI.ViewModels
{
    public class UserEditViewModel : ViewModel<Library.UserEdit>
    {
        public UserEditViewModel(Library.UserEdit userEdit)
        {
            Model = userEdit;
        }

        protected override void OnModelChanged(Library.UserEdit oldValue, Library.UserEdit newValue)
        {
            base.OnModelChanged(oldValue, newValue);
        }

        //public new void Save(object sender, ExecuteEventArgs e)
        //{
        //    if (Model.IsSavable)
        //        Model.Save();
        //}

        public void Close(object sender, ExecuteEventArgs e)
        {
            //
        }

    }
}
