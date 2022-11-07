using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AK.Model;

namespace AK.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Polja
        private User currentUser;
        private UserCollection userList;
        private ListCollectionView userListView;
        private string filteringText;
        private Mediator mediator;

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
        public UserCollection UserList
        {
            get { return userList; }
            set
            {
                if (userList == value)
                {
                    return;
                }
                userList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserList"));
            }
        }
        public ListCollectionView UserListView
        {
            get { return userListView; }
            set
            {
                if (userListView == value)
                {
                    return;
                }
                userListView = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserListView"));
            }
        }
        public String FilteringText
        {
            get { return filteringText; }
            set
            {
                if (filteringText == value)
                {
                    return;
                }
                filteringText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FilteringText"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        #endregion
        #region Konstruktor
        public MainWindowViewModel(Mediator mediator)
        {
            this.mediator = mediator;
            this.PropertyChanged += MainWindowViewModel_PropertyChanged;
            UserList = UserCollection.GetAllUsers();
            userListView = new ListCollectionView(UserList);
            userListView.Filter = UserFilter;
            userListView.SortDescriptions.Add(new SortDescription("UserName", ListSortDirection.Ascending));
            Currentuser = new User();
            mediator.Register("UserChange", UserChanged);
        }
        #endregion
        #region Filtriranje
        void UserChanged(object sender)
        {
            User user = (User)sender;

            int index = UserList.IndexOf(user);

            if (index != -1)
            {
                UserList.RemoveAt(index);
                UserList.Insert(index, user);
            }
            else
            {
                UserList.Add(user);
            }
        }

        private void MainWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("FilteringText"))
            {
                userListView.Refresh();
            }
        }

        
        private bool UserFilter(object obj)
        {
            if (FilteringText == null)
            {
                return true;
            }
            if (FilteringText.Equals(""))
            {
                return true;
            }
            User user = obj as User;
            return (user.UserName.ToLower().StartsWith(FilteringText.ToLower()));
        }
        #endregion       
    }
}
