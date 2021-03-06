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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KanData;
using KanDic.Resources;
using System.Reflection;

namespace KanDic.Viewer
{
    /// <summary>
    /// Map.xaml 的交互逻辑
    /// </summary>
    public partial class MapPanel : UserControl
    {
        public static Map[] maps = new Map[200];
        public int num, tagnum, radionum, buttonnum, windex;
        public double rate1, rate2, rate3;
        public System.Windows.Window mainwindow;

        public MapPanel()
        {
            InitializeComponent();
            rate1 = 1;
            rate2 = 1;
            rate3 = 0;
        }

        #region 选择关卡标签
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton im = (RadioButton)sender;
            switch ((string)im.Tag)
            {
                case "1":
                    bt1.Style = (Style)MapGrid.Resources["bt11"];
                    bt2.Style = (Style)MapGrid.Resources["bt12"];
                    bt3.Style = (Style)MapGrid.Resources["bt13"];
                    bt4.Style = (Style)MapGrid.Resources["bt14"];
                    radionum = 1;
                    break;
                case "2":
                    bt1.Style = (Style)MapGrid.Resources["bt21"];
                    bt2.Style = (Style)MapGrid.Resources["bt22"];
                    bt3.Style = (Style)MapGrid.Resources["bt23"];
                    bt4.Style = (Style)MapGrid.Resources["bt24"];
                    radionum = 2;
                    break;
                case "3":
                    bt1.Style = (Style)MapGrid.Resources["bt31"];
                    bt2.Style = (Style)MapGrid.Resources["bt32"];
                    bt3.Style = (Style)MapGrid.Resources["bt33"];
                    bt4.Style = (Style)MapGrid.Resources["bt34"];
                    radionum = 3;
                    break;
                case "4":
                    bt1.Style = (Style)MapGrid.Resources["bt41"];
                    bt2.Style = (Style)MapGrid.Resources["bt42"];
                    bt3.Style = (Style)MapGrid.Resources["bt43"];
                    bt4.Style = (Style)MapGrid.Resources["bt44"];
                    radionum = 4;
                    break;
                case "5":
                    bt1.Style = (Style)MapGrid.Resources["bt51"];
                    bt2.Style = (Style)MapGrid.Resources["bt52"];
                    bt3.Style = (Style)MapGrid.Resources["bt53"];
                    bt4.Style = (Style)MapGrid.Resources["bt54"];
                    radionum = 5;
                    break;
                case "6":
                    bt1.Style = (Style)MapGrid.Resources["bt61"];
                    bt2.Style = (Style)MapGrid.Resources["bt62"];
                    bt3.Style = (Style)MapGrid.Resources["bt0"];
                    bt4.Style = (Style)MapGrid.Resources["bt0"];
                    radionum = 6;
                    break;
            }
            
        }
        #endregion

        #region 打开地图
        private void bt_Click(object sender, RoutedEventArgs e)
        {
            Button xx = sender as Button;
            buttonnum = Convert.ToInt32(xx.Tag);
            string path = "/Cache/map/" + radionum + "-" + buttonnum + ".png";
            MapBackground.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            RadioButton_Init(radionum+"-"+buttonnum);
            Map_Show();
        }
        #endregion

        #region 绘制按钮
        private void RadioButton_Init(string x)
        {
            MapButton.Children.Clear();
            MapFont.Children.Clear();
            ResetPos();
            for (int i = 1; i < 200; i++)
            {
                if (maps[i] != null && maps[i].Key == x)
                {
                    TextBlock tb = new TextBlock();
                    tb.Text = maps[i].Code;
                    tb.FontSize = 32;
                    tb.Foreground = new SolidColorBrush(Colors.Black);
                    Canvas.SetLeft(tb, Convert.ToDouble(maps[i].xPos + 45));
                    Canvas.SetTop(tb, Convert.ToDouble(maps[i].yPos - 30));
                    MapFont.Children.Add(tb);

                    RadioButton xx = new RadioButton();
                    xx.Style = (Style)FindResource(maps[i].Type);
                    xx.Tag = i;
                    Canvas.SetLeft(xx, Convert.ToDouble(maps[i].xPos));
                    Canvas.SetTop(xx, Convert.ToDouble(maps[i].yPos));
                    xx.Checked += new RoutedEventHandler(Point_Detail);
                    MapButton.Children.Add(xx);
                }
            }
        }
        #endregion

        #region 海图移动动画
        private void Map_Show()
        {
            MapBox.Visibility = Visibility.Visible;
            DoubleAnimation da = new DoubleAnimation();
            da.From = 480;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(0.2);
            MapBoxGrid.BeginAnimation(Canvas.TopProperty, da);
        }
        #endregion

        #region 打开地图点介绍
        private void Point_Detail(object sender, RoutedEventArgs e)
        {
            RadioButton xx = (RadioButton)sender;
            tagnum = Convert.ToInt32(xx.Tag);

            bool isbattle = true;

            PointDetail.DataContext = maps[tagnum];

            if (maps[tagnum].Pattern != null) Add_Enermy();
            Add_Drop();

            if (maps[tagnum].NoBattle != null) { isbattle = false; }
            ResetPos();

            DoubleAnimation da = new DoubleAnimation();
            if (Canvas.GetLeft(xx) < 380)
            {
                da.From = 800;
                da.To = 494;
            }
            else
            {
                da.From = -305;
                da.To = 0;
            }
            da.Duration = TimeSpan.FromSeconds(0.2);
            if (isbattle)
            {
                Battle.BeginAnimation(Canvas.LeftProperty, da);
            }
            else
            {
                Nobattle.BeginAnimation(Canvas.LeftProperty, da);
            }
        }
        #endregion

        #region 添加敌人配置按钮
        private void Add_Enermy()
        {
            EnermyButton.Children.Clear();
            for (int i = 0; i < maps[tagnum].Pattern.Count; i++)
            {
                Button_Init(i);
            }
        }

        private void Button_Init(int i)
        {
            if (maps[tagnum].Pattern[i].ShipList == null) return;
            Button tempbut = new Button();
            tempbut.Click += Enermy_Detail;
            tempbut.Tag = i.ToString();
            tempbut.Content = maps[tagnum].Pattern[i].ShipList;
            EnermyButton.Children.Add(tempbut);
        }
        #endregion

        #region 添加掉落文字
        private void Add_Drop()
        {
            DropDetail.Children.Clear();
            TextBlock_Init(maps[tagnum].DropDD);
            TextBlock_Init(maps[tagnum].DropCL);
            TextBlock_Init(maps[tagnum].DropCA);
            TextBlock_Init(maps[tagnum].DropAV);
            TextBlock_Init(maps[tagnum].DropCV);
            TextBlock_Init(maps[tagnum].DropSS);
            TextBlock_Init(maps[tagnum].DropBB);
        }

        private void TextBlock_Init(string x)
        {
            if (x == "") return;
            TextBlock temptext = new TextBlock();
            temptext.TextWrapping = TextWrapping.Wrap;
            temptext.Text = x;
            temptext.Margin = new Thickness(5);
            DropDetail.Children.Add(temptext);
        }
        #endregion

        #region 介绍Panel复原
        private void ResetPos()
        {
            Battle.BeginAnimation(Canvas.LeftProperty, null);
            Nobattle.BeginAnimation(Canvas.LeftProperty, null);
            Battle.SetValue(Canvas.LeftProperty, (double)-305);
            Nobattle.SetValue(Canvas.LeftProperty, (double)-305);
        }
        #endregion

        #region 打开深海详细窗口
        private void Enermy_Detail(object sender, RoutedEventArgs e)
        {
            Button xx = (Button)sender;
            foreach (System.Windows.Window element in Application.Current.Windows)
            {
                string type = element.GetType().ToString();
                if (type == "KanDic.StartWindow") mainwindow = element;
                if (type == "KanDic.Window.EnermySet")
                {
                    element.Close();
                    break;
                }
            }
            int temp = Convert.ToInt32(xx.Tag);
            MahApps.Metro.Controls.MetroWindow win = new Window.EnermySet(maps[tagnum].Pattern[temp]);
            win.Owner = mainwindow;
            win.Show();
        }
        #endregion

        private void MapBox_MLBD(object sender, MouseButtonEventArgs e)
        {
            MapBox.Visibility = Visibility.Hidden;
        }

        private void Close_PointDetail(object sender, MouseButtonEventArgs e)
        {
            foreach (RadioButton xx in MapButton.Children)
            {
                if (Convert.ToInt32(xx.Tag) == tagnum)
                {
                    xx.IsChecked = false;
                    break;
                }
            }
            ResetPos();
        }

        #region 经验计算部分
        private void WinMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            windex = ((ComboBox)sender).SelectedIndex;
            switch (windex)
            {
                case 0:
                    rate3 = 1.2;
                    break;
                case 1:
                    rate3 = 1;
                    break;
                case 2:
                    rate3 = 1;
                    break;
                case 3:
                    rate3 = 0.8;
                    break;
                case 4:
                    rate3 = 0.7;
                    break;
                case 5:
                    rate3 = 0.5;
                    break;
                default:
                    rate3 = 0;
                    break;
            }
            ExpShow();
        }

        private void MVP_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            rate1 = (bool)cb.IsChecked ? 2 : 1;
            ExpShow();
        }

        private void Ultimate_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            rate2 = (bool)cb.IsChecked ? 1.5 : 1;
            ExpShow();
        }

        private void ExpShow()
        {
            if (rate3 != 0)
            {
                int temp = Convert.ToInt32(maps[tagnum].Exp * rate1 * rate2 * rate3);
                ExpCal.Text = temp.ToString();
            }
            else
            {
                ExpCal.Text = "请选择胜利状态";
            }

        }
        #endregion
    }
}