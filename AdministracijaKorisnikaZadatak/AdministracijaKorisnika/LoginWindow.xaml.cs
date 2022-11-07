using AK.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdministracijaKorisnika
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainWindowView = new MainWindowViewModel(Mediator.Instance);
            this.DataContext = mainWindowView;
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            NewEditSingin newWindow = new NewEditSingin();
            newWindow.DataContext = new NewEditSingInWindowViewModel(Mediator.Instance);
            newWindow.ShowDialog();
        }

        //private void loginBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow mainWindow = new MainWindow();
        //    mainWindow.DataContext = new LoginVWindowViewModel(txtUsername.Text, txtpassword.Password);
        //    mainWindow.ShowDialog();
        //    this.Close();
        //}

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT COUNT(1) FROM Users WHERE UserName=@UserName AND UserPass=@UserPass", conn);

                command.Parameters.AddWithValue("@UserName", txtUsername.Text);
                command.Parameters.AddWithValue("@UserPass", txtpassword.Password);

                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("Login failed. Please try again.");
                }
                
            }
        }

    }
}
