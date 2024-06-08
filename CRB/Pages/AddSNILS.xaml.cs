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
    /// Логика взаимодействия для AddSNILS.xaml
    /// </summary>
    public partial class AddSNILS : Page
    {
        static OMS_policy policy;
        public AddSNILS(OMS_policy polis)
        {
            InitializeComponent();
            policy = polis;
            DataContext = policy = polis is null ? new OMS_policy() { start_date = DateTime.Now, end_date = DateTime.Now } : polis; 
        }

        private void AddPacBtn_Click(object sender, RoutedEventArgs e)
        {
            var errorsBuild = new StringBuilder();
            if (string.IsNullOrEmpty(policy.region)) errorsBuild.AppendLine("Не введен регион");
            if (string.IsNullOrEmpty(policy.policy_number)) errorsBuild.AppendLine("Не введен регион");
            if (policy.start_date < new DateTime(1900, 1, 1)) errorsBuild.AppendLine("Дата начала не верна");
            if (policy.end_date < new DateTime(1900, 1, 1)) errorsBuild.AppendLine("Дата окончания неверно");
            if (errorsBuild.Length > 0)
            {
                MessageBox.Show(errorsBuild.ToString(), "Ошибка сохранения!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                ConnectDB.GetCont().OMS_policy.Add(policy);
                ConnectDB.GetCont().SaveChanges(); 
                MessageBox.Show("Данные сохранены!");
                Nav.frame.Navigate(new AddPacForm(null));
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
