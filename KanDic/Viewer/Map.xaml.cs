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

namespace KanDic.Viewer
{
    /// <summary>
    /// Map.xaml 的交互逻辑
    /// </summary>
    public partial class Map : UserControl
    {
        public class MapDetail
        {
            public string MAP { set; get; }
            public MapDetail() { }
            public MapDetail(string x) { MAP = x; }
        }

        public int radionum,buttonnum;

        public Map()
        {
            InitializeComponent();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton im = (RadioButton)sender;
            switch ((string)im.Tag)
            {
                case "1":
                    bt1.Style = (Style)MapGrid.Resources["bt11"];
                    bt2.Style = (Style)MapGrid.Resources["bt12"];
                    bt3.Style = (Style)MapGrid.Resources["bt13"];
                    bt4.Style = (Style)MapGrid.Resources["bt14"];
                    radionum = 1;
                    break;
                case "2":
                    bt1.Style = (Style)MapGrid.Resources["bt21"];
                    bt2.Style = (Style)MapGrid.Resources["bt22"];
                    bt3.Style = (Style)MapGrid.Resources["bt23"];
                    bt4.Style = (Style)MapGrid.Resources["bt24"];
                    radionum = 2;
                    break;
                case "3":
                    bt1.Style = (Style)MapGrid.Resources["bt31"];
                    bt2.Style = (Style)MapGrid.Resources["bt32"];
                    bt3.Style = (Style)MapGrid.Resources["bt33"];
                    bt4.Style = (Style)MapGrid.Resources["bt34"];
                    radionum = 3;
                    break;
                case "4":
                    bt1.Style = (Style)MapGrid.Resources["bt41"];
                    bt2.Style = (Style)MapGrid.Resources["bt42"];
                    bt3.Style = (Style)MapGrid.Resources["bt43"];
                    bt4.Style = (Style)MapGrid.Resources["bt44"];
                    radionum = 4;
                    break;
                case "5":
                    bt1.Style = (Style)MapGrid.Resources["bt51"];
                    bt2.Style = (Style)MapGrid.Resources["bt52"];
                    bt3.Style = (Style)MapGrid.Resources["bt53"];
                    bt4.Style = (Style)MapGrid.Resources["bt54"];
                    radionum = 5;
                    break;
                case "6":
                    bt1.Style = (Style)MapGrid.Resources["bt61"];
                    bt2.Style = (Style)MapGrid.Resources["bt62"];
                    bt3.Style = (Style)MapGrid.Resources["bt0"];
                    bt4.Style = (Style)MapGrid.Resources["bt0"];
                    radionum = 6;
                    break;
            }
            
        }

        private void bt_Click(object sender, RoutedEventArgs e)
        {
            Button xx = sender as Button;
            buttonnum = Convert.ToInt32(xx.Tag);
            MapBackground.DataContext = new MapDetail("/Cache/map/"+radionum+"-"+buttonnum+".png");
            Button_Init(radionum+"-"+buttonnum);
            Map_Show();
        }

        private void Button_Init(string x)
        {
            switch (x)
            {
                default :
                    TextBlock xx = new TextBlock();
                    xx.Text = "数据制作中……";
                    xx.FontSize = 40;
                    Canvas.SetTop(xx, 180);
                    Canvas.SetLeft(xx, 280);
                    MapButton.Children.Add(xx);
                    TextBlock yy = new TextBlock();
                    yy.Text = "Now Loading......";
                    yy.FontSize = 40;
                    Canvas.SetTop(yy, 220);
                    Canvas.SetLeft(yy, 280);
                    MapButton.Children.Add(yy);
                    break;
            }
        }

        private void Map_Show()
        {
            MapBox.Visibility = Visibility.Visible;
            DoubleAnimation da = new DoubleAnimation();
            da.From = 480;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(0.3);
            MapBackground.BeginAnimation(Canvas.TopProperty, da);
        }

        private void MapBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MapBox.Visibility = Visibility.Hidden;
        }

    }
}
