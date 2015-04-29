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
using KanData;
using KanDic.Resources;
using System.Net.NetworkInformation;
using System.Net;
using System.Configuration;
using System.IO;

namespace KanDic
{
    /// <summary>
    /// Loading.xaml 的交互逻辑
    /// </summary>
    public partial class Loading : System.Windows.Window
    {
        public System.Windows.Threading.DispatcherTimer myTimer = new System.Windows.Threading.DispatcherTimer();

        public Loading()
        {
            InitializeComponent();
            AnimationInit();
            AnimationInit2();
            string url = "pngbase.sinapp.com";
            //仅收集操作系统与IP信息，不会收集用户其他信息
            SendInfo();
            //检查更新
            if (bool.Parse(ConfigurationManager.AppSettings["autoupdate"]) && CheckServeStatus(url))
            {
                CheckUpdate();
            }
            else
            {
                DataLoading();
            }
        }

        #region 检测网络状态
        private static bool CheckServeStatus(string urls)
        {
            if (!LocalConnectionStatus())
            {
                return false;
            }
            else if (!MyPing(urls))
            {
                return false;
            }
            return true;
        }

        //本地连接检测
        [System.Runtime.InteropServices.DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);
        private static bool LocalConnectionStatus()
        {
            System.Int32 dwFlag = new Int32();
            if (InternetGetConnectedState(ref dwFlag, 0))
            {
                return true;
            }
            return false;
        }

        //ping测试
        private static bool MyPing(string url)
        {
            bool flag = true;
            Ping ping = new Ping();
            try
            {
                PingReply pr = ping.Send(url);
                //Console.WriteLine("Ping " + pr.Status.ToString());
                if (pr.Status == IPStatus.TimedOut)
                {
                    flag = false;
                }
                if (pr.Status == IPStatus.Success)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch
            {
                MessageBox.Show("未能连接到更新服务器！");
                flag = false;
            }
            return flag;
        }
        #endregion

        #region 检测是否有更新
        private void CheckUpdate()
        {
            HttpWebRequest htr = (HttpWebRequest)WebRequest.Create("http://1.pngbase.sinaapp.com/update.json");
            htr.Method = "GET";
            HttpWebResponse hwr = (HttpWebResponse)htr.GetResponse();
            Stream responseStream = hwr.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream);
            var html = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            htr.Abort();
            hwr.Close();
            var updateInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateInfo>(html);

            if (Version.Parse(ConfigurationManager.AppSettings["appver"]) < Version.Parse(updateInfo.AppVersion))
            {
                Confirm cr = new Confirm(updateInfo.AppVersion);
                cr.Owner = this;
                if (!(bool)cr.ShowDialog()) DataLoading();
            }
            else
            {
                DataLoading();
            }
        }
        /*废除
            var client = new WebClient();
            client.DownloadDataCompleted += (x, y) =>
            {
                try
                {
                    Update updateInfo = new Update();
                    MemoryStream stream = new MemoryStream(y.Result);
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(stream);
                    XmlElement Infos = xDoc.DocumentElement;
                    foreach (XmlNode temp in Infos.ChildNodes)
                    {
                        string nodename = temp.Name;
                        typeof(Update).GetProperty(nodename).SetValue(updateInfo, temp.InnerText, null);
                    }
                    stream.Close();
                    if (Version.Parse(ConfigurationManager.AppSettings["appver"]) < Version.Parse(updateInfo.AppVersion))
                    {
                        Confirm cr = new Confirm(updateInfo.AppVersion);
                        cr.Owner = this;
                        if (!(bool)cr.ShowDialog()) DataLoading();
                    }
                    else
                    {
                        DataLoading();
                    }
                }
                catch
                {
                    MessageBox.Show("未能下载更新文件！");
                    //DataLoading();
                }
            };
            client.DownloadDataAsync(new Uri(url));
        }*/
        #endregion

        #region 动画绑定
        private void AnimationInit()
        {
            DoubleAnimationUsingKeyFrames dak = new DoubleAnimationUsingKeyFrames();
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(515, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(507, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(515, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.6))));
            dak.RepeatBehavior = RepeatBehavior.Forever;
            ship.BeginAnimation(Canvas.TopProperty, dak);
        }

        private void AnimationInit2()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 22.5;
            da.To = 76.5;
            da.RepeatBehavior = RepeatBehavior.Forever;
            da.Duration = TimeSpan.FromSeconds(2.0);
            ov1.BeginAnimation(EllipseGeometry.RadiusXProperty, da);
            DoubleAnimation dd = new DoubleAnimation();
            dd.From = 6;
            dd.To = 20.4;
            dd.RepeatBehavior = RepeatBehavior.Forever;
            dd.Duration = TimeSpan.FromSeconds(2.0);
            ov1.BeginAnimation(EllipseGeometry.RadiusYProperty, dd);
            DoubleAnimationUsingKeyFrames dak = new DoubleAnimationUsingKeyFrames();
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.5))));
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2))));
            dak.RepeatBehavior = RepeatBehavior.Forever;
            oval1.BeginAnimation(OpacityProperty, dak);
        }
        #endregion

        #region 读取数据
        private void DataLoading()
        {
            DataInit di = new DataInit();
            for (int i = 0; i < di.kanlist.Count; i++)
            {
                di.kanlist[i].ImageNormal = "/Cache/ships/" + di.kanlist[i].FileName + ".swf/Image 5.jpg";
                di.kanlist[i].ImageSmall = "/Cache/ships/" + di.kanlist[i].FileName + ".swf/Image 1.jpg";
                KanDic.Viewer.KanColle.ships[di.kanlist[i].Number] = di.kanlist[i];
            }
            for (int i = 0; i < di.soubilist.Count; i++)
            {
                KanDic.Viewer.Equipment.equips[di.soubilist[i].Number] = di.soubilist[i];
            }
            for (int i = 0; i < di.explist.Count; i++)
            {
                di.explist[i].Hard = "/KanDic;component/Cache/icon/expand/" + di.explist[i].Hard + ".PNG";
                KanDic.Viewer.ExpPanel.exps[i + 1] = di.explist[i];
            }
            for (int i = 0; i < di.maplist.Count; i++)
            {
                KanDic.Viewer.MapPanel.maps[i + 1] = di.maplist[i];
            }
            for (int i = 0; i < di.enemylist.Count; i++)
            {
                KanDic.Window.EnermySet.enemys[i + 1] = di.enemylist[i];
            }
            for (int i = 0; i < di.questlist.Count; i++)
            {
                KanDic.Viewer.QuestPanel.list.Add(di.questlist[i]);
            }
            myTimer.Interval = new TimeSpan(0, 0, 0 ,1);
            myTimer.Tick += myTimer_Tick;
            myTimer.Start();
        }
        #endregion

        #region 发送操作系统信息
        private void SendInfo()
        {
            string strName = Environment.UserDomainName;
            string strOS = Environment.OSVersion.ToString();
            string strVer = Environment.Version.ToString();

            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "&name=" + strName + "&os=" + strOS + "&ver=" + strVer;
            byte[] data = encoding.GetBytes(postData);
            try
            {
                HttpWebRequest htr = (HttpWebRequest)WebRequest.Create("http://1.pngbase.sinaapp.com/statistics.php");
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
            }
            catch { }
        }
        #endregion

        void myTimer_Tick(object sender, EventArgs e)
        {
            myTimer.Stop();
            StartWindow mainwin = new StartWindow();
            this.Close();
            mainwin.Show();
        }
    }
}
