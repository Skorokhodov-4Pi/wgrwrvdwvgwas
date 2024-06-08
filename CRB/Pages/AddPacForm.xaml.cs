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
    /// Логика взаимодействия для AddPacForm.xaml
    /// </summary>
    public partial class AddPacForm : Page 
    {
        static Patient pat;
        public AddPacForm(Patient patt)
        {
            InitializeComponent();
            DataContext = pat = patt is null ? new Patient() { date_of_birth = new DateTime(2000,1,1) } : patt;
            genderCmb.ItemsSource = new string[]
            {
                "мужской",
                "женский"
            };
            PolisTxt.Text = pat.policy_number;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
        }
        private void AddPacBtn_Click(object sender, RoutedEventArgs e)
        {
            var errorsBuild = new StringBuilder();
            if (string.IsNullOrEmpty(pat.Surname)) errorsBuild.AppendLine("Не введена фамилия");
            if (string.IsNullOrEmpty(pat.First_name)) errorsBuild.AppendLine("Не введено имя");
            if (string.IsNullOrEmpty(pat.Patronymic)) errorsBuild.AppendLine("Не введено отчетсво");
            if (string.IsNullOrEmpty(pat.gender)) errorsBuild.AppendLine("Не введен пол");
            if (string.IsNullOrEmpty(pat.phone_number)) errorsBuild.AppendLine("Не введен номер телефона");
            if (string.IsNullOrEmpty(pat.email)) errorsBuild.AppendLine("Не введена почта");
            if (string.IsNullOrEmpty(pat.actual_address)) errorsBuild.AppendLine("Не введен адрес нахождения");
            if (string.IsNullOrEmpty(pat.registered_address)) errorsBuild.AppendLine("Не введен адрес прописки");
            if (pat.date_of_birth < new DateTime(1900, 1, 1)) errorsBuild.AppendLine("Не правильно введена дата");
            if (string.IsNullOrEmpty(pat.SNILS) || pat.SNILS.Count() < 11) errorsBuild.AppendLine("Не правильно введен снилс");
            if (errorsBuild.Length > 0)
            {
                MessageBox.Show(errorsBuild.ToString(), "Ошибка сохранения!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (pat.id_patient == 0)
                ConnectDB.GetCont().Patient.Add(pat);
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

        private void genderCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
