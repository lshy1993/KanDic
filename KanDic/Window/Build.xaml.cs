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
using System.Net;
using System.IO;
using System.Configuration;

namespace KanDic
{
    /// <summary>
    /// Build.xaml 的交互逻辑
    /// </summary>
    public partial class Build : MetroWindow
    {
        //排行榜类
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
        //用户信息类
        public class UserInfo
        {
            public string NickName { set; get; }//昵称
            public int Fuel { set; get; }
            public int Steel { set; get; }
            public int Ammo { set; get; }
            public int Aluminium { set; get; }
            public int Material { set; get; }
            public int QuickBuild { set; get; }
            public List<string> FinishTime { set; get; }//建造队列
            public TimeSpan[] WaitTime = new TimeSpan[4];//建造队列
            public List<int> ShipID { set; get; }//建造船Number
            public double FaceRate { set; get; }//脸黑

            public UserInfo()
            {
                WaitTime[0] = new TimeSpan(0);
                WaitTime[1] = new TimeSpan(0);
                WaitTime[2] = new TimeSpan(0);
                WaitTime[3] = new TimeSpan(0);
            }
        }

        DispatcherTimer timer = new DispatcherTimer();//计时器
        
        public UserInfo tidu = new UserInfo();//提督信息

        public List<Board> nEurope = new List<Board>();//普建欧洲榜
        public List<Board> nAfrica = new List<Board>();//普建非洲榜
        public List<Board> hEurope = new List<Board>();//大建欧洲榜
        public List<Board> hAfrica = new List<Board>();//大建非洲榜

        public bool isdevelop = false;
        public bool ishugebuild = false;

        public int builddock, fuel, steel, ammo, aluminium, material;

        public string userseed;

        public Build()
        {
            InitializeComponent();
            try
            {
                userseed = ConfigurationManager.AppSettings["randomseed"];
                //检测是否首次开启
                if (ConfigurationManager.AppSettings["nickname"] == "")
                {
                    //注册网名与seed对应
                    Register();
                }
                //下载用户数据
                Download();
                //更新界面
                UIRefresh();
                //实时排行榜
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Tick += timer_Tick;
                timer.Start();
            }
            catch(Exception e)
            {
                Cat.Visibility = Visibility.Visible;
                ErrorBox.Text = e.ToString();
            }
        }

        private void Register()
        {
            tidu.NickName = "lucky";
            //MessageBox.Show("测试阶段无须用户名");
        }

        private void UIRefresh()
        {
            UserBox.DataContext = tidu;
        }

        private void Download()
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "&userseed=" + userseed + "&nickname=" + tidu.NickName;
            byte[] data = encoding.GetBytes(postData);
            //提交userseed获取tidu信息
            HttpWebRequest htr = (HttpWebRequest)WebRequest.Create("http://1.pngbase.sinaapp.com/userinfo1.php");
            htr.Method = "POST";
            htr.ContentType = "application/x-www-form-urlencoded";
            htr.ContentLength = data.Length;

            Stream newStream = htr.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse hwr = (HttpWebResponse)htr.GetResponse();
            Stream responseStream = hwr.GetResponseStream();

            StreamReader streamReader = new StreamReader(responseStream);
            var html = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            htr.Abort();
            hwr.Close();
            Console.WriteLine(html);
            tidu = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfo>(html);
            for (int i = 0; i < 4; i++)
            {
                var dt = DateTime.Parse(tidu.FinishTime[i]);
                TimeSpan ts = dt - DateTime.Now;
                tidu.WaitTime[i] = new TimeSpan(ts.Hours, ts.Minutes, ts.Seconds);
                Console.WriteLine(tidu.WaitTime[i]);
            }
        }

        private void BuildStart_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("{0}/{1}/{2}/{3}", fuel, steel, ammo, aluminium);
            material = 1;
            post();
            Download();
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
            FormulaAnimation();
        }

        private void post()
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "&userseed=" + userseed + "&fuel=" + fuel + "&steel=" + steel + "&ammo=" + ammo + "&aluminium=" + aluminium + "&material=" + material + "&dock=" + builddock;
            byte[] data = encoding.GetBytes(postData);
            //提交公式与紫菜，返回时间
            HttpWebRequest htr = (HttpWebRequest)WebRequest.Create("http://1.pngbase.sinaapp.com/build.php");
            htr.Method = "POST";
            htr.ContentType = "application/x-www-form-urlencoded";
            htr.ContentLength = data.Length;
            Stream newStream = htr.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse hwr = (HttpWebResponse)htr.GetResponse();
            Stream responseStream = hwr.GetResponseStream();

            StreamReader streamReader = new StreamReader(responseStream);
            var html = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            htr.Abort();
            hwr.Close();

            Console.WriteLine(html);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (tidu.WaitTime[0].Equals(new TimeSpan(0))) dock1.Input.Visibility = Visibility.Visible;
            else tidu.WaitTime[0] = tidu.WaitTime[0].Subtract(new TimeSpan(0, 0, 1));
            if (tidu.WaitTime[1].Equals(new TimeSpan(0))) dock2.Input.Visibility = Visibility.Visible;
            else tidu.WaitTime[1] = tidu.WaitTime[1].Subtract(new TimeSpan(0, 0, 1));
            if (tidu.WaitTime[2].Equals(new TimeSpan(0))) dock3.Input.Visibility = Visibility.Visible;
            else tidu.WaitTime[2] = tidu.WaitTime[2].Subtract(new TimeSpan(0, 0, 1));
            if (tidu.WaitTime[3].Equals(new TimeSpan(0))) dock4.Input.Visibility = Visibility.Visible;
            else tidu.WaitTime[3] = tidu.WaitTime[3].Subtract(new TimeSpan(0, 0, 1));
            dock1.RestTime.Text = string.Format("{0:T}", tidu.WaitTime[0]);
            dock2.RestTime.Text = string.Format("{0:T}", tidu.WaitTime[1]);
            dock3.RestTime.Text = string.Format("{0:T}", tidu.WaitTime[2]);
            dock4.RestTime.Text = string.Format("{0:T}", tidu.WaitTime[3]);
        }

        private void DevelopStart_Click(object sender, RoutedEventArgs e)
        {
            FormulaAnimation();
        }

        private void Rectangle_MLBD(object sender, MouseButtonEventArgs e)
        {
            FormulaAnimation();
        }

        private void FormulaAnimation()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 700;
            da.Duration = TimeSpan.FromSeconds(0.2);
            FormulaBox.BeginAnimation(Canvas.LeftProperty, da);
        }
    }
}
