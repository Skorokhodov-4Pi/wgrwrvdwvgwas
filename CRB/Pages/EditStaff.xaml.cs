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
    /// Логика взаимодействия для EditStaff.xaml
    /// </summary>
    public partial class EditStaff : Page
    {
        public EditStaff()
        {
            InitializeComponent();
            var role = ConnectDB.GetCont().Positions.ToList();
            RoleCmb.ItemsSource = role;
            DataContext = Right.editUsers ?? new Staff();

        }

        private void RoleCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectDB.GetCont().SaveChanges();
            Nav.frame.GoBack();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
        }
    }
}
