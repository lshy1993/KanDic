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
using System.IO;
using System.Xml;
using System.Reflection;
using MahApps.Metro.Controls;
using KanDic.Resources;
using System.Configuration;

namespace KanDic.Plugins
{
    /// <summary>
    /// AutoUpdate.xaml 的交互逻辑
    /// </summary>
    public partial class AutoUpdate : MetroWindow
    {

        public System.Windows.Threading.DispatcherTimer WaitTimer = new System.Windows.Threading.DispatcherTimer();
        public Update updateInfo;
        int sum = 0;
        bool ifwait = false;

        public AutoUpdate()
        {
            InitializeComponent();

            WaitTimer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            WaitTimer.Tick += new EventHandler(ShowSecond);

            AppVer.Text = "当前程序版本：" + ConfigurationManager.AppSettings["appver"];
            DataVer.Text = "当前数据库版本：" + ConfigurationManager.AppSettings["dataver"];
            if (bool.Parse(ConfigurationManager.AppSettings["autoupdate"]))
            {
                ifwait = true;
                WaitTimer.Start();
                Status.Text = "检查更新...";
                ChangeLog.Source = new Uri("http://www.baidu.com");
                CheckUpdate();
            }
            else
            {
                ifwait = false;
                WaitTimer.Start();
            }
        }

        private void CheckUpdate()
        {
            string url = "http://1.pngbase.sinaapp.com/Update.xml";
            var client = new System.Net.WebClient();
            client.DownloadDataCompleted += (x, y) =>
            {
                try
                {
                    updateInfo = new Update();
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
                    StartUpdate();
                }
                catch
                {

                }
            };
            client.DownloadDataAsync(new Uri(url));
        }

        private void StartUpdate()
        {
            if (Version.Parse(ConfigurationManager.AppSettings["appver"]) < Version.Parse(updateInfo.AppVersion) || Version.Parse(ConfigurationManager.AppSettings["appver"]) < Version.Parse(updateInfo.DataVersion))
            {
                Status.Text = "数据库更新中";
                //更新代码
            }
            else
            {
                Status.Text = "Now Loading";
                ifwait = false;
                WaitTimer.Start();
            }

        }

        private void ShowSecond(object sender, EventArgs e)
        {
            if (ifwait)
            {
                sum++;
                if (sum == 200) Status.Text = "检查更新失败！程序将直接启动";
                if (sum >= 300) ifwait = false;
            }
            else
            {
                xx.Value++;
                if (xx.Value == xx.Maximum)
                {
                    WaitTimer.Stop();
                    StartWindow mainwin = new StartWindow();
                    this.Close();
                    mainwin.Show();
                }
            }
        }

    }
}
