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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Windows.Threading;

namespace KanDic
{
    /// <summary>
    /// Build.xaml 的交互逻辑
    /// </summary>
    public partial class Build : MetroWindow
    {
        public class Board
        {
            public string User { set; get; }
            public string NewGet { set; get; }
            public string UseFormula { set; get; }
            public string FaceColor { set; get; }
            public string FormulaTimes { set; get; }
            public string GetCount { set; get; }
            public string GetRate { set; get; }

            public Board() { }
        }

        public class UserInfo
        {
            public string UserID { set; get; }
            public int QuickBuild { set; get; }
            public int Material { set; get; }
            public double FaceRate { set; get; }
            public int Fuel { set; get; }
            public int Steel { set; get; }
            public int Ammo { set; get; }
            public int Aluminum { set; get; }

            public UserInfo() { }
        }

        public List<Board> nEurope = new List<Board>();//普建欧洲榜
        public List<Board> nAfrica = new List<Board>();//普建非洲榜
        public List<Board> hEurope = new List<Board>();//大建欧洲榜
        public List<Board> hAfrica = new List<Board>();//大建非洲榜

        public bool isdevelop = false;
        public bool ishugebuild = false;
        public TimeSpan[] timecount = new TimeSpan[4];
        DispatcherTimer timer = new DispatcherTimer();
        public int builddock, boardnum, fuel, steel, ammo, aluminium;

        public Build()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            timer.Start();
            for (int i = 0; i < 4; i++)
            {
                timecount[i] = new TimeSpan(0);
            }
            BoardInit();
            UserInit();
        }

        private void UserInit()
        {
            UserBox.DataContext = null;
        }

        private void BoardInit()
        {
            leaderboard.DataContext = nEurope;
        }

        private void FormulaAnimation()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 700;
            da.Duration = TimeSpan.FromSeconds(0.2);
            FormulaBox.BeginAnimation(Canvas.LeftProperty, da);
        }

        private void Rectangle_MLBD(object sender, MouseButtonEventArgs e)
        {
            FormulaAnimation();
        }

        private void BuildStart_Click(object sender, RoutedEventArgs e)
        {
            switch (builddock)
            {
                case 1:
                    dock1.Input.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    dock2.Input.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    dock3.Input.Visibility = Visibility.Collapsed;
                    break;
                case 4:
                    dock4.Input.Visibility = Visibility.Collapsed;
                    break;

            }
            timecount[builddock - 1] = new TimeSpan(0, 1, 0);
            Console.WriteLine("{0}/{1}/{2}/{3}", fuel, steel, ammo, aluminium);
            FormulaAnimation();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (timecount[0].Equals(new TimeSpan(0))) dock1.Input.Visibility = Visibility.Visible;
            else timecount[0] = timecount[0].Subtract(new TimeSpan(0, 0, 1));
            if (timecount[1].Equals(new TimeSpan(0))) dock2.Input.Visibility = Visibility.Visible;
            else timecount[1] = timecount[1].Subtract(new TimeSpan(0, 0, 1));
            if (timecount[2].Equals(new TimeSpan(0))) dock3.Input.Visibility = Visibility.Visible;
            else timecount[2] = timecount[2].Subtract(new TimeSpan(0, 0, 1));
            if (timecount[3].Equals(new TimeSpan(0))) dock4.Input.Visibility = Visibility.Visible;
            else timecount[3] = timecount[3].Subtract(new TimeSpan(0, 0, 1));
            dock1.RestTime.Text = string.Format("{0:T}", timecount[0]);
            dock2.RestTime.Text = string.Format("{0:T}", timecount[1]);
            dock3.RestTime.Text = string.Format("{0:T}", timecount[2]);
            dock4.RestTime.Text = string.Format("{0:T}", timecount[3]);
        }

        private void DevelopStart_Click(object sender, RoutedEventArgs e)
        {
            FormulaAnimation();
        }
    }
}
