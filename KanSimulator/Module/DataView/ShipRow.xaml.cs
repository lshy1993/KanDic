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
using KanSimulator.Class;

namespace KanSimulator.Module
{
    /// <summary>
    /// ShipRow.xaml 的交互逻辑
    /// </summary>
    public partial class ShipRow : UserControl
    {
        public ShipRow()
        {
            InitializeComponent();
        }

        private void row_Click(object sender, RoutedEventArgs e)
        {
            MainSimulator xx = (MainSimulator)MainSimulator.GetWindow(this);
            if (this.DataContext == null) return;
            xx.ConfirmPanel.Visibility = Visibility.Visible;
            DoubleAnimation da = new DoubleAnimation();
            da.From = 700;
            da.To = 490;
            da.Duration = TimeSpan.FromSeconds(0.2);
            xx.ShipView.BeginAnimation(Canvas.LeftProperty, da);
            xx.rownum = Convert.ToInt32(this.Tag);
            xx.example[xx.posnum] = new MoniKan(xx.ships[xx.shippage * 10 - 11 + xx.rownum]);
            xx.ShipView.DataContext = xx.example[xx.posnum];
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext == null) level.Text = "";
            else level.Text = "1";
        }
    }
}
