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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CRB.Pages
{
    /// <summary>
    /// Логика взаимодействия для Uchet.xaml
    /// </summary>
   
    public partial class Uchet : Page
    {
        Staff curUser = Right.curUser;
        public Uchet()
        {
            InitializeComponent();
            Update();
            CmbFilter.ItemsSource = new string[]
            {
                "Все",
                "Выписан",
                "Переведен на другой профиль коек",
                "Переведен в другое ЛПУ",
                "Смерть"
            };
        }

        void Update()
        {
            if (CmbFilter.SelectedIndex == 0)
            {
                var up = ConnectDB.GetCont().Electronic_medical_card.ToList();
                PacLV.ItemsSource = up;
            }
            else if (CmbFilter.SelectedIndex == 1)
            {
                var up = ConnectDB.GetCont().Electronic_medical_card.ToList();
                up = up.Where(x => x.id_outcomes == 1).ToList();
                PacLV.ItemsSource = up;
            }
            else if (CmbFilter.SelectedIndex == 2)
            {
                var up = ConnectDB.GetCont().Electronic_medical_card.ToList();
                up = up.Where(x => x.id_outcomes == 2).ToList();
                PacLV.ItemsSource = up;
            }
            else if (CmbFilter.SelectedIndex == 3)
            {
                var up = ConnectDB.GetCont().Electronic_medical_card.ToList();
                up = up.Where(x => x.id_outcomes == 3).ToList();
                PacLV.ItemsSource = up;
            }
            else if (CmbFilter.SelectedIndex == 4)
            {
                var up = ConnectDB.GetCont().Electronic_medical_card.ToList();
                up = up.Where(x => x.id_outcomes == 4).ToList();
                PacLV.ItemsSource = up;
            }

     
            
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            CurElMedCard.card = (sender as Button).DataContext as Electronic_medical_card;
            Nav.frame.Navigate(new UchetEdit());
        }


        private void SearchTxt_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            var mat = ConnectDB.GetCont().Electronic_medical_card.ToList();
            mat = mat.Where(x => x.Patient.Surname.Contains(SearchTxt.Text)).ToList();
            PacLV.ItemsSource = mat;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void OtchetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OtchetDP.SelectedDate != null && OtchetDP2.SelectedDate != null)
            {
                OtchetPoDiaposonu();
            }
            else if (OtchetDP.SelectedDate == null && OtchetDP2.SelectedDate == null)
            {
                Otchet1();
            }
            else if (OtchetDP.SelectedDate != null && OtchetDP2.SelectedDate == null)
            {
                Otchet2();
            }
            else
            {
                Otchet3();
            }
        }

        void Otchet1()
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            //Новая книга
            Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet ws = wb.Sheets[1];
            Microsoft.Office.Interop.Excel.Range range = ws.Range["A1", "F1"];
            range.Merge();
            range.Value = "Отчет поступивших пациентов";
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //Заголовки
            ws.Cells[2, 1] = "Фамилия";
            ws.Cells[2, 1].Font.Bold = true;
            ws.Cells[2, 2] = "Имя";
            ws.Cells[2, 2].Font.Bold = true;
            ws.Cells[2, 3] = "Отчество";
            ws.Cells[2, 3].Font.Bold = true;
            ws.Cells[2, 4] = "Дата рождения";
            ws.Cells[2, 4].Font.Bold = true;
            ws.Cells[2, 5] = "Дата поступления";
            ws.Cells[2, 5].Font.Bold = true;
            ws.Cells[2, 6] = "Диагноз";
            ws.Cells[2, 6].Font.Bold = true;
            int row = 3;
            foreach (var item in PacLV.Items)
            {
                dynamic otchet = item;
                ws.Cells[row, 1] = otchet.Patient.Surname;
                ws.Cells[row, 2] = otchet.Patient.First_name;
                ws.Cells[row, 3] = otchet.Patient.Patronymic;
                ws.Cells[row, 4] = otchet.Patient.date_of_birth;
                ws.Cells[row, 5] = otchet.date_receipt;
                List<string> m = new List<string>();
                foreach (var a in otchet.MKB)
                {
                    m.Add(a.name_diagnosis);
                }
                ws.Cells[row, 6] = string.Join(", ", m);
                row++;
            }
            ws.Cells[row, 1] = "Кол-во пациентов:";
            ws.Cells[row, 1].Font.Bold = true;
            ws.Cells[row, 6] = row - 3;
            ws.Cells[row, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            ws.Cells[row + 1, 1] = "Автор:";
            ws.Cells[row + 1, 1].Font.Bold = true;
            ws.Cells[row + 1, 6] = Right.curUser.surname + " " + Right.curUser.first_name + " " + Right.curUser.patronymic;
            ws.Cells[row + 1, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            ws.Cells[row + 2, 1] = "Дата создания отчета:";
            ws.Cells[row + 2, 1].Font.Bold = true;
            ws.Cells[row + 2, 6] = DateTime.Now;
        }

        void Otchet2()
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            DateTime d1 = OtchetDP.SelectedDate.Value.Date;
            //Новая книга
            Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet ws = wb.Sheets[1];
            Microsoft.Office.Interop.Excel.Range range = ws.Range["A1", "F1"];
            range.Merge();
            range.Value = "Отчет поступивших пациентов c " + d1;
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //Заголовки
            ws.Cells[2, 1] = "Фамилия";
            ws.Cells[2, 1].Font.Bold = true;
            ws.Cells[2, 2] = "Имя";
            ws.Cells[2, 2].Font.Bold = true;
            ws.Cells[2, 3] = "Отчество";
            ws.Cells[2, 3].Font.Bold = true;
            ws.Cells[2, 4] = "Дата рождения";
            ws.Cells[2, 4].Font.Bold = true;
            ws.Cells[2, 5] = "Дата поступления";
            ws.Cells[2, 5].Font.Bold = true;
            ws.Cells[2, 6] = "Диагноз";
            ws.Cells[2, 6].Font.Bold = true;
            int row = 3;
            var res = ConnectDB.GetCont().Electronic_medical_card.ToList();           
            res = res.Where(x => x.date_receipt.Date >= d1).ToList();
            foreach (var item in res)
            {
                dynamic otchet = item;

                ws.Cells[row, 1] = otchet.Patient.Surname;
                ws.Cells[row, 2] = otchet.Patient.First_name;
                ws.Cells[row, 3] = otchet.Patient.Patronymic;
                ws.Cells[row, 4] = otchet.Patient.date_of_birth;
                ws.Cells[row, 5] = otchet.date_receipt;
                List<string> m = new List<string>();
                foreach (var a in otchet.MKB)
                {
                    m.Add(a.name_diagnosis);
                }
                ws.Cells[row, 6] = string.Join(", ", m);
                row++;
            }
            int abba = row - 1;
            ws.Cells[row, 1] = "Кол-во пациентов:";
            ws.Cells[row, 1].Font.Bold = true;
            ws.Cells[row, 6] = row - 3;
            ws.Cells[row, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            ws.Cells[row + 1, 1] = "Автор:";
            ws.Cells[row + 1, 1].Font.Bold = true;
            ws.Cells[row + 1, 6] = Right.curUser.surname + " " + Right.curUser.first_name + " " + Right.curUser.patronymic;
            ws.Cells[row + 1, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            ws.Cells[row + 2, 1] = "Дата создания отчета:";
            ws.Cells[row + 2, 1].Font.Bold = true;
            ws.Cells[row + 2, 6] = DateTime.Now;
        }

        void Otchet3()
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            DateTime d2 = OtchetDP2.SelectedDate.Value.Date;
            //Новая книга
            Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet ws = wb.Sheets[1];
            Microsoft.Office.Interop.Excel.Range range = ws.Range["A1", "F1"];
            range.Merge();
            range.Value = "Отчет поступивших пациентов по " + d2;
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //Заголовки
            ws.Cells[2, 1] = "Фамилия";
            ws.Cells[2, 1].Font.Bold = true;
            ws.Cells[2, 2] = "Имя";
            ws.Cells[2, 2].Font.Bold = true;
            ws.Cells[2, 3] = "Отчество";
            ws.Cells[2, 3].Font.Bold = true;
            ws.Cells[2, 4] = "Дата рождения";
            ws.Cells[2, 4].Font.Bold = true;
            ws.Cells[2, 5] = "Дата поступления";
            ws.Cells[2, 5].Font.Bold = true;
            ws.Cells[2, 6] = "Диагноз";
            ws.Cells[2, 6].Font.Bold = true;
            int row = 3;
            var res = ConnectDB.GetCont().Electronic_medical_card.ToList();
            res = res.Where(x => x.date_receipt.Date <= d2).ToList();
            foreach (var item in res)
            {
                dynamic otchet = item;
                ws.Cells[row, 1] = otchet.Patient.Surname;
                ws.Cells[row, 2] = otchet.Patient.First_name;
                ws.Cells[row, 3] = otchet.Patient.Patronymic;
                ws.Cells[row, 4] = otchet.Patient.date_of_birth;
                ws.Cells[row, 5] = otchet.date_receipt;
                List<string> m = new List<string>();
                foreach (var a in otchet.MKB)
                {
                    m.Add(a.name_diagnosis);
                }
                ws.Cells[row, 6] = string.Join(", ", m);
                row++;
            }
            int abba = row - 1;
            ws.Cells[row, 1] = "Кол-во пациентов:";
            ws.Cells[row, 1].Font.Bold = true;
            ws.Cells[row, 6] = row - 3;
            ws.Cells[row, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            ws.Cells[row + 1, 1] = "Автор:";
            ws.Cells[row + 1, 1].Font.Bold = true;
            ws.Cells[row + 1, 6] = Right.curUser.surname + " " + Right.curUser.first_name + " " + Right.curUser.patronymic;
            ws.Cells[row + 1, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            ws.Cells[row + 2, 1] = "Дата создания отчета:";
            ws.Cells[row + 2, 1].Font.Bold = true;
            ws.Cells[row + 2, 6] = DateTime.Now;
        }
        void OtchetPoDiaposonu()
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            DateTime d1 = OtchetDP.SelectedDate.Value.Date;
            DateTime d2 = OtchetDP2.SelectedDate.Value.Date;
            //Новая книга
            Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet ws = wb.Sheets[1];
            Microsoft.Office.Interop.Excel.Range range = ws.Range["A1", "F1"];
            range.Merge();
            range.Value = "Отчет поступивших пациентов с " + d1 + " по " + d2;
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //Заголовки
            ws.Cells[2, 1] = "Фамилия";
            ws.Cells[2, 1].Font.Bold = true;
            ws.Cells[2, 2] = "Имя";
            ws.Cells[2, 2].Font.Bold = true;
            ws.Cells[2, 3] = "Отчество";
            ws.Cells[2, 3].Font.Bold = true;
            ws.Cells[2, 4] = "Дата рождения";
            ws.Cells[2, 4].Font.Bold = true;
            ws.Cells[2, 5] = "Дата поступления";
            ws.Cells[2, 5].Font.Bold = true;
            ws.Cells[2, 6] = "Диагноз";
            ws.Cells[2, 6].Font.Bold = true;
            int row = 3;
            var res = ConnectDB.GetCont().Electronic_medical_card.ToList();
            res = res.Where(x => x.date_receipt.Date >= d1 && x.date_receipt.Date <= d2).ToList();
            foreach (var item in res)
            {
                dynamic otchet = item;
                ws.Cells[row, 1] = otchet.Patient.Surname;
                ws.Cells[row, 2] = otchet.Patient.First_name;
                ws.Cells[row, 3] = otchet.Patient.Patronymic;
                ws.Cells[row, 4] = otchet.Patient.date_of_birth;
                ws.Cells[row, 5] = otchet.date_receipt;
                List<string> m = new List<string>();
                foreach (var a in otchet.MKB)
                {
                    m.Add(a.name_diagnosis);
                }
                ws.Cells[row, 6] = string.Join(", ", m);
                row++;
            }
            int abba = row - 1;
            ws.Cells[row, 1] = "Кол-во пациентов:";
            ws.Cells[row, 1].Font.Bold = true;
            ws.Cells[row, 6] = row - 3;
            ws.Cells[row, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            ws.Cells[row + 1, 1] = "Автор:";
            ws.Cells[row + 1, 1].Font.Bold = true;
            ws.Cells[row + 1, 6] = Right.curUser.surname + " " + Right.curUser.first_name + " " + Right.curUser.patronymic;
            ws.Cells[row + 1, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            ws.Cells[row + 2, 1] = "Дата создания отчета:";
            ws.Cells[row + 2, 1].Font.Bold = true;
            ws.Cells[row + 2, 6] = DateTime.Now;
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new NavMenu());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new AddCase());
        }

        private void Otchet2Btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
