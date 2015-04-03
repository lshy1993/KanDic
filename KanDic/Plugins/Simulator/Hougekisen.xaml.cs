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
using KanDic.Resources;

namespace KanDic.Plugins.Simulator
{
    /// <summary>
    /// Hougekisen.xaml 的交互逻辑
    /// </summary>
    public partial class Hougekisen : UserControl
    {
        public Keisanki calc;
        public Calculator xx;

        public Hougekisen(MoniKan[] example)
        {
            InitializeComponent();
            calc = new Keisanki(example);
        }

        private void MainPanel_Add()
        {
            MainPanel.Children.Clear();
            for (int i = 1; i <= 6; i++)
            {
                if (calc.IfExist(i))
                {
                    double capa, capb;
                    StackPanel sp = new StackPanel();
                    sp.Orientation = Orientation.Horizontal;
                    //舰船名
                    TextBlock tb = new TextBlock();
                    tb.Text = calc.GetName(i);
                    tb.Width = 60;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //顺序
                    tb = new TextBlock();
                    if (!calc.IfFire(i))
                    {
                        tb.Text = "-";
                    }
                    else
                    {
                        tb.Text = calc.GetFireNumber(i).ToString() + "/" + i.ToString();
                    }
                    tb.Width = 80;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //普通伤害
                    tb = new TextBlock();
                    capb = calc.CAPB(xx.formation, xx.status, 0);
                    if (!calc.IfFire(i))
                    {
                        tb.Text = "-";
                    }
                    else if (calc.IfAirforce(i))
                    {
                        tb.Text = calc.DamageAll(calc.BasicDamageA(i), capb, 1, 1).ToString() + "-" + calc.DamageAll(calc.BasicDamageA(i), capb, 1.5, 1).ToString();
                    }
                    else
                    {
                        tb.Text = calc.DamageAll(calc.BasicDamageP(i), capb, 1, 1).ToString() + "-" + calc.DamageAll(calc.BasicDamageP(i), capb, 1.5, 1).ToString();
                    }
                    tb.Width = 60;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //弹着射击
                    tb = new TextBlock();
                    tb.Text = calc.IfCutIn(i) ? calc.GetCIType(i) : "-";
                    tb.Width = 80;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //伤害
                    tb = new TextBlock();
                    capb = calc.CAPB(xx.formation, xx.status, 0);
                    capa = calc.CAPA(calc.GetCIType(i));
                    tb.Text = calc.IfCutIn(i) ? calc.DamageAll(calc.BasicDamageP(i), capb, capa, 1).ToString() + "-" + calc.DamageAll(calc.BasicDamageP(i), capb, capa * 1.5, 1).ToString() : "-";
                    tb.Width = 60;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //连击伤害
                    tb = new TextBlock();
                    capb = calc.CAPB(xx.formation, xx.status, 0);
                    capa = calc.CAPA("连击");
                    tb.Text = calc.IfDoubleHit(i) ? calc.DamageAll(calc.BasicDamageP(i), capb, capa, 1).ToString() + "-" + calc.DamageAll(calc.BasicDamageP(i), capb, capa * 1.5, 1).ToString() : "-";
                    tb.Width = 80;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //对潜
                    tb = new TextBlock();
                    capb = calc.CAPB(xx.formation, xx.status, 1);
                    capa = calc.CAPA(calc.GetCIType(i));
                    tb.Text = calc.IfAntiSub(i) ? calc.DamageAllS(calc.BasicDamageS(i), capb, capa, 1).ToString() + "-" + calc.DamageAllS(calc.BasicDamageS(i), capb, capa * 1.5, 1).ToString() : "-";
                    tb.Width = 50;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //命中
                    tb = new TextBlock();
                    tb.Text = "-";
                    tb.Width = 50;
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
            xx = (Calculator)Calculator.GetWindow(this);
            Formation.SelectedIndex = xx.formation;
            Status.SelectedIndex = xx.status;
            MainPanel_Add();
        }
    }
}
