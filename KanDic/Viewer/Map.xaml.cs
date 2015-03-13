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
using KanDic;
using KanDic.Resources;
using System.Xml;
using System.Reflection;

namespace KanDic.Viewer
{
    /// <summary>
    /// Map.xaml 的交互逻辑
    /// </summary>
    public partial class Map : UserControl
    {
        public MapInfo[] MapPoint = new MapInfo[25];
        public int num, tagnum;
        public System.Windows.Window mainwindow;

        #region xaml显示图片用临时class
        public class MapImage
        {
            public string MAP { set; get; }
            public MapImage() { }
            public MapImage(string x) { MAP = x; }
        }
        #endregion

        public int radionum,buttonnum;

        public Map()
        {
            InitializeComponent();
        }

        #region 选择关卡标签

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
        #endregion

        #region 打开地图
        private void bt_Click(object sender, RoutedEventArgs e)
        {
            Button xx = sender as Button;
            buttonnum = Convert.ToInt32(xx.Tag);
            MapBackground.DataContext = new MapImage("/Cache/map/"+radionum+"-"+buttonnum+".png");
            Button_Init(radionum+"-"+buttonnum);
            Map_Show();
        }
        #endregion

        #region 读取xml生成数据，绘制按钮
        private void Button_Init(string x)
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanDic.Resources.Data.Map.xml");
            XmlDocument MapList = new XmlDocument();
            MapList.Load(sStream);
            foreach (XmlNode temp in MapList.DocumentElement.ChildNodes)
            {
                if (temp.FirstChild.InnerText == x)
                {
                    num = 0; 
                    foreach (XmlNode y in temp.ChildNodes)
                    {
                        if(y.Name != "Key")Add_Point(y);
                    }
                    break;
                }
                
            }
            MapButton.Children.Clear();
            ResetPos();
            for (int i = 1; i <= num; i++)
            {
                RadioButton xx = new RadioButton();
                xx.Style = (Style)FindResource(MapPoint[i].Type);
                xx.Tag = i;
                MapButton.Children.Add(xx);
                Canvas.SetLeft(xx, Convert.ToDouble(MapPoint[i].xPos));
                Canvas.SetTop(xx, Convert.ToDouble(MapPoint[i].yPos));
                xx.Checked += new RoutedEventHandler(Point_Detail);
            }
        }
        #endregion

        #region 生成MapInfo类
        private void Add_Point(XmlNode x)
        {
            num++;
            MapPoint[num] = new MapInfo();
            foreach (XmlNode yy in x.ChildNodes)
            {
                string name1 = yy.Name;
                typeof(MapInfo).GetProperty(name1).SetValue(MapPoint[num], yy.InnerText, null);
            }
        }
        #endregion

        #region 打开地图动画
        private void Map_Show()
        {
            MapBox.Visibility = Visibility.Visible;
            DoubleAnimation da = new DoubleAnimation();
            da.From = 480;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(0.2);
            MapBackground.BeginAnimation(Canvas.TopProperty, da);
        }
        #endregion

        #region 打开地图点介绍
        private void Point_Detail(object sender, RoutedEventArgs e)
        {
            RadioButton xx = (RadioButton)sender;
            DoubleAnimation da = new DoubleAnimation();
            tagnum = Convert.ToInt32(xx.Tag);
            bool isbattle = true;
            PointDetail.DataContext = MapPoint[tagnum];
            if (MapPoint[tagnum].nobattle != null) { isbattle = false; }
            ResetPos();
            if (Canvas.GetLeft(xx) < 380)
            {
                da.From = 800;
                da.To = 500;
            }
            else
            {
                da.From = -305;
                da.To = 0;
            }
            da.Duration = TimeSpan.FromSeconds(0.2);
            if (isbattle)
            {
                Battle.BeginAnimation(Canvas.LeftProperty, da);
            }
            else
            {
                NoBattle.BeginAnimation(Canvas.LeftProperty, da);
            }
        }
        #endregion

        #region 介绍Panel复原
        private void ResetPos()
        {
            Battle.BeginAnimation(Canvas.LeftProperty, null);
            NoBattle.BeginAnimation(Canvas.LeftProperty, null);
            Battle.SetValue(Canvas.LeftProperty, (double)-305);
            NoBattle.SetValue(Canvas.LeftProperty, (double)-305);
        }
        #endregion

        private void MapBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MapBox.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (RadioButton xx in MapButton.Children)
            {
                if (Convert.ToInt32(xx.Tag) == tagnum)
                {
                    xx.IsChecked = false;
                    break;
                }
            }
            ResetPos();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock xx = (TextBlock)sender;
            foreach (System.Windows.Window element in Application.Current.Windows)
            {
                string type = element.GetType().ToString();
                if (type == "KanDic.StartWindow") mainwindow = element;
                if (type == "KanDic.Window.Enermy")
                {
                    element.Close();
                    break;
                }
            }
            MahApps.Metro.Controls.MetroWindow win = new Window.Enermy();
            win.Owner = mainwindow;
            win.Show();
        }
    }
}
