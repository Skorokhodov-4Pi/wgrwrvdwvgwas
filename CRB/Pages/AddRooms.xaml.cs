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
    /// Логика взаимодействия для AddRooms.xaml
    /// </summary>
    public partial class AddRooms : Page
    {
        public AddRooms()
        {
            InitializeComponent();

        }

        private void AddBedBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new AddBedsPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) => AddRoomsLV.ItemsSource = ConnectDB.context.beds.ToList();

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
        }
    }
}
