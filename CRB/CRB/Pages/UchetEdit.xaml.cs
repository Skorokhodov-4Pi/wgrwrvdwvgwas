using CRB.AppData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для UchetEdit.xaml
    /// </summary>
    public partial class UchetEdit : Page
    {
		public UchetEdit()
        {
            InitializeComponent();
            DataContext = CurElMedCard.card ?? new Electronic_medical_card();

		}

		private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            ConnectDB.GetCont().SaveChanges();
            Nav.frame.GoBack();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //Nav.frame.Navigate(new EditMKB((sender as Button).DataContext as MKB));
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
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
		}

    }
}
