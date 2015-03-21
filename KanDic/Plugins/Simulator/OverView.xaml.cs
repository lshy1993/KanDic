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
using KanDic.Resources;
using KanDic.Plugins;

namespace KanDic.Viewer
{
    /// <summary>
    /// OverView.xaml 的交互逻辑
    /// </summary>
    public partial class OverView : UserControl
    {
        public OverView()
        {
            InitializeComponent();
        }

        private void Change_Ship(object sender, RoutedEventArgs e)
        {
            Calculator xx = (Calculator)Calculator.GetWindow(this);
            xx.posnum = Convert.ToInt32(this.Tag);
            xx.SelectPanel.Visibility = Visibility.Visible;
            xx.DetailPanel.Visibility = Visibility.Hidden;
            DoubleAnimation da = new DoubleAnimation();
            da.From = 700;
            da.To = 245;
            da.Duration = TimeSpan.FromSeconds(0.2);
            xx.ShipList.BeginAnimation(Canvas.LeftProperty, da);
        }

        private void Show_Detail(object sender, RoutedEventArgs e)
        {
            Calculator xx = (Calculator)Calculator.GetWindow(this);
            xx.posnum = Convert.ToInt32(this.Tag);
            xx.DetailPanel.Visibility = Visibility.Visible;
            xx.SelectPanel.Visibility = Visibility.Hidden;
            DoubleAnimation da = new DoubleAnimation();
            da.From = 700;
            da.To = 210;
            da.Duration = TimeSpan.FromSeconds(0.2);
            xx.ShipDetail.BeginAnimation(Canvas.LeftProperty, da);
        }
    }
}