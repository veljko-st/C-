using AK.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AK.ViewModel
{
    public class LoginVWindowViewModel // : INotifyPropertyChanged
    {
        //#region Polja
        //public User currentUser;
        //private string userName;
        //private string userPass;
        //public ICommand loginCommand;
        //#endregion
        //#region Svojstva
        //public string UserName
        //{
        //    get { return userName; }
        //    set { userName = value; }
        //}
        //public string UserPass
        //{
        //    get { return userPass; }
        //    set { userPass = value; }
        //}
        
        //public User CurrentUser
        //{
        //    get { return currentUser; }
        //    set
        //    {
        //        if (currentUser == value)
        //        {
        //            return;
        //        }
        //        currentUser = value;
        //        OnPropertyChange("CurrentUser");
        //    }
        //}
        //public ICommand LoginCommand
        //{
        //    get { return loginCommand; }
        //    set
        //    {
        //        if (loginCommand == value)
        //        {
        //            return;
        //        }
        //        loginCommand = value;
        //        OnPropertyChange("LoginCommand");
        //    }
        //}
        //#endregion
        //public LoginVWindowViewModel(string username, string userpass)
        //{
        //    CurrentUser = new User();
        //    UserName = username;
        //    UserPass = userpass;
        //    LoginCommand = new RelayCommand(LoginExecute, CanLogin);
        //}
        //void LoginExecute(object obj)
        //{
        //    if (CurrentUser.UserName == UserName && CurrentUser.UserPass == UserPass)
        //    {
        //        //Currentuser.Save();
        //        MessageBox.Show("Hello");
        //        return;
        //    }  
            
        //}
        //bool CanLogin(object obj)
        //{
        //    return true;
        //}
        ////private void Login()
        ////{
        ////    try
        ////    {
        ////        currentUser.LogIn(UserName, UserPass);
        ////    }
        ////    catch (Exception)
        ////    {
        ////        throw;
        ////    }
        ////}

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void OnPropertyChange(string propertyname)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        //    }
        //}

    }
}
