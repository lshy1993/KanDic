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
using KanData;

namespace KanDic.Plugins
{
    /// <summary>
    /// RevampBox.xaml 的交互逻辑
    /// </summary>
    public partial class RevampBox : UserControl
    {
        public int starnum = 0;
        public Revamp sou;

        public RevampBox(Revamp x)
        {
            InitializeComponent();
            sou = x;
            this.DataContext = sou;
            IconInit();
            StarInit(starnum);
            TextInit();
        }

        private void IconInit()
        {
            string icon = "/KanDic;component/Cache/icon/soubi/" + sou.Icon;
            Image im = new Image();
            im.Source = new BitmapImage(new Uri(icon, UriKind.Relative));
            im.Loaded += new RoutedEventHandler(setpos);
            IconBox.Children.Add(im);
        }

        private void TextInit()
        {
            int x = Convert.ToInt32(Confirm.IsChecked);
            Mishu.Text = sou.Secretary != null ? "二号位置：" + sou.Secretary : "任意二号舰即可";
            if (starnum == 0)
            {
                Kaifa.Text = sou.Kaifa[0 + x].ToString();
                Gaixiu.Text = sou.Gaixiu[0 + x].ToString();
                if (sou.Extra != null)
                {
                    Extra.Text = sou.Extra[0] != null ? sou.Extra[0] : "无特殊消耗";
                }
                else
                {
                    Extra.Text = "无特殊消耗";
                }
            }
            else if (starnum == 6)
            {
                Kaifa.Text = sou.Kaifa[2 + x].ToString();
                Gaixiu.Text = sou.Gaixiu[2 + x].ToString();
                if (sou.Extra != null)
                {
                    Extra.Text = sou.Extra[1] != null ? sou.Extra[1] : "无特殊消耗";
                }
                else
                {
                    Extra.Text = "无特殊消耗";
                }
            }
            else
            {
                Kaifa.Text = sou.Kaifa[4 + x].ToString();
                Gaixiu.Text = sou.Gaixiu[4 + x].ToString();
                if (sou.Extra != null)
                {
                    Extra.Text = sou.Extra[1] != null ? sou.Extra[1] : "无特殊消耗";
                }
                else
                {
                    Extra.Text = "无特殊消耗";
                }
            }
        }

        private void StarInit(int x)
        {
            StackPanel temp = new StackPanel();
            temp.Orientation = Orientation.Horizontal;
            for (int i = 0; i < x; i++)
            {
                Image star = new Image();
                star.Stretch = Stretch.None;
                star.Source = new BitmapImage(new Uri("/KanDic;component/Cache/icon/info/star.PNG", UriKind.Relative));
                temp.Children.Add(star);
            }
            StarBox.Content = temp;
        }

        private void StarBox_Click(object sender, RoutedEventArgs e)
        {
            if (starnum == 0)
            {
                starnum = 6;
            }
            else if (starnum == 6 && sou.Kaifa.Count > 4)
            {
                starnum = 10;
            }
            else
            {
                starnum = 0;
            }
            StarInit(starnum);
            TextInit();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            TextInit();
        }

        private void setpos(object sender, EventArgs e)
        {
            Image im = (Image)sender;
            im.SetValue(Canvas.LeftProperty, (30 - im.ActualWidth) / 2);
            im.SetValue(Canvas.TopProperty, (30 - im.ActualHeight) / 2);
        }
    }
}
