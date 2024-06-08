using CRB.AppData;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;
using System;
namespace CRB.Pages
{
    /// <summary>
    /// Логика взаимодействия для Avtoris.xaml
    /// </summary>
    public partial class Avtoris : Window
    {
        public Avtoris()
        {
            InitializeComponent();
            timeBlock = new DispatcherTimer();
            timeBlock.Tick += new EventHandler(timer_Tick);
            timeBlock.Interval = new TimeSpan(0, 0, 0, 10);
        }

        DispatcherTimer timeBlock;
        int countEnter = 0;
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            btnVhod.IsEnabled = true;
            timeBlock.IsEnabled = false;
        }

        private void btnVhod_Click(object sender, RoutedEventArgs e)
        {
            if (!ConnectDB.GetCont().Users.Any(x => x.login == LoginUser.Text && x.password == PasswordUser.Password))
            {
                countEnter++;
                if (countEnter == 3)
                {
                    countEnter = 0;
                    btnVhod.IsEnabled = false;
                    timeBlock.IsEnabled = true;
                    MessageBox.Show("Были произведены 3 неверных попытки, авторизация заблокирована на 10 секунд");
                }
                return;
            }
            Right.curUser = ConnectDB.GetCont().Users.FirstOrDefault(x => x.login == LoginUser.Text && x.password == PasswordUser.Password);
            Nav.frame.Navigate(new Uchet());
        }
    }
}
