using AK.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdministracijaKorisnika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainWindowView = new MainWindowViewModel(Mediator.Instance);
            this.DataContext = mainWindowView;

        }

        private void newBtn_Click(object sender, RoutedEventArgs e)
        {
            NewEditSingin newWindow = new NewEditSingin();
            newWindow.DataContext = new NewEditSingInWindowViewModel(Mediator.Instance);
            newWindow.ShowDialog();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {           
            MainWindowViewModel viewModel = (MainWindowViewModel)DataContext;
            NewEditSingin editWindow = new NewEditSingin();
            editWindow.DataContext = new NewEditSingInWindowViewModel(viewModel.Currentuser.Clone(), Mediator.Instance);
            editWindow.ShowDialog();
        }

       
    }
}
