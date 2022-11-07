using AK.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for NewEditSingin.xaml
    /// </summary>
    public partial class NewEditSingin : Window
    {
        public NewEditSingin()
        {
            InitializeComponent();
        }

        private void rectangle_Loaded(object sender, RoutedEventArgs e)
        {
            NewEditSingInWindowViewModel viewModel = (NewEditSingInWindowViewModel)DataContext;
            viewModel.Done += ViewModel_Done;
        }

        private void ViewModel_Done(object sender, NewEditSingInWindowViewModel.DoneEventArgs e)
        {
            MessageBox.Show(e.Message);
        }

    }
}
