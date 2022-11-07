using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace AK.Model
{
    public class User:INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region Polja
        //ID, UserName, UserPass, IsAdmin
        private int _id;
        private string _userName;
        private string _userPass;
        private string _isAdmin;
        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        #endregion
        #region Svojstva

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);

        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                {
                    return;
                }
                _id = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Id"));
            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName == value)
                {
                    return;
                }
                _userName = value;

                List<string> errors = new List<string>();
                bool valid = true;

                if (value == null || value == "")
                {
                    errors.Add("User name can't be empty.");
                    SetErrors("UserName", errors);
                    valid = false;
                }


                if (!Regex.Match(value, @"^[a-zA-Z]+$").Success)
                {
                    errors.Add("User name can only contain letters.");
                    SetErrors("UserName", errors);
                    valid = false;
                }

                if (valid)
                {
                    ClearErrors("UserName");
                }
                
                OnPropertyChanged(new PropertyChangedEventArgs("UserName"));
            }
        }
        public string UserPass
        {
            get { return _userPass; }
            set {
                if (_userPass == value)
                {
                    return;
                }
                _userPass = value;

                List<string> errors = new List<string>();
                bool valid = true;

                if (value == null || value == "")
                {
                    errors.Add("Password can't be empty.");
                    SetErrors("UserPass", errors);
                    valid = false;
                }

                if (valid)
                {
                    ClearErrors("UserPass");
                }
                
                OnPropertyChanged(new PropertyChangedEventArgs("UserPass"));
            }
        }
        public string IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                if (_isAdmin == value)
                {
                    return;
                }
                _isAdmin = value;
                List<string> errors = new List<string>();
                bool valid = true;

                if (value == null || value == "")
                {
                    errors.Add("Admin can't be empty.");
                    SetErrors("IsAdmin", errors);
                    valid = false;
                }

                if (value != "true" && value != "false")
                {
                    errors.Add("Admin can only contain true or false values.");
                    SetErrors("IsAdmin", errors);
                    valid = false;
                }

                if (valid)
                {
                    ClearErrors("IsAdmin");
                }
                OnPropertyChanged(new PropertyChangedEventArgs("IsAdmin"));
            }
        }

        public bool HasErrors
        {
            get
            {
                return (errors.Count > 0);
            }
        }

        public User(int id, string userName, string userpass, string isAdmin)
        {
            this.Id = id;
            this.UserName = userName;
            this.UserPass = userpass;
            this.IsAdmin = isAdmin;
        }
        public User( string userName, string userpass, string isAdmin)
        {    
            this.UserName = userName;
            this.UserPass = userpass;
            this.IsAdmin = isAdmin;
        }
        public User()
        {
            UserName = "";
            UserPass = "";
            IsAdmin = "false";
        }

        #endregion
        #region Data Acess
        public static User GetUserFromResultSet(SqlDataReader reader)
        {
            User user = new User((int)reader["ID"], (string)reader["UserName"], (string)reader["UserPass"], (string)reader["IsAdmin"]);
            return user;
        }

        public void UpdateUser()
        {
            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE Users SET UserName=@UserName, UserPass=@UserPass, IsAdmin=@IsAdmin WHERE ID=@Id", conn);

                SqlParameter userNameParam = new SqlParameter("@UserName", SqlDbType.NVarChar);
                userNameParam.Value = this.UserName;

                SqlParameter userPassParam = new SqlParameter("@UserPass", SqlDbType.NVarChar);
                userPassParam.Value = this.UserPass;

                SqlParameter isAdminParam = new SqlParameter("@IsAdmin", SqlDbType.NVarChar);
                isAdminParam.Value = this.IsAdmin;

                SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                myParam.Value = this.Id;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(userPassParam);
                command.Parameters.Add(isAdminParam);
                command.Parameters.Add(myParam);

                int rows = command.ExecuteNonQuery();
            }
        }
        public void InsertUser()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Users (UserName, UserPass, IsAdmin) VALUES(@UserName, @UserPass, @IsAdmin); SELECT IDENT_CURRENT('Users');", conn);

                SqlParameter userNameParam = new SqlParameter("@UserName", SqlDbType.NVarChar);
                userNameParam.Value = this.UserName;

                SqlParameter userPassParam = new SqlParameter("@UserPass", SqlDbType.NVarChar);
                userPassParam.Value = this.UserPass;

                SqlParameter isAdminParam = new SqlParameter("@IsAdmin", SqlDbType.NVarChar);
                isAdminParam.Value = this.IsAdmin;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(userPassParam);
                command.Parameters.Add(isAdminParam);

                var id = command.ExecuteScalar();

                if (id != null)
                {
                    this.Id = Convert.ToInt32(id);
                }
            }
        }
        public void Login()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT COUNT(1) FROM Users WHERE UserName=@UserName AND UserPass=@UserPass", conn);
                
                SqlParameter userNameParam = new SqlParameter("@UserName", SqlDbType.NVarChar);
                userNameParam.Value = this.UserName;

                SqlParameter userPassParam = new SqlParameter("@UserPass", SqlDbType.NVarChar);
                userPassParam.Value = this.UserPass;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(userPassParam);

                var id = command.ExecuteScalar();

                if (id != null)
                {
                    this.Id = Convert.ToInt32(id);
                }
            }
        }
        #endregion
        #region Validacija, sinhronizacija...
        public void Save()
        {
            if (Id == 0)
            {
                InsertUser();
            }
            else
            {
                UpdateUser();
            }
        }
        //public void LogIn(string username, string userpass)
        //{
        //    if (username == UserName && userpass == UserPass)
        //    {
                
        //    }
        //}
        private void SetErrors(string propertyName, List<string> propertyErrors)
        {
            errors.Remove(propertyName);
            errors.Add(propertyName, propertyErrors);
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
        private void ClearErrors(string propertyName) 
        {
            errors.Remove(propertyName);
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return (errors.Values);
            }
            else
            {
                if (errors.ContainsKey(propertyName))
                {
                    return (errors[propertyName]);
                }
                else
                {
                    return null;
                }
            }
        }
        public User Clone()
        {
            User clonedUser = new User();
            clonedUser.Id = this.Id;
            clonedUser.UserName = this.UserName;
            clonedUser.UserPass = this.UserPass;
            clonedUser.IsAdmin = this.IsAdmin;

            return clonedUser;
        }
        #endregion
    }
}
