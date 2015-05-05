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

namespace KanDic.Plugins
{
    /// <summary>
    /// BuildBox.xaml 的交互逻辑
    /// </summary>
    public partial class BuildBox : UserControl
    {
        public int docknum { get; set; }

        public BuildBox()
        {
            InitializeComponent();
        }

        private void Input_Click(object sender, RoutedEventArgs e)
        {
            Build father = (Build)Build.GetWindow(this);
            father.builddock = docknum;
            father.re1.ResourceInit();
            father.re2.ResourceInit();
            father.re3.ResourceInit();
            father.re4.ResourceInit();
            DoubleAnimation da = new DoubleAnimation();
            da.From = 700;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(0.2);
            father.FormulaBox.BeginAnimation(Canvas.LeftProperty, da);
        }

        private void Get_Click(object sender, RoutedEventArgs e)
        {
            Build father = (Build)Build.GetWindow(this);
            father.builddock = docknum;
            father.GetShip();
            GetShip.Visibility = Visibility.Collapsed;
        }

        private void Quick_Click(object sender, RoutedEventArgs e)
        {
            Build father = (Build)Build.GetWindow(this);
            father.builddock = docknum;
            father.QuickGet();
            Quick.IsEnabled = false;
        }
    }
}
