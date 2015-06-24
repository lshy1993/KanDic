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
            InfoPanel.Children.Clear();
            if (x == null) return;
            if (x.Range != null)
            {
                TextBlock aaa = new TextBlock();
                aaa.Text = "射程 " + x.Range + "/";
                InfoPanel.Children.Add(aaa);
            }
            if (x.Power != 0)
            {
                TextBlock aaa = new TextBlock();
                aaa.Text = "火力+" + x.Power.ToString() + "/";
                InfoPanel.Children.Add(aaa);
            }
            if (x.Torpedo != 0)
            {
                TextBlock aaa = new TextBlock();
                aaa.Text = "雷装+" + x.Torpedo.ToString() + "/";
                InfoPanel.Children.Add(aaa);
            }
            if (x.Bomb != 0)
            {
                TextBlock aaa = new TextBlock();
                aaa.Text = "爆装+" + x.Bomb.ToString() + "/";
                InfoPanel.Children.Add(aaa);
            }
            if (x.Air != 0)
            {
                TextBlock aaa = new TextBlock();
                aaa.Text = "对空+" + x.Air.ToString() + "/";
                InfoPanel.Children.Add(aaa);
            }
            if (x.Antisub != 0)
            {
                TextBlock aaa = new TextBlock();
                aaa.Text = "对潜+" + x.Antisub.ToString() + "/";
                InfoPanel.Children.Add(aaa);
            }
            if (x.Search != 0)
            {
                TextBlock aaa = new TextBlock();
                aaa.Text = "索敌+" + x.Search.ToString() + "/";
                InfoPanel.Children.Add(aaa);
            }
            if (x.Hitrate != 0)
            {
                TextBlock aaa = new TextBlock();
                aaa.Text = "命中+" + x.Hitrate.ToString() + "/";
                InfoPanel.Children.Add(aaa);
            }
            if (x.Dodge != 0)
            {
                TextBlock aaa = new TextBlock();
                aaa.Text = "回避+" + x.Dodge.ToString() + "/";
                InfoPanel.Children.Add(aaa);
            }
        }
    }
}
