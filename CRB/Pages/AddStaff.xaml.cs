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
    /// Логика взаимодействия для AddStaff.xaml
    /// </summary>
    public partial class AddStaff : Page
    {
        public AddStaff()
        {
            InitializeComponent();
            Update();
            CmbFilterRole.ItemsSource = new string[]
            {
                "Все",
                "Админ",
                "Врач",
                "Cтаршая медицинская сестра",
                "Медицинская сестра"
            };
            

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
            
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            Right.editUsers = (sender as Button).DataContext as Staff;
            Nav.frame.Navigate(new EditStaff());
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
        }
        void Update()
        {
            if(CmbFilterRole.SelectedIndex == 0)
            {
                var up = ConnectDB.GetCont().Staff.ToList();
                AddStaffLV.ItemsSource = up;
            }
            else if (CmbFilterRole.SelectedIndex == 1)
            {
                var up = ConnectDB.GetCont().Staff.ToList();
                up = up.Where(x => x.Positions.id_position == 4).ToList();
                AddStaffLV.ItemsSource = up;
            }
            else if (CmbFilterRole.SelectedIndex == 2)
            {
                var up = ConnectDB.GetCont().Staff.ToList();
                up = up.Where(x => x.Positions.id_position == 1).ToList();
                AddStaffLV.ItemsSource = up;
            }
            else if (CmbFilterRole.SelectedIndex == 3)
            {
                var up = ConnectDB.GetCont().Staff.ToList();
                up = up.Where(x => x.Positions.id_position == 2).ToList();
                AddStaffLV.ItemsSource = up;
            }
            else if (CmbFilterRole.SelectedIndex == 4)
            {
                var up = ConnectDB.GetCont().Staff.ToList();
                up = up.Where(x => x.Positions.id_position == 3).ToList();
                AddStaffLV.ItemsSource = up;
            }
            
        }

        private void CmbFilterRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new AddStafForm());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            //Новая книга
            Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet ws = wb.Sheets[1];
            Microsoft.Office.Interop.Excel.Range range = ws.Range["A1", "F1"];
            range.Merge();
            range.Value = "Лист пользователей";
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            ws.Cells[2, 1] = "Фамилия";
            ws.Cells[2, 1].Font.Bold = true;
            ws.Cells[2, 2] = "Имя";
            ws.Cells[2, 2].Font.Bold = true;
            ws.Cells[2, 3] = "Отчество";
            ws.Cells[2, 3].Font.Bold = true;
            ws.Cells[2, 4] = "Логин";
            ws.Cells[2, 4].Font.Bold = true;
            ws.Cells[2, 5] = "Пароль";
            ws.Cells[2, 5].Font.Bold = true;

            int row = 3;
            foreach (var item in AddStaffLV.Items)
            {
                dynamic otchet = item;

                ws.Cells[row, 1] = otchet.surname;
                ws.Cells[row, 2] = otchet.first_name;
                ws.Cells[row, 3] = otchet.patronymic;
                ws.Cells[row, 4] = otchet.login;
                ws.Cells[row, 5] = otchet.password;
                row++;
            }
            int abba = row - 1;
            ws.Cells[row, 1] = "Кол-во сотрудников:";
            ws.Cells[row, 1].Font.Bold = true;
            ws.Cells[row, 5] = "=СЧЁТЗ(E3:E" + abba + ")";
            ws.Cells[row, 5].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            ws.Cells[row + 1, 1] = "Автор:";
            ws.Cells[row + 1, 1].Font.Bold = true;
            ws.Cells[row + 1, 5] = Right.curUser.surname + " " + Right.curUser.first_name + " " + Right.curUser.patronymic;
            ws.Cells[row + 1, 5].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            ws.Cells[row + 2, 1] = "Дата создания отчета:";
            ws.Cells[row + 2, 1].Font.Bold = true;
            ws.Cells[row + 2, 5] = DateTime.Now;


        }
    }
}
