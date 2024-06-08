using CRB.AppData;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;
using System;
using MaterialDesignThemes.Wpf;
using System.Windows.Navigation;
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
            timeBlock.Tick += new EventHandler(Timer_Tick);
            timeBlock.Interval = new TimeSpan(0, 0, 0, 10);
        }
        DispatcherTimer timeBlock;
        int countEnter = 0;
        string cap = "";
        private void Captcha()
        {
            CaptchaStack.Visibility = Visibility.Visible;
            string allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            string[] arr = allowchar.Split(',');
            Random r = new Random();
            cap = "";
            cap += CaptchaTxt1.Text = arr[r.Next(0, arr.Length)];
            cap += CaptchaTxt2.Text = arr[r.Next(0, arr.Length)];
            cap += CaptchaTxt3.Text = arr[r.Next(0, arr.Length)];
            cap += CaptchaTxt4.Text = arr[r.Next(0, arr.Length)];
            CaptchaTxt1.Margin = new Thickness(0, r.Next(0, 20), 0, 0);
            CaptchaTxt2.Margin = new Thickness(0, r.Next(0, 20), 0, 0);
            CaptchaTxt3.Margin = new Thickness(0, r.Next(0, 20), 0, 0);
            CaptchaTxt4.Margin = new Thickness(0, r.Next(0, 20), 0, 0);
        }
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
        private void Timer_Tick(object sender, EventArgs e)
        {
            btnVhod.IsEnabled = true;
            timeBlock.IsEnabled = false;
        }

        private void btnVhod_Click(object sender, RoutedEventArgs e)
        {
            if (!ConnectDB.GetCont().Staff.Any(x => x.login == LoginUser.Text && x.password == PasswordUser.Password))
            {
                countEnter++;
                Captcha();
                if (countEnter == 3)
                {
                    countEnter = 0;
                    btnVhod.IsEnabled = false;
                    timeBlock.IsEnabled = true;
                    MessageBox.Show("Были произведены 3 неверных попытки, авторизация заблокирована на 10 секунд");
                }
                TxtBlockMessage.Visibility = Visibility.Visible;
                return;
            }
            cap = "";
            CaptchaStack.Visibility = Visibility.Collapsed;
            TxtBlockMessage.Visibility = Visibility.Collapsed;
            Right.curUser = ConnectDB.GetCont().Staff.FirstOrDefault(x => x.login == LoginUser.Text && x.password == PasswordUser.Password);
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();          
        }
        private void CaptchaTxt1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Captcha();
        }
    }
}
