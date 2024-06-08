using CRB.AppData;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddStafForm.xaml
    /// </summary>
    public partial class AddStafForm : Page
    {
        static Staff users;
        public AddStafForm()
        {
            InitializeComponent();
            DataContext = users = new Staff() { date_birth = new DateTime(2000, 1, 1) };
            RoleCmb.ItemsSource = ConnectDB.GetCont().Positions.ToList();
            OtdelCmb.ItemsSource = ConnectDB.GetCont().Departments.ToList();
        }

        private void OtdelCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RoleCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
        }

        private void AddStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(SurnameTxt.Text))
                errors.AppendLine("Не введена фамилия");

            if (string.IsNullOrEmpty(NameTxt.Text))
                errors.AppendLine("Не введено имя");

            if (string.IsNullOrEmpty(PatrTxt.Text))
                errors.AppendLine("Не введено отчество");

            if (users.date_birth < new DateTime(1900, 1, 1)) errors.AppendLine("Не правильно введена дата");

            if (string.IsNullOrEmpty(users.SNILS) || users.SNILS.Count() < 11) errors.AppendLine("Не правильно введен снилс");

            if (string.IsNullOrEmpty(users.phone_number)) errors.AppendLine("Не введен номер телефона");

            if (string.IsNullOrEmpty(users.email)) errors.AppendLine("Не введена почта");

            if (string.IsNullOrEmpty(users.actual_address)) errors.AppendLine("Не введен адрес нахождения");

            if (string.IsNullOrEmpty(users.registered_address)) errors.AppendLine("Не введен адрес прописки");

            if (string.IsNullOrEmpty(OtdelCmb.Text)) errors.AppendLine("Не выбран отдел");

            if (string.IsNullOrEmpty(RoleCmb.Text)) errors.AppendLine("Не выбрана роль");

            if (string.IsNullOrEmpty(RoleCmb.Text)) errors.AppendLine("Не выбрана роль");

            if (string.IsNullOrEmpty(loginTxt.Text)) errors.AppendLine("Не введен логин");

            if (string.IsNullOrEmpty(passTxt.Text)) errors.AppendLine("Не введен пароль");





            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            ConnectDB.GetCont().Staff.Add(users);
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

        private void CancelBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
        }
    }
}
