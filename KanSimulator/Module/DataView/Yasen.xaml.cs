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
    /// Yasen.xaml 的交互逻辑
    /// </summary>
    public partial class Yasen : UserControl
    {
        public Keisanki calc;
        public MainSimulator xx;

        public Yasen(MoniKan[] example)
        {
            InitializeComponent();
            calc = new Keisanki(example);
        }

        private void MainPanel_Add()
        {
            MainPanel.Children.Clear();
            for (int i = 1; i <= 6; i++)
            {
                if (calc.IfExist(i) && !calc.IfAirforce(i))
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
                    //普通伤害
                    tb = new TextBlock();
                    capb = calc.CAPB(xx.formation, xx.status, 3);
                    tb.Text = calc.DamageAllY(calc.BasicDamageY(i), capb, 1, 1).ToString() + "-" + calc.DamageAllY(calc.BasicDamageY(i), capb, 1.5, 1).ToString();
                    tb.Width = 60;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //夜战CI类型
                    tb = new TextBlock();
                    tb.Text = calc.GetCITypeY(i);
                    tb.Width = 120;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //伤害
                    tb = new TextBlock();
                    capb = calc.CAPB(xx.formation, xx.status, 3);
                    capa = calc.CAPY(calc.GetCITypeY(i));
                    if(calc.GetCITypeY(i) != "-")
                    {
                        tb.Text = calc.DamageAllY(calc.BasicDamageY(i), capb, capa, 1).ToString() + "-" + calc.DamageAllY(calc.BasicDamageY(i), capb, capa * 1.5, 1).ToString();
                        if (calc.CITimes(calc.GetCITypeY(i)) == 2) tb.Text += " * 2";
                    }else
                    {
                        tb.Text = "-";
                    }
                    tb.Width = 80;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //发动率
                    tb = new TextBlock();
                    tb.Text = calc.GetCITypeY(i) != "-" ? calc.CIRate(i).ToString() : "-";
                    tb.Width = 60;
                    tb.Margin = new Thickness(5);
                    sp.Children.Add(tb);
                    //对潜
                    tb = new TextBlock();
                    tb.Text = calc.IfAntiSub(i) ? "0-1" : "-";
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
            xx = (MainSimulator)MainSimulator.GetWindow(this);
            Formation.SelectedIndex = xx.formation;
            Status.SelectedIndex = xx.status;
            MainPanel_Add();
        }
    }
}
