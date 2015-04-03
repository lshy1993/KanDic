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
    /// OverViewRow.xaml 的交互逻辑
    /// </summary>
    public partial class OverViewRow : UserControl
    {
        public OverViewRow(Keisanki calc,int i)
        {
            InitializeComponent();
            MainPanel.DataContext = calc.example[i];
            nextexp.Text = "next: " + calc.NextExp(i);
            for (int j = 1; j <= 4; j++)
            {
                if (calc.IfEquip(i, j)) addicon(calc.GetIcon(i, j), calc.GetSoubiName(i, j));
            }
        }
        private void addicon(string path,string name)
        {
            Image im = new Image();
            im.Stretch = Stretch.None;
            im.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            im.ToolTip = name;
            im.Loaded += new RoutedEventHandler(setpos);
            Canvas cv = new Canvas();
            cv.Width = 30;
            cv.Height = 30;
            cv.Children.Add(im);
            SoubiIcon.Children.Add(cv);
        }

        private void setpos(object sender, EventArgs e)
        {
            Image im = (Image)sender;
            im.SetValue(Canvas.LeftProperty, (30 - im.ActualWidth) / 2);
            im.SetValue(Canvas.TopProperty, (30 - im.ActualHeight) / 2);
        }
    }
}
