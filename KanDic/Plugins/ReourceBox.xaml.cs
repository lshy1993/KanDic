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

namespace KanDic.Plugins
{
    /// <summary>
    /// ReourceBox.xaml 的交互逻辑
    /// </summary>
    public partial class ReourceBox : UserControl
    {
        public int sum;
        public string boxtype { set; get; }

        public ReourceBox()
        {
            InitializeComponent();
        }

        public void ResourceInit()
        {
            sum = 30;
            TextRefresh();
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            if (sum >= 999) return;
            sum ++;
            TextRefresh();
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            if (sum <= 30) return;
            sum --;
            TextRefresh();
        }

        private void PlusTen_Click(object sender, RoutedEventArgs e)
        {
            if (sum >= 990) return;
            sum += 10;
            TextRefresh();
        }

        private void MinusTen_Click(object sender, RoutedEventArgs e)
        {
            if (sum <= 30) return;
            sum -= 10;
            TextRefresh();
        }

        private void PlusHundred_Click(object sender, RoutedEventArgs e)
        {
            if (sum >= 900) return;
            sum += 100;
            TextRefresh();
        }

        private void MinusHundred_Click(object sender, RoutedEventArgs e)
        {
            if (sum < 130) return;
            sum -= 100;
            TextRefresh();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            sum = 30;
            TextRefresh();
        }

        private void Max_Click(object sender, RoutedEventArgs e)
        {
            sum = 999;
            TextRefresh();
        }

        private void TextRefresh()
        {
            Build father = (Build)Build.GetWindow(this);
            switch (boxtype)
            {
                case "1":
                    father.fuel = sum;
                    break;
                case "2":
                    father.steel = sum;
                    break;
                case "3":
                    father.ammo = sum;
                    break;
                case "4":
                    father.aluminium = sum;
                    break;
            }
            Number.Text = sum.ToString("D3");
            if (sum <= 30)
            {
                plus0.IsEnabled = true;
                minus0.IsEnabled = false;
                plus1.IsEnabled = true;
                minus1.IsEnabled = false;
                plus2.IsEnabled = true;
                minus2.IsEnabled = false;
            }
            else if (sum < 130)
            {
                plus0.IsEnabled = true;
                minus0.IsEnabled = true;
                plus1.IsEnabled = true;
                minus1.IsEnabled = true;
                plus2.IsEnabled = true;
                minus2.IsEnabled = false;
            }
            else if (sum < 900)
            {
                plus0.IsEnabled = true;
                minus0.IsEnabled = true;
                plus1.IsEnabled = true;
                minus1.IsEnabled = true;
                plus2.IsEnabled = true;
                minus2.IsEnabled = true;
            }
            else if (sum < 990)
            {
                plus0.IsEnabled = true;
                minus0.IsEnabled = true;
                plus1.IsEnabled = true;
                minus1.IsEnabled = true;
                plus2.IsEnabled = false;
                minus2.IsEnabled = true;
            }
            else
            {
                if (sum == 999) plus0.IsEnabled = false;
                else plus0.IsEnabled = true;
                minus0.IsEnabled = true;
                plus1.IsEnabled = false;
                minus1.IsEnabled = true;
                plus2.IsEnabled = false;
                minus2.IsEnabled = true;
            }
        }
    }
}
