using AK.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AK.ViewModel
{
    public class NewEditSingInWindowViewModel : INotifyPropertyChanged
    {
        #region Polja
        public User currentUser;
        public string windowTitle;
        private ICommand saveCommand;
        private Mediator mediator;
        //private ICommand loginCommand;
        #endregion
        #region Svojstva
        public User Currentuser
        {
            get { return currentUser; }
            set
            {
                if (currentUser == value)
                {
                    return;
                }
                currentUser = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Currentuser"));
            }
        }
        public string WindowTitle
        {
            get { return windowTitle; }
            set
            {
                if (windowTitle == value)
                {
                    return;
                }
                windowTitle = value;
                OnPropertyChanged(new PropertyChangedEventArgs("WindowTitle"));
            }
        }
        public ICommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                if (saveCommand == value)
                {
                    return;
                }
                saveCommand = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SaveCommand"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        #endregion
        #region Konstruktori
        public NewEditSingInWindowViewModel(User user, Mediator mediator)
        {
            this.mediator = mediator;
            SaveCommand = new RelayCommand(SaveExecute, CanSave);
            currentUser = user;
            windowTitle = "Edit user";
        }

        public NewEditSingInWindowViewModel(Mediator mediator)//New i SingIn
        {
            this.mediator = mediator;
            SaveCommand = new RelayCommand(SaveExecute, CanSave);
            currentUser = new User();
            windowTitle = "New user";
            
        }
        #endregion
        #region Save
        void SaveExecute(object obj)
        {
            if (Currentuser != null && !Currentuser.HasErrors)
            {
                Currentuser.Save();
                OnDone(new DoneEventArgs("User Saved."));
                mediator.Notify("UserChange", Currentuser);
            }
            else
            {
                OnDone(new DoneEventArgs("Chech your input."));
            }
        }       
        bool CanSave(object obj)
        {
            return true;
        }
        public delegate void DoneEventHandler(object sender, DoneEventArgs e);

        public class DoneEventArgs : EventArgs // sluzi za cuvanje poruke za prikaz korisniku
        {
            private string massage;

            public string Message
            {
                get { return massage; }
                set
                {
                    if (massage == value)
                    {
                        return;
                    }
                    massage = value;
                }
            }

            public DoneEventArgs(string message)
            {
                this.massage = message;
            }

        }

        public event DoneEventHandler Done;

        public void OnDone(DoneEventArgs e)
        {
            if (Done != null)
            {
                Done(this, e);
            }
        }


        #endregion

        #region 'Login'
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
        //        OnPropertyChanged(new PropertyChangedEventArgs("SaveCommand"));
        //    }
        //}
        //void LoginExecute(object obj)
        //{
        //    if (Currentuser.UserName == Currentuser.UserName && Currentuser.UserPass == Currentuser.UserPass)
        //    {
        //        MainWindowViewModel 
        //        OnDone(new DoneEventArgs("User Saved."));
        //        mediator.Notify("UserChange", Currentuser);
        //    }
        //    else
        //    {
        //        OnDone(new DoneEventArgs("Chech your input."));
        //    }
        //}
        #endregion
    }
}

