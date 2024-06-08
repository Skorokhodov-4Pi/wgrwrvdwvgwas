using CRB.AppData;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRB.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOMS.xaml
    /// </summary>
    public partial class AddOMS : Page
    {
        public AddOMS()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new AddOMSForm(null));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AddOMSLV.ItemsSource = ConnectDB.GetCont().Patient.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Right.patt = (sender as Button).DataContext as Patient;
            Nav.frame.Navigate(new EditOMS());
        }
    }
}
