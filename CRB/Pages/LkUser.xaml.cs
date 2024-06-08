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
    /// Логика взаимодействия для LkUser.xaml
    /// </summary>
    public partial class LkUser : Page
    {
        public LkUser()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var ab = new List<Staff> { Right.curUser }; 
            PLV.ItemsSource = ab;
            PLV2.ItemsSource = ab;
            PLV3.ItemsSource = ab;
            InfLV.ItemsSource = ab;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
        }
    }
}
