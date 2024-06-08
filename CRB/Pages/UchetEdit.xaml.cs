using CRB.AppData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using Word = Microsoft.Office.Interop.Word;

namespace CRB.Pages
{
    /// <summary>
    /// Логика взаимодействия для UchetEdit.xaml
    /// </summary>
    public partial class UchetEdit : Page
    {
		public UchetEdit()
        {
            InitializeComponent();
            DataContext = CurElMedCard.card ?? new Electronic_medical_card();
            if(CurElMedCard.card.Outcomes.id_outcomes != 1) { EpBtn.Visibility = Visibility.Collapsed; }
		}

		private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

            if (CmbOutcomes.SelectedIndex == 0)
            {
                MessageBoxResult result = MessageBox.Show("Вы хотите создать эпикриз?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    Epicriz();
                CurElMedCard.card.date_discharge = DateTime.Now;
                CurElMedCard.card.beds.status_bed = "свободна";
            }
            ConnectDB.GetCont().SaveChanges();
            Nav.frame.GoBack();
            
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //Nav.frame.Navigate(new EditMKB((sender as Button).DataContext as MKB));
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new Uchet());
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new EditMKB());
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var delMkb = MKBLV.SelectedItems.Cast<MKB>().ToList();
			foreach(var del in delMkb)
            CurElMedCard.card.MKB.Remove(del);
            MKBLV.ItemsSource = CurElMedCard.card.MKB.ToList();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			MKBLV.ItemsSource = CurElMedCard.card.MKB.ToList();
            CmbOutcomes.ItemsSource = ConnectDB.GetCont().Outcomes.ToList();
            NumRoomCmb.ItemsSource = ConnectDB.GetCont().beds.ToList();
            NumBedCmb.ItemsSource = ConnectDB.GetCont().beds.ToList();
		}

        private void CmbOutcomes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        void Epicriz()
        {
            var app = new Word.Application();
            Word.Document document = app.Documents.Add();
            Word.Paragraph paragraph = document.Paragraphs.Add();
            Word.Range range = paragraph.Range;
            range.Text = "ВЫПИСНОЙ ЭПИКРИЗ";
            range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            range.InsertParagraphAfter();

            Word.Paragraph paragraph2 = document.Paragraphs.Add();
            Word.Range range2 = paragraph2.Range;
            range2.Text = "Наименование медицинской организации (фамилия, имя, отчество (при наличии) индивидуального " +
                "предпринимателя, осуществляющего медицинскую деятельность), ОГРН (ОГРНИП): БЮДЖЕТНОЕ УЧРЕЖДЕНИЕ ЗДРАВООХРАНЕНИЯ" +
                " ВОРОНЕЖСКОЙ ОБЛАСТИ `БОРИСОГЛЕБСКАЯ РАЙОННАЯ БОЛЬНИЦА`, 1023600610000";
            range2.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphThaiJustify;
            range2.InsertParagraphAfter();

            Word.Paragraph paragraph3 = document.Paragraphs.Add();
            Word.Range range3 = paragraph3.Range;
            range3.Text = "Наименование отделения (структурного подразделения): Терапевтическое отделение";
            range3.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphThaiJustify;
            range3.InsertParagraphAfter();

            Word.Paragraph paragraph4 = document.Paragraphs.Add();
            Word.Range range4 = paragraph4.Range;
            range4.Text = "Номер медицинской карты: " + CurElMedCard.card.id_case;
            range4.InsertParagraphAfter();

            Word.Paragraph paragraph5 = document.Paragraphs.Add();
            Word.Range range5 = paragraph5.Range;
            range5.Text = "Сведения о пациенте:";
            range5.InsertParagraphAfter();

            Word.Paragraph paragraph6 = document.Paragraphs.Add();
            Word.Range range6 = paragraph6.Range;
            range6.Text = "Фамилия, имя, отчество (при наличии): " + CurElMedCard.card.Patient.fullname2;
            range6.InsertParagraphAfter();

            Word.Paragraph paragraph7 = document.Paragraphs.Add();
            Word.Range range7 = paragraph7.Range;
            range7.Text = "Дата рождения: " + CurElMedCard.card.Patient.date_of_birth + " " + "Пол: " + CurElMedCard.card.Patient.gender;
            range7.InsertParagraphAfter();

            Word.Paragraph paragraph8 = document.Paragraphs.Add();
            Word.Range range8 = paragraph8.Range;
            range8.Text = "Регистрация по месту жительства:";
            range8.InsertParagraphAfter();

            Word.Paragraph paragraph9 = document.Paragraphs.Add();
            Word.Range range9 = paragraph9.Range;
            range9.Text = CurElMedCard.card.Patient.registered_address;
            range9.InsertParagraphAfter();

            Word.Paragraph paragraph10 = document.Paragraphs.Add();
            Word.Range range10 = paragraph10.Range;
            range10.Text = "\rРегистрация по месту пребывания:";
            range10.InsertParagraphAfter();

            Word.Paragraph paragraph11 = document.Paragraphs.Add();
            Word.Range range11 = paragraph11.Range;
            range11.Text = CurElMedCard.card.Patient.actual_address;
            range11.InsertParagraphAfter();

            Word.Paragraph paragraph12 = document.Paragraphs.Add();
            Word.Range range12 = paragraph12.Range;
            range12.Text = "Поступил: в стационар";
            range12.InsertParagraphAfter();

            Word.Paragraph paragraph13 = document.Paragraphs.Add();
            Word.Range range13 = paragraph13.Range;
            range13.Text = "Период нахождения в стационаре, дневном стационаре: с" + " " + CurElMedCard.card.date_receipt + " " + "по" + " " + CurElMedCard.card.date_discharge;
            range13.InsertParagraphAfter();

            DateTime d1 = CurElMedCard.card.date_receipt;
            DateTime d2 = (DateTime)CurElMedCard.card.date_discharge;
            TimeSpan difference = d2 - d1;
            int days = difference.Days;

            Word.Paragraph paragraph14 = document.Paragraphs.Add();
            Word.Range range14 = paragraph14.Range;
            range14.Text = "Количество дней нахождения в медицинской организации:" + " " + days;
            range14.InsertParagraphAfter();

            Word.Paragraph paragraph15 = document.Paragraphs.Add();
            Word.Range range15 = paragraph15.Range;
            range15.Text = "Исход госпитализации: выписан";
            range15.InsertParagraphAfter();

            Word.Paragraph paragraph16 = document.Paragraphs.Add();
            Word.Range range16 = paragraph16.Range;
            range16.Text = "Результат госпитализации: выздоровление - 1, улучшение - 2, без перемен - 3, ухудшение - 4.";
            range16.InsertParagraphAfter();

            Word.Paragraph paragraph17 = document.Paragraphs.Add();
            Word.Range range17 = paragraph17.Range;
            range17.Text = "Форма оказания медицинской помощи: плановая - 1, экстренная - 2(указать) _________________________________________________________________";
            range17.InsertParagraphAfter();

            Word.Paragraph paragraph18 = document.Paragraphs.Add();
            Word.Range range18 = paragraph18.Range;
            range18.Text = "Дополнительные сведения о пациенте и госпитализации: ______________________";
            range18.InsertParagraphAfter();

            Word.Paragraph paragraph19 = document.Paragraphs.Add();
            Word.Range range19 = paragraph19.Range;
            range19.Text = "\rЗаключительный клинический диагноз:";
            range19.InsertParagraphAfter();

            Word.Paragraph paragraph20 = document.Paragraphs.Add();
            Word.Range range20 = paragraph20.Range;
            range20.Text = "Основное заболевание:" + " " + CurElMedCard.card.MKBList + " " + "код по МКБ:" + " " + CurElMedCard.card.MKBList2;
            range20.InsertParagraphAfter();

            Word.Paragraph paragraph21 = document.Paragraphs.Add();
            Word.Range range21 = paragraph21.Range;
            range21.Text = "Осложнения основного заболевания ____________________ код по МКБ ______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________";
            range21.InsertParagraphAfter();

            Word.Paragraph paragraph22 = document.Paragraphs.Add();
            Word.Range range22 = paragraph22.Range;
            range22.Text = "Внешняя причина при травмах, отравлениях ____________ код по МКБ ___________________________________________________________________________________________________________________________________________________________________________________________________________________________________________";
            range22.InsertParagraphAfter();

            Word.Paragraph paragraph23 = document.Paragraphs.Add();
            Word.Range range23 = paragraph23.Range;
            range23.Text = "Сопутствующие заболевания ___________________________ код по МКБ ___________________________________________________________________________________________________________________________________________________________________________________________________________________________________________";
            range23.InsertParagraphAfter();

            Word.Paragraph paragraph24 = document.Paragraphs.Add();
            Word.Range range24 = paragraph24.Range;
            range24.Text = "Дополнительные сведения о заболевании ___________________________________________________________________________________________________________________________________________________________________________________________";
            range24.InsertParagraphAfter();

            Word.Paragraph paragraph25 = document.Paragraphs.Add();
            Word.Range range25 = paragraph25.Range;
            range25.Text = "Состояние при поступлении:__________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________";
            range25.InsertParagraphAfter();

            Word.Paragraph paragraph26 = document.Paragraphs.Add();
            Word.Range range26 = paragraph26.Range;
            range26.Text = "Проведенные обследования, лечение, медицинская реабилитация:\rОсмотры врачей-специалистов, консилиумы врачей, врачебные комиссии:\r___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________";
            range26.InsertParagraphAfter();

            Word.Paragraph paragraph27 = document.Paragraphs.Add();
            Word.Range range27 = paragraph27.Range;
            range27.Text = "Результаты медицинского обследования:\r___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________\r";
            range27.InsertParagraphAfter();

            Word.Paragraph paragraph28 = document.Paragraphs.Add();
            Word.Range range28 = paragraph28.Range;
            range28.Text = "Применение лекарственных препаратов (включая химиотерапию, вакцинацию), медицинских изделий, лечебного питания:";
            range28.InsertParagraphAfter();

            Word.Paragraph paragraph29 = document.Paragraphs.Add();
            Word.Range range29 = paragraph29.Range;
            range29.Text = "___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________";
            range29.InsertParagraphAfter();

            Word.Paragraph paragraph30 = document.Paragraphs.Add();
            Word.Range range30 = paragraph30.Range;
            range30.Text = "Трансфузии (переливания) донорской крови и (или) ее компонентов:\r___________________________________________________________________________\r\n___________________________________________________________________________\r";
            range30.InsertParagraphAfter();

            Word.Paragraph paragraph31 = document.Paragraphs.Add();
            Word.Range range31 = paragraph31.Range;
            range31.Text = "Оперативные вмешательства (операции), включая сведения об\r\nанестезиологическом пособии:\r___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________";
            range31.InsertParagraphAfter();

            Word.Paragraph paragraph32 = document.Paragraphs.Add();
            Word.Range range32 = paragraph32.Range;
            range32.Text = "Медицинские вмешательства:\r___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________";
            range32.InsertParagraphAfter();

            Word.Paragraph paragraph33 = document.Paragraphs.Add();
            Word.Range range33 = paragraph33.Range;
            range33.Text = "Дополнительные сведения:\r___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________";
            range33.InsertParagraphAfter();

            Word.Paragraph paragraph34 = document.Paragraphs.Add();
            Word.Range range34 = paragraph34.Range;
            range34.Text = "Состояние при выписке, трудоспособность, листок нетрудоспособности:\r___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________";
            range34.InsertParagraphAfter();

            Word.Paragraph paragraph35 = document.Paragraphs.Add();
            Word.Range range35 = paragraph35.Range;
            range35.Text = "Рекомендации:\r___________________________________________________________________________\r\n___________________________________________________________________________\r\n___________________________________________________________________________";
            range35.InsertParagraphAfter();

            Word.Paragraph paragraph36 = document.Paragraphs.Add();
            Word.Range range36 = paragraph36.Range;
            range36.Text = "Фамилия, имя, отчество (при наличии), должность, специальность, подпись лечащий врач:" + " " + CurElMedCard.card.Staff.fullname_Staff + "\rзаведующий отделением: Майстренко Елена Александровна";
            range36.InsertParagraphAfter();

            Word.Paragraph paragraph37 = document.Paragraphs.Add();
            Word.Range range37 = paragraph37.Range;
            DateTime dt3 = DateTime.Now;
            int day = dt3.Day;
            int year = dt3.Year;
            string[] monthNames = new string[] {
            "января", "февраля", "марта", "апреля", "мая", "июня",
            "июля", "августа", "сентября", "октября", "ноября", "декабря"
            };
            int monthIndex = dt3.Month - 1;
            string monthName = monthNames[monthIndex];
            range37.Text = "\"" + day + "\"" + " " + monthName + " " + year + " г.";

            app.Visible = true;
        }

        private void EpBtn_Click(object sender, RoutedEventArgs e)
        {
            Epicriz();
        }
    }
}
