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
        Users curUser = Right.curUser;
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

            if (curUser != null )
            {
                if(curUser.id_role == 1) {
                    Nav.Visible(OtchetBtn);
                }
                else if(curUser.id_role == 2)
                {
                    Nav.Visible(OtchetBtn);
                }
                else
                {
                    Nav.Collapsed(OtchetBtn);
                }
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
            
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new Avtoris2());
        }
    }
}
