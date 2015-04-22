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
using KanDic;
using KanDic.Resources;
using System.Xml;
using System.Reflection;
using System.Collections.ObjectModel;

namespace KanDic.Viewer
{
    /// <summary>
    /// KanColle.xaml 的交互逻辑
    /// </summary>
    public partial class KanColle : UserControl
    {
        public System.Windows.Window mainwindow;
        public static Kan[] ships = new Kan[500];
        public int shipgroup, shipteam, classtag;
        public bool iffinalon;
        public string classname;

        CollectionViewSource shipview = new CollectionViewSource();
        ObservableCollection<NewKan> customers = new ObservableCollection<NewKan>();

        public KanColle()
        {
            shipteam = 1;
            shipgroup = 1;
            InitializeComponent();
            for (int i = 1; i < 500; i++)
            {
                if (ships[i] != null && ships[i].Name != null) customers.Add(new NewKan(ships[i], false));
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            shipview.Filter += shipview_Filter;
            shipview.Source = customers;
            MainList.DataContext = shipview;
        }

        //筛选器
        void shipview_Filter(object sender, FilterEventArgs e)
        {
            if (iffinalon)
            {
                e.Accepted = ((NewKan)e.Item).IsFinal;
                if (!string.IsNullOrEmpty(Keyword.Text))
                {
                    e.Accepted = ((NewKan)e.Item).IsFinal && ((NewKan)e.Item).Name.Contains(Keyword.Text) || ((NewKan)e.Item).Hiragana.Contains(Keyword.Text) || ((NewKan)e.Item).RomaName.Contains(Keyword.Text);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Keyword.Text))
                {
                    e.Accepted = ((NewKan)e.Item).Name.Contains(Keyword.Text) || ((NewKan)e.Item).Hiragana.Contains(Keyword.Text) || ((NewKan)e.Item).RomaName.Contains(Keyword.Text);
                }
            }
        }

        //图鉴点击事件
        private void Show_Detail(object sender, RoutedEventArgs e)
        {
            Image xx = (Image)sender;
            int num = (shipgroup - 1) * 50 + (shipteam - 1) * 10 + Convert.ToInt32(xx.Tag);
            if (ships[num] == null) return;
            Open_Window(num);
        }
        
        #region 打开舰娘详细窗口
        private void Open_Window(int num)
        {
            bool IsOpened = false;
            foreach (System.Windows.Window element in Application.Current.Windows)
            {
                string type = element.GetType().ToString();
                if (type == "KanDic.StartWindow") mainwindow = element;
                if (type == "KanDic.Window.KanDetail")
                {
                    if (element.Title == ships[num].Name)
                    {
                        IsOpened = true;
                        break;
                    }
                }
            }
            if (!IsOpened)
            {
                Window.KanDetail win = new Window.KanDetail(num);
                win.Owner = mainwindow;
                win.Show();
            }
        }
        #endregion

        private void ShipTag_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton xx = sender as RadioButton;
            shipgroup = Convert.ToInt32(xx.Tag);
            AlbumPanel.DataContext = new TabNum(shipgroup, shipteam, ships);
        }

        private void NumberTag_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton xx = sender as RadioButton;
            shipteam = Convert.ToInt32(xx.Tag);
            AlbumPanel.DataContext = new TabNum(shipgroup, shipteam, ships);
        }
        //“图鉴模式”点击事件
        private void Alubm_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Visible;
            ListPanel.Visibility = Visibility.Hidden;
            ClassPanel.Visibility = Visibility.Hidden;
            ModeAddition.Visibility = Visibility.Collapsed;
            TypeAddition.Visibility = Visibility.Collapsed;
        }
        //“快速搜索”点击事件
        private void List_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Hidden;
            ListPanel.Visibility = Visibility.Visible;
            ClassPanel.Visibility = Visibility.Hidden;
            ModeAddition.Visibility = Visibility.Visible;
            TypeAddition.Visibility = Visibility.Collapsed;
            Final1.IsChecked = iffinalon;
        }
        //“类型一览”点击事件
        private void Class_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Hidden;
            ListPanel.Visibility = Visibility.Hidden;
            ClassPanel.Visibility = Visibility.Visible;
            ModeAddition.Visibility = Visibility.Collapsed;
            TypeAddition.Visibility = Visibility.Visible;
            Final2.IsChecked = iffinalon;
        }
        //关键字变更
        private void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            shipview.View.Refresh();
        }
        //“显示最大值”点击事件
        private void MaxData_Click(object sender, RoutedEventArgs e)
        {
            bool ifmax = (bool)((CheckBox)sender).IsChecked;
            for (int i = 0; i < customers.Count; i++)
            {
                int x = Convert.ToInt32(customers[i].Number);
                customers[i] = new NewKan(ships[x], ifmax);
            }
            shipview.View.Refresh();
        }
        //“显示最终版”点击事件
        private void Final_Click(object sender, RoutedEventArgs e)
        {
            iffinalon = (bool)((CheckBox)sender).IsChecked;
            shipview.View.Refresh();
            Class_Refresh();
        }
        //列表行点击事件
        private void MainList_MLBD(object sender, RoutedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            int num = ((NewKan)dg.SelectedValue).Number;
            Open_Window(num);
        }
        //舰种标签点击事件
        private void ClassChange_Click(object sender, RoutedEventArgs e)
        {
            classtag = Convert.ToInt32(((RadioButton)sender).Tag);
            Class_Refresh();
        }
        //刷新小图标
        private void Class_Refresh()
        {
            MainClass.Children.Clear();
            for (int i = 1; i < 500; i++)
            {
                if (ships[i] != null && IfIsClass(classtag, ships[i]))
                {
                    Image im = new Image();
                    im.Stretch = Stretch.None;
                    im.Source = new BitmapImage(new Uri(ships[i].ImageSmall, UriKind.Relative));
                    im.MouseLeftButtonDown += im_MLBD;
                    im.ToolTip = ships[i].Name;
                    im.Cursor = Cursors.Hand;
                    im.Margin = new Thickness(5);
                    im.Tag = ships[i].Number;
                    MainClass.Children.Add(im);
                }
            }
        }
        //类型判断
        private bool IfIsClass(int i,Kan ship)
        {
            bool flag = false;
            switch (i)
            {
                case 1:
                    flag = ship.Type == "戦艦" || ship.Type == "航空戦艦";
                    break;
                case 2:
                    flag = ship.Type == "正規空母" || ship.Type == "装甲空母";
                    break;
                case 3:
                    flag = ship.Type == "軽空母" || ship.Type == "水上機母艦";
                    break;
                case 4:
                    flag = ship.Type == "重巡洋艦" || ship.Type == "航空巡洋艦";
                    break;
                case 5:
                    flag = ship.Type == "軽巡洋艦";
                    break;
                case 6:
                    flag = ship.Type == "重雷装巡洋艦";
                    break;
                case 7:
                    flag = ship.Type == "駆逐艦";
                    break;
                case 8:
                    flag = ship.Type == "潜水母艦" || ship.Type == "潜水艦" || ship.Type == "潜水空母";
                    break;
                case 9:
                    flag = ship.Type == "工作艦" || ship.Type == "揚陸艦" || ship.Type == "練習巡洋艦";
                    break;
            }
            return iffinalon ? flag && ship.IsFinal : flag;
        }
        //小图标点击事件
        private void im_MLBD(object sender, MouseButtonEventArgs e)
        {
            int num = Convert.ToInt32(((Image)sender).Tag);
            Open_Window(num);
        }
    }
}
