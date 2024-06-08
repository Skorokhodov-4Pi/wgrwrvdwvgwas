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
    /// Логика взаимодействия для AddCase.xaml
    /// </summary>
    public partial class AddCase : Page
    {
        public Electronic_medical_card em;
        public AddCase()
        {
            InitializeComponent();
            PacCmb.ItemsSource = ConnectDB.GetCont().Patient.ToList();

            MKBCmb.ItemsSource = ConnectDB.GetCont().MKB.ToList();
            DepCmb.ItemsSource = ConnectDB.GetCont().Departments.ToList();

            var sf = ConnectDB.GetCont().Staff.ToList();
            sf = sf.Where(x => x.id_position == 1).ToList();
            StaffCmb.ItemsSource = sf;
            OutcomesCmb.ItemsSource = ConnectDB.GetCont().Outcomes.ToList();
            DataContext = em = new Electronic_medical_card();
            MKBLV.ItemsSource = em.MKB.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            em.MKB.Add(MKBCmb.SelectedItem as MKB);
            MKBLV.ItemsSource = em.MKB.ToList();
            MKBCmb.ItemsSource = ConnectDB.GetCont().MKB.ToList().Except(em.MKB.ToList());
            MKBCmb.SelectedIndex = 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if((PacCmb.SelectedItem as Patient).policy_number == "Нет данных")
            {
                MessageBox.Show("У пациента отсутствует актуальный полис","Невозможно добавить случай", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            em.date_receipt = DateTime.Now;
            em.beds.status_bed = "занята";
            ConnectDB.GetCont().Electronic_medical_card.Add(em);
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var delMkb = MKBLV.SelectedItems.Cast<MKB>().ToList();
            foreach (var del in delMkb)
                em.MKB.Remove(del);
            MKBLV.ItemsSource = em.MKB.ToList();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var ab = ConnectDB.GetCont().beds.ToList();
            ab = ab.Where(x => x.status_bed != "занята").ToList();
            NumRoomCmb.ItemsSource = ab;
        }

        private void PacCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
        private void Update()
        {
            var ab = ConnectDB.GetCont().beds.ToList();
            ab = ab.Where(x => x.status_bed != "занята").ToList();
            var p = PacCmb.SelectedItem as Patient;
            if (p != null)
                ab = ab.Where(x => x.hospital_rooms.type_room.Contains(p.gender[0])).ToList();
            NumRoomCmb.ItemsSource = ab;
        }
    }
}
