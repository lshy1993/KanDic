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
using KanDic.Resources;

namespace KanDic.Plugins.Simulator
{
    /// <summary>
    /// SoubiButton.xaml 的交互逻辑
    /// </summary>
    public partial class SoubiButton : UserControl
    {
        public SoubiButton()
        {
            InitializeComponent();
        }

        private void row_Click(object sender, RoutedEventArgs e)
        {
            Calculator xx = (Calculator)Calculator.GetWindow(this);
            xx.soubinum = Convert.ToInt32(this.Tag);
            xx.soubipage = 1;
            xx.SoubiList_Change(1);
            xx.SoubiPanel.Visibility = Visibility.Visible;
            DoubleAnimation da = new DoubleAnimation();
            da.From = 700;
            da.To = 230;
            da.Duration = TimeSpan.FromSeconds(0.2);
            xx.SoubiList.BeginAnimation(Canvas.LeftProperty, da);
            da.From = 210;
            da.To = 0;
            xx.ShipDetail.BeginAnimation(Canvas.LeftProperty, da);
        }

        public void seticon(string path)
        {
            Border bd = new Border();
            bd.BorderThickness = new Thickness(1);
            bd.BorderBrush = new SolidColorBrush(Colors.Gold);
            byte red = Convert.ToByte("2c", 16);
            byte green = Convert.ToByte("3a", 16);
            byte blue = Convert.ToByte("3b", 16);
            bd.Background = new SolidColorBrush(Color.FromRgb(red, green, blue));
            bd.CornerRadius = new CornerRadius(15);
            bd.Width = 30;
            bd.Height = 30;
            IconBox.Children.Add(bd);
            if (path != null)
            {
                Image im = new Image();
                im.Stretch = Stretch.None;
                im.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                im.Loaded += new RoutedEventHandler(setpos);
                IconBox.Children.Add(im);
            }
        }

        private void setpos(object sender, EventArgs e)
        {
            Image im = (Image)sender;
            im.SetValue(Canvas.LeftProperty, (30 - im.ActualWidth) / 2);
            im.SetValue(Canvas.TopProperty, (30 - im.ActualHeight) / 2);
        }
    }
}
