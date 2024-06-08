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
	/// Логика взаимодействия для EditMKB.xaml
	/// </summary>
	public partial class EditMKB : Page
	{

		public EditMKB()
		{
			InitializeComponent();
			var mkb = ConnectDB.GetCont().MKB.ToList();
			mkb = mkb.Except(CurElMedCard.card.MKB).ToList();
			MKBCmb.ItemsSource = mkb;
		}

		private void SaveBtn_Click(object sender, RoutedEventArgs e)
		{
			CurElMedCard.card.MKB.Add(MKBCmb.SelectedItem as MKB);
			Nav.frame.GoBack();	
        }

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			Nav.frame.GoBack();
		}

		private void MKBCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
