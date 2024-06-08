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
    /// Логика взаимодействия для EditOMS.xaml
    /// </summary>
    public partial class EditOMS : Page
    {
        OMS_policy polis;
        public EditOMS()
        {
            InitializeComponent();
            DataContext = ConnectDB.GetCont().OMS_policy.Where(x => x.id_patient == Right.patt.id_patient && x.end_date < DateTime.Now).OrderByDescending(x => x.end_date).FirstOrDefault();
        }

        private void EditOMSBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
        }
    }
}
