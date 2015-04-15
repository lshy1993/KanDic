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
using System.Globalization;
using System.Net;
using System.ComponentModel;
using System.Data;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using KanDic.Resources;
using System.Configuration;
using ICSharpCode.SharpZipLib;

namespace KanDic.Plugins
{
    /// <summary>
    /// AutoUpdate.xaml 的交互逻辑
    /// </summary>
    public partial class AutoUpdate : MetroWindow
    {
        private string curpath;
        private WebClient downWebClient = new WebClient();

        public AutoUpdate()
        {
            InitializeComponent();
            curpath = AppDomain.CurrentDomain.BaseDirectory;

            AppVer.Text = "当前程序版本：" + ConfigurationManager.AppSettings["appver"];
            DataVer.Text = "当前数据库版本：" + ConfigurationManager.AppSettings["dataver"];

            xx.Maximum = 60;

            string homeurl = "http://1.pngbase.sinaapp.com/changelog.html";
            ChangeLog.Navigate(new Uri(homeurl));
        }

        private void StartUpdate()
        {
            Status.Text = "正在下载更新……";
            //更新代码
            string url = "http://1.pngbase.sinaapp.com/Update.zip";
            downWebClient.DownloadProgressChanged += downWebClient_DownloadProgressChanged;
            downWebClient.DownloadDataCompleted += downWebClient_DownloadDataCompleted;
            downWebClient.DownloadDataAsync(new Uri(url));
        }

        void downWebClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                string zipFilePath = System.IO.Path.Combine(curpath, "Update.zip");
                byte[] data = e.Result;
                BinaryWriter writer = new BinaryWriter(new FileStream(zipFilePath, FileMode.OpenOrCreate));
                writer.Write(data);
                writer.Flush();
                writer.Close();

                System.Threading.ThreadPool.QueueUserWorkItem((s) =>
                {
                    Action f = () =>
                    {
                        Status.Text = "开始更新程序...";
                    };
                    this.Dispatcher.Invoke(f);
                    string tempDir = System.IO.Path.Combine(curpath, "temp");
                    if (!Directory.Exists(tempDir))
                    {
                        Directory.CreateDirectory(tempDir);
                    }
                    //解压缩包
                    UnZipFile(zipFilePath, tempDir);
                    //移动文件
                    if (Directory.Exists(tempDir))
                    {
                        CopyDirectory(tempDir, curpath);
                    }
                    f = () =>
                    {
                        Status.Text = "更新完成!";
                        try
                        {
                            //清空缓存文件夹
                            System.IO.Directory.Delete(curpath + "temp", true);
                            System.IO.File.Delete(zipFilePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    };
                    this.Dispatcher.Invoke(f);
                    try
                    {
                        f = () =>
                        {
                            //启动软件
                            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
                            Info.FileName = "KanDic.exe";
                            Info.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            System.Diagnostics.Process.Start(Info);
                            Application.Current.Shutdown();
                        };
                        this.Dispatcher.Invoke(f);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
            catch
            {
                Status.Text = "自动获取失败，请手动下载";
            }
        }
        
        void downWebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs ex)
        {
            Status.Text = "正在下载……";
            xx.Maximum = ex.TotalBytesToReceive;
            xx.Value = ex.ProgressPercentage; 
        }

        private static void UnZipFile(string zipFilePath, string targetDir)
        {
            ICSharpCode.SharpZipLib.Zip.FastZipEvents evt = new ICSharpCode.SharpZipLib.Zip.FastZipEvents();
            ICSharpCode.SharpZipLib.Zip.FastZip fz = new ICSharpCode.SharpZipLib.Zip.FastZip(evt);
            fz.ExtractZip(zipFilePath, targetDir, "");
        }

        public void CopyDirectory(string sourceDirName, string destDirName)
        {
            try
            {
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                    File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
                }
                if (destDirName[destDirName.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                    destDirName = destDirName + System.IO.Path.DirectorySeparatorChar;
                string[] files = Directory.GetFiles(sourceDirName);
                foreach (string file in files)
                {
                    File.Copy(file, destDirName + System.IO.Path.GetFileName(file), true);
                    File.SetAttributes(destDirName + System.IO.Path.GetFileName(file), FileAttributes.Normal);
                }
                string[] dirs = Directory.GetDirectories(sourceDirName);
                foreach (string dir in dirs)
                {
                    CopyDirectory(dir, destDirName + System.IO.Path.GetFileName(dir));
                }
            }
            catch
            {
                throw new Exception("复制文件错误");
            }
        }

        private void Yun_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://pan.baidu.com/share/home?uk=1610972107");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            StartWindow mainwin = new StartWindow();
            this.Close();
            mainwin.Show();
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            BarPanel.Visibility = Visibility.Visible;
            ButtonPanel.Visibility = Visibility.Hidden;
            StartUpdate();
        }
    }
}
