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
    /// Логика взаимодействия для NavMenu.xaml
    /// </summary>
    public partial class NavMenu : Page
    {
        public NavMenu()
        {
            InitializeComponent();
            
        }

        private void AddStaffBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddPacBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new AddPac());
        }

        private void UchetPacBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new Uchet());
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new Avtoris2());
        }

        private void AddStaffBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new AddStaff());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Right.curUser.id_position == 4)
            {
                AddStaffBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AddStaffBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new LkUser());
        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new AddRooms());
        }

        private void AddOMS_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new AddOMS());
        }
    }
}
