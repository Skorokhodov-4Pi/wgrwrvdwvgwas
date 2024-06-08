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
    /// Логика взаимодействия для AddPac.xaml
    /// </summary>
    public partial class AddPac : Page
    {
        public AddPac()
        {
            InitializeComponent();
            AddPacLV.ItemsSource = ConnectDB.GetCont().Patient.ToList();

        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            var pac = ConnectDB.GetCont().Patient.ToList();
            pac = pac.Where(x => x.Surname.Contains(SearchTxt.Text)).ToList();
            AddPacLV.ItemsSource = pac;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new NavMenu());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new AddPacForm(null));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AddPacLV.ItemsSource = ConnectDB.GetCont().Patient.ToList();

        }
    }
}
