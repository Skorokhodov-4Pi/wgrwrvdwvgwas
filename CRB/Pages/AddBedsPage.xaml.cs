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
    /// Логика взаимодействия для AddBedsPage.xaml
    /// </summary>
    public partial class AddBedsPage : Page
    {
        public AddBedsPage()
        {
            InitializeComponent();
            RoomCmb.ItemsSource = ConnectDB.context.hospital_rooms.ToList();
            GendreCmb.ItemsSource = new[] { "м", "ж" };
        }

        private void AddBedBtn_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(RoomCmb.Text, out int num) && int.TryParse(CountBedsTxt.Text, out int count))
            {
                int bed = 1;
                var room = ConnectDB.GetCont().hospital_rooms.FirstOrDefault(x => x.id_room == num);
                if (room == null)
                    ConnectDB.GetCont().hospital_rooms.Add(room = new hospital_rooms()
                    {
                        id_room = num,
                        type_room = GendreCmb.SelectedValue.ToString(),
                    });
                else
                {
                    bed = ConnectDB.GetCont().beds.OrderByDescending(x=>x.num_bed).FirstOrDefault(x => x.id_room == room.id_room).num_bed;
                    bed++;
                }
                for (int i = bed; i < bed + count; i++)
                    ConnectDB.context.beds.Add(new beds()
                    {
                        id_bed = 0,
                        num_bed = i,
                        status_bed = "свободно",
                        id_room = room.id_room
                    });
            }
            ConnectDB.GetCont().SaveChanges();
            Nav.frame.GoBack();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Nav.frame.GoBack();
        }
    }
}
