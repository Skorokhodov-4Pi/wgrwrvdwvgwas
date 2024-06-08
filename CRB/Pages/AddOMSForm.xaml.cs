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
    /// Логика взаимодействия для AddOMSForm.xaml
    /// </summary>
    public partial class AddOMSForm : Page
    {
        static OMS_policy policy;
        public AddOMSForm(OMS_policy oms)
        {
            InitializeComponent();
            DataContext = policy = oms is null ? new OMS_policy() : oms;
            PacientCmb.ItemsSource = ConnectDB.GetCont().Patient.ToList();
        }

        private void AddOMSBtn_Click(object sender, RoutedEventArgs e)
        {
            if (policy.id_patient == 0)
                ConnectDB.GetCont().OMS_policy.Add(policy);
            try
            {
                ConnectDB.GetCont().SaveChanges();
                MessageBox.Show("Данные сохранены!");
                Nav.frame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка сохранения!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
        }
    }
}
