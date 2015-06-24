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
using KanSimulator.Class;

namespace KanSimulator.Module
{
    /// <summary>
    /// Raigekisen.xaml 的交互逻辑
    /// </summary>
    public partial class Raigekisen : UserControl
    {
        public Keisanki calc;
        public MainSimulator xx;

        public Raigekisen(MoniKan[] example)
        {
            InitializeComponent();
            calc = new Keisanki(example);
        }

        private void MainPanel_Add()
        {
            MainPanel.Children.Clear();
            for (int i = 1; i <= 6; i++)
            {
                if (calc.IfExist(i) && calc.IfTorpedo(i))
                {
                    StackPanel sp = new StackPanel();
                    sp.Orientation = Orientation.Horizontal;
                    //舰船名
                    TextBlock tb = new TextBlock();
                    tb.Text = calc.GetName(i);
                    tb.Width = 80;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //开幕雷击
                    tb = new TextBlock();
                    tb.Text = calc.IfOpenTorpedo(i) ? "Yes" : "No";
                    tb.Width = 60;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //基本攻击力
                    tb = new TextBlock();
                    tb.Text = calc.BasicDamageT(i).ToString();
                    tb.Width = 80;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //总伤害
                    tb = new TextBlock();
                    double capb = calc.CAPB(xx.formation,xx.status,2);
                    tb.Text = calc.DamageAll(calc.BasicDamageT(i), capb, 1, 1).ToString() + "-" + calc.DamageAll(calc.BasicDamageT(i), capb, 1.5, 1).ToString();
                    tb.Width = 100;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //命中
                    tb = new TextBlock();
                    tb.Text = calc.HitRate(i,xx.commanderlv.LV).ToString("F2");
                    tb.Width = 80;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //回避
                    tb = new TextBlock();
                    tb.Text = calc.MissRate(i).ToString("F2");
                    tb.Width = 80;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //放入容器
                    MainPanel.Children.Add(sp);
                }
            }
        }

        private void Formation_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (xx != null) xx.formation = ((ComboBox)sender).SelectedIndex;
            MainPanel_Add();
        }

        private void Status_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (xx != null) xx.status = ((ComboBox)sender).SelectedIndex;
            MainPanel_Add();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            xx = (MainSimulator)MainSimulator.GetWindow(this);
            Formation.SelectedIndex = xx.formation;
            Status.SelectedIndex = xx.status;
            MainPanel_Add();
        }
    }
}
