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
        #region 临时类
        //排行榜类
        public class Board
        {
            public string NickName { set; get; }
            public int ShipID { set; get; }
            public string NewGet { set; get; }
            public string Formula { set; get; }
            public double FaceRate { set; get; }
            public int UseTimes { set; get; }
            public int GetCount { set; get; }
            public double GetRate { set; get; }

            public Board() { }
        }
        //最新信息类
        public class NEWS
        {
            public string NickName { set; get; }
            public int ShipID { set; get; }
            public string NewGet { set; get; }
            public string Formula { set; get; }
            public string LastTime { set; get; }

            public NEWS() { }
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
        #endregion

        DispatcherTimer timer = new DispatcherTimer();//计时器
        public UserInfo tidu = new UserInfo();//提督信息
        public List<Board> nEurope = new List<Board>();//普建欧洲榜非洲榜
        public List<NEWS> news = new List<NEWS>();//最新播报列表
        public int builddock, boardmode, fuel, steel, ammo, aluminium, material, timecount;
        //建造队列，排行类型，油，钢，弹，铝，资材，计时器
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
                boardmode = 1;
                //下载用户数据
                DownloadUser();
                DownloadBoard();
                DownloadNews();
                //更新界面
                timer_Tick(null, null);
                UIRefresh();
                //倒计时与实时排行榜
                timecount = 0;
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
            LeaderBoard.DataContext = nEurope;
        }

        #region 拿到船并提交数据
        public void GetShip()
        {

        }
        #endregion

        #region 下载用户数据
        private void DownloadUser()
        {
            string postData = "&userseed=" + userseed + "&nickname=" + tidu.NickName;
            string url = "http://1.pngbase.sinaapp.com/userinfo.php";
            tidu = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfo>(post(postData, url));
            for (int i = 0; i < 4; i++)
            {
                if (tidu.FinishTime[i] == "0000-00-00 00:00:00")
                {
                    tidu.WaitTime[i] = new TimeSpan(0);
                }
                else
                {
                    var dt = DateTime.Parse(tidu.FinishTime[i]);
                    TimeSpan ts = dt - DateTime.Now;
                    if (ts <= new TimeSpan(0))
                    {
                        tidu.WaitTime[i] = new TimeSpan(0);
                    }
                    else
                    {
                        tidu.WaitTime[i] = new TimeSpan(ts.Hours, ts.Minutes, ts.Seconds);
                    }
                }
            }
        }
        #endregion

        #region 下载排行数据
        private void DownloadBoard()
        {
            string postData = "&boardmode=" + boardmode;
            string url = "http://1.pngbase.sinaapp.com/board.php";
            nEurope = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Board>>(post(postData, url));
        }
        #endregion

        #region 下载最新数据
        private void DownloadNews()
        {
            string postData = "&time=";
            string url = "http://1.pngbase.sinaapp.com/news.php";
            news = Newtonsoft.Json.JsonConvert.DeserializeObject<List<NEWS>>(post(postData, url));
        }
        #endregion

        #region POST数据方法
        private string post(string postData, string url)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            //提交公式与紫菜
            HttpWebRequest htr = (HttpWebRequest)WebRequest.Create(url);
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

            //Console.WriteLine(html);
            return html;
        }
        #endregion

        private void BuildStart_Click(object sender, RoutedEventArgs e)
        {
            material = 1;
            Console.WriteLine("{0}/{1}/{2}/{3}/{4}", fuel, steel, ammo, aluminium, material);
            string postData = "&userseed=" + userseed + "&fuel=" + fuel + "&steel=" + steel + "&ammo=" + ammo + "&aluminium=" + aluminium + "&material=" + material + "&dock=" + builddock;
            string url = "http://1.pngbase.sinaapp.com/build.php";
            post(postData, url);
            DownloadUser();
            FormulaAnimation();
        }

        #region 计时器工作内容
        void timer_Tick(object sender, EventArgs e)
        {
            //队列一
            if (tidu.WaitTime[0].Equals(new TimeSpan(0)))
            {
                dock1.Input.Visibility = Visibility.Visible;
                if (tidu.ShipID[0] != 0) dock1.GetShip.Visibility = Visibility.Visible;
            }
            else
            {
                dock1.Input.Visibility = Visibility.Collapsed;
                dock1.GetShip.Visibility = Visibility.Collapsed;
                tidu.WaitTime[0] = tidu.WaitTime[0].Subtract(new TimeSpan(0, 0, 1));
            }
            //队列二
            if (tidu.WaitTime[1].Equals(new TimeSpan(0)))
            {
                dock2.Input.Visibility = Visibility.Visible;
                if (tidu.ShipID[1] != 0) dock2.GetShip.Visibility = Visibility.Visible;
            }
            else
            {
                dock2.Input.Visibility = Visibility.Collapsed;
                dock2.GetShip.Visibility = Visibility.Collapsed;
                tidu.WaitTime[1] = tidu.WaitTime[1].Subtract(new TimeSpan(0, 0, 1));
            }
            //队列三
            if (tidu.WaitTime[2].Equals(new TimeSpan(0)))
            {
                dock3.Input.Visibility = Visibility.Visible;
                if (tidu.ShipID[2] != 0) dock3.GetShip.Visibility = Visibility.Visible;
            }
            else
            {
                dock3.Input.Visibility = Visibility.Collapsed;
                dock3.GetShip.Visibility = Visibility.Collapsed;
                tidu.WaitTime[2] = tidu.WaitTime[2].Subtract(new TimeSpan(0, 0, 1));
            }
            //队列四
            if (tidu.WaitTime[3].Equals(new TimeSpan(0)))
            {
                dock4.Input.Visibility = Visibility.Visible;
                if (tidu.ShipID[3] != 0) dock4.GetShip.Visibility = Visibility.Visible;
            }
            else
            {
                dock4.Input.Visibility = Visibility.Collapsed;
                dock4.GetShip.Visibility = Visibility.Collapsed;
                tidu.WaitTime[3] = tidu.WaitTime[3].Subtract(new TimeSpan(0, 0, 1));
            }
            //文字显示
            dock1.RestTime.Text = string.Format("{0:T}", tidu.WaitTime[0]);
            dock2.RestTime.Text = string.Format("{0:T}", tidu.WaitTime[1]);
            dock3.RestTime.Text = string.Format("{0:T}", tidu.WaitTime[2]);
            dock4.RestTime.Text = string.Format("{0:T}", tidu.WaitTime[3]);
            //1分钟更新排行榜
            if (timecount == 60)
            {
                DownloadBoard();
                DownloadNews();
                UIRefresh();
                timecount = 0;
            }
            if (timecount % 10 == 0)
            {
                NewsText.Text = news[0].NickName + "提督用玄学【" + news[0].Formula + "】公式，成功建造【" + news[0].ShipID + "】一只！" + news[0].LastTime;
            }
            timecount++;
        }
        #endregion

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

        private void BoardMode_Click(object sender, RoutedEventArgs e)
        {
            if(boardmode == 2)
            {
                boardmode = 1;
            }
            else
            {
                boardmode = 2;
            }
            DownloadBoard();
            UIRefresh();
        }
    }
}
