﻿using System;
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
using KanDic.Resources;
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
    public partial class Loading : System.Windows.Window
    {
        public System.Windows.Threading.DispatcherTimer myTimer = new System.Windows.Threading.DispatcherTimer();

        public Loading()
        {
            InitializeComponent();
            AnimationInit();
            AnimationInit2();
            string url = "pngbase.sinapp.com";
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
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                flag = false;
            }
            return flag;
        }
        #endregion

        #region 检测是否有更新
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
                        DataLoading();
                    }
                }
                catch
                {
                    DataLoading();
                }
            };
            client.DownloadDataAsync(new Uri(url));
        }
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

        private void DataLoading()
        {
            this.Visibility = Visibility.Visible;
            myTimer.Interval = new TimeSpan(0, 0, 0 ,3);
            myTimer.Tick += myTimer_Tick;
            myTimer.Start();
        }

        void myTimer_Tick(object sender, EventArgs e)
        {
            myTimer.Stop();
            StartWindow mainwin = new StartWindow();
            this.Close();
            mainwin.Show();
        }
    }
}