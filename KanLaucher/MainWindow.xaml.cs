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

namespace KanLaucher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CheckUpdate();
        }

        public void CheckUpdate()
        {
            string url = "http://1.pngbase.sinaapp.com/Update.xml";
            var client = new System.Net.WebClient();
            client.DownloadDataCompleted += (x, y) =>
            {
                UpdateInfo updateInfo = new UpdateInfo();

                MemoryStream stream = new MemoryStream(y.Result);
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(stream);
                XmlElement Infos = xDoc.DocumentElement;
                foreach (XmlNode temp in Infos.ChildNodes)
                {
                    string nodename = temp.Name;
                    typeof(UpdateInfo).GetProperty(nodename).SetValue(updateInfo, temp.InnerText, null);
                }
                stream.Close();
                AppVersion.Text = "程序版本：" + updateInfo.AppVersion;
                AppDescription.Text = updateInfo.AppDescription;
                AppUpdateTime.Text = updateInfo.AppUpdateTime;
                DataVersion.Text = "数据库版本：" + updateInfo.DataVersion;
                DataDescription.Text = updateInfo.DataDescription;
                DataUpdateTime.Text = updateInfo.DataUpdateTime;
                StartUpdate(updateInfo);
            };
            client.DownloadDataAsync(new Uri(url));
        }

        public void StartUpdate(UpdateInfo xxx)
        {
            if (App.ResourceAssembly.GetName(false).Version >= Version.Parse(xxx.AppVersion)) return;
            Console.WriteLine("need update");
            //System.Diagnostics.Process process = new System.Diagnostics.Process();
            //process.StartInfo.FileName = "KanDic.exe";
            //process.Start();
            /*
            //更新程序复制到缓存文件夹
            string appDir = System.IO.Path.Combine(System.Reflection.Assembly.GetEntryAssembly().Location.Substring(0, System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf(System.IO.Path.DirectorySeparatorChar)));
            string updateFileDir = System.IO.Path.Combine(System.IO.Path.Combine(appDir.Substring(0, appDir.LastIndexOf(System.IO.Path.DirectorySeparatorChar))), "Update");
            if (!Directory.Exists(updateFileDir)) Directory.CreateDirectory(updateFileDir);
            updateFileDir = System.IO.Path.Combine(updateFileDir, xxx.MD5.ToString());
            if (!Directory.Exists(updateFileDir)) Directory.CreateDirectory(updateFileDir);
            string exePath = System.IO.Path.Combine(updateFileDir, "AutoUpdater.exe");
            File.Copy(System.IO.Path.Combine(appDir, "AutoUpdater.exe"), exePath, true);
            var info = new System.Diagnostics.ProcessStartInfo(exePath);
            info.UseShellExecute = true;
            info.WorkingDirectory = exePath.Substring(0, exePath.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
            xxx.AppDescription = xxx.AppDescription;
            info.Arguments = "update " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(CallExeName)) + " " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(updateFileDir)) + " " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(appDir)) + " " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(updateInfo.AppName)) + " " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(updateInfo.AppVersion.ToString())) + " " + (string.IsNullOrEmpty(updateInfo.Desc) ? "" : Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(updateInfo.Desc)));
            System.Diagnostics.Process.Start(info);*/
        }

    }
}
