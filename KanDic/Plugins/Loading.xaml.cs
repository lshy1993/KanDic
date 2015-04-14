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
using System.Windows.Shapes;
using KanDic.Resources;
using MahApps.Metro.Controls;
using System.Net.NetworkInformation;
using System.Net;
using System.Configuration;
using System.IO;
using System.Xml;

namespace KanDic.Plugins
{
    /// <summary>
    /// Loading.xaml 的交互逻辑
    /// </summary>
    public partial class Loading : MetroWindow
    {
        public Loading()
        {
            InitializeComponent();
            string url = "http://1.pngbase.sinaapp.com";
            if (bool.Parse(ConfigurationManager.AppSettings["autoupdate"]) && CheckServeStatus(url))
            {
                CheckUpdate();
            }
            else
            {
                MainWindow();
            }
        }

        #region 检测网络状态
        private static bool CheckServeStatus(string urls)
        {
            if (!LocalConnectionStatus())
            {
                return false;
            }
            /*else if (!MyPing(urls))
            {
                return false;
            }*/
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
                PingReply pr = ping.Send(url, 3000);
                Console.WriteLine("Ping " + pr.Status.ToString());
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
                flag = false;
            }
            return flag;
        }
        #endregion

        private void MainWindow()
        {
            StartWindow mainwin = new StartWindow();
            this.Close();
            mainwin.Show();
        }

        private void CheckUpdate()
        {
            string url = "http://1.pngbase.sinaapp.com/Update.xml";
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
                    if (Version.Parse(ConfigurationManager.AppSettings["appver"]) < Version.Parse(updateInfo.AppVersion) || Version.Parse(ConfigurationManager.AppSettings["appver"]) < Version.Parse(updateInfo.DataVersion))
                    {
                        AutoUpdate au = new AutoUpdate();
                        this.Close();
                        au.Show();
                    }
                    else
                    {
                        MainWindow();
                    }
                }
                catch
                {
                    MainWindow();
                }
            };
            client.DownloadDataAsync(new Uri(url));
        }
    }
}
