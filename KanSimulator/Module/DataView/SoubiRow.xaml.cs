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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KanData;

namespace KanSimulator.Module
{
    /// <summary>
    /// SoubiRow.xaml 的交互逻辑
    /// </summary>
    public partial class SoubiRow : UserControl
    {
        public Soubi x = new Soubi();
        public SoubiRow()
        {
            InitializeComponent();
        }

        private void row_Click(object sender, RoutedEventArgs e)
        {
            MainSimulator xx = (MainSimulator)MainSimulator.GetWindow(this);
            if (this.DataContext == null) return;
            xx.rownum = Convert.ToInt32(this.Tag);
            xx.Compare_Init();
            xx.ChangePanel.Visibility = Visibility.Visible;
            DoubleAnimation da = new DoubleAnimation();
            da.From = 700;
            da.To = 480;
            da.Duration = TimeSpan.FromSeconds(0.2);
            xx.SoubiChange.BeginAnimation(Canvas.LeftProperty, da);
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            x = (Soubi)this.DataContext;
            if (x == null) return;
            if (x.Icon != null)
            {
                BitmapImage bi = new BitmapImage(new Uri(x.Icon, UriKind.Relative));
                IconImage.Source = bi;
                Canvas.SetLeft(IconImage, (30 - bi.PixelWidth) / 2);
                Canvas.SetTop(IconImage, (30 - bi.PixelHeight) / 2);
            }
            InfoPanel.Children.Clear();
            Add_Block(x.Range != null ? "射程 " + x.Range.ToString() + "/" : "");
            Add_Block(x.Power != 0 ? "火力+" + x.Power.ToString() + "/" : "");
            Add_Block(x.Torpedo != 0 ? "雷装+" + x.Torpedo.ToString() + "/" : "");
            Add_Block(x.Bomb != 0 ? "爆装+" + x.Bomb.ToString() + "/" : "");
            Add_Block(x.Air != 0 ? "对空+" + x.Air.ToString() + "/" : "");
            Add_Block(x.Antisub != 0 ? "对潜+" + x.Antisub.ToString() + "/" : "");
            Add_Block(x.Search != 0 ? "索敌+" + x.Search.ToString() + "/" : "");
            Add_Block(x.Hitrate != 0 ? "命中+" + x.Hitrate.ToString() + "/" : "");
            Add_Block(x.Dodge != 0 ? "回避+" + x.Dodge.ToString() + "/" : "");
        }

        private void Add_Block(string str)
        {
            TextBlock tb = new TextBlock();
            tb.Text = str;
            if (str != "") InfoPanel.Children.Add(tb);
        }
    }
}
