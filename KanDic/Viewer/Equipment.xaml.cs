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
using KanDic.Resources;
using System.Xml;
using System.Reflection;
using System.Collections.ObjectModel;

namespace KanDic.Viewer
{
    /// <summary>
    /// Equipment.xaml 的交互逻辑
    /// </summary>
    public partial class Equipment : UserControl
    {
        public System.Windows.Window mainwindow;

        public int equipgroup, equipteam, equiptype;

        public static Soubi[] equips = new Soubi[151];

        CollectionViewSource equipview = new CollectionViewSource();
        ObservableCollection<Soubi> customers = new ObservableCollection<Soubi>();

        public Equipment()
        {
            equipteam = 1;
            equipgroup = 1;
            equiptype = 1;
            for (int i = 1; i <= 150; i++)
            {
                if (equips[i] != null) customers.Add(equips[i]);
            }
            InitializeComponent();
            equipview.Filter += equipview_Filter;
            equipview.Source = customers;
            MainList.DataContext = equipview;
        }

        //筛选器
        private void equipview_Filter(object sender, FilterEventArgs e)
        {
            if (equiptype == 1)
            {
                e.Accepted = ((Soubi)e.Item).Type.Contains("主砲");
            }
            if (equiptype == 2)
            {
                e.Accepted = ((Soubi)e.Item).Type.Contains("副砲");
            }
            if (equiptype == 3)
            {
                e.Accepted = ((Soubi)e.Item).Type.Contains("強化弾");
            }
            if (equiptype == 4)
            {
                e.Accepted = ((Soubi)e.Item).Type.Contains("魚雷") || ((Soubi)e.Item).Type.Contains("潜航艇");
            }
            if (equiptype == 5)
            {
                e.Accepted = ((Soubi)e.Item).Type.Contains("艦上") || ((Soubi)e.Item).Type.Contains("水上");
            }
            if (equiptype == 6)
            {
                e.Accepted = ((Soubi)e.Item).Type.Contains("電探");
            }
            if (equiptype == 7)
            {
                e.Accepted = ((Soubi)e.Item).Type.Contains("爆雷") || ((Soubi)e.Item).Type.Contains("ソナ");
            }
            if (equiptype == 8)
            {
                e.Accepted = ((Soubi)e.Item).Type.Contains("機銃") || ((Soubi)e.Item).Type.Contains("高射");
            }
            if (equiptype == 9)
            {
                e.Accepted = ((Soubi)e.Item).Type.Contains("追加") || ((Soubi)e.Item).Type.Contains("機関部強化");
            }
            if (equiptype == 10)
            {
                e.Accepted = ((Soubi)e.Item).Type.Contains("追加") || ((Soubi)e.Item).Type.Contains("機関部強化");
            }
        }

        //图片按下事件
        private void Show_Detail(object sender, RoutedEventArgs e)
        {
            Image xx = (Image)sender;
            int num = (equipgroup - 1) * 50 + (equipteam - 1) * 10 + Convert.ToInt32(xx.Tag);
            if (equips[num] == null) return;
            Open_Window(num);
        }

        #region 打开装备详细窗口
        private void Open_Window(int num)
        {
            bool IsOpened = false;
            foreach (System.Windows.Window element in Application.Current.Windows)
            {
                string type = element.GetType().ToString();
                if (type == "KanDic.StartWindow") mainwindow = element;
                if (type == "KanDic.Window.SoubiDetail")
                {
                    if (element.Title == equips[num].Name)
                    {
                        IsOpened = true;
                        break;
                    }
                }
            }
            if (!IsOpened)
            {
                MahApps.Metro.Controls.MetroWindow win = new Window.SoubiDetail(num);
                win.Owner = mainwindow;
                win.Show();
            }
        }
        #endregion

        private void SoubiTag_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton xx = sender as RadioButton;
            equipgroup = Convert.ToInt32(xx.Tag);
            AlbumPanel.DataContext = new TabNums(equipgroup, equipteam, equips);
        }

        private void NumberTag_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton xx = sender as RadioButton;
            equipteam = Convert.ToInt32(xx.Tag);
            AlbumPanel.DataContext = new TabNums(equipgroup, equipteam, equips);
        }

        //“图鉴模式”点击事件
        private void Album_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Visible;
            ListPanel.Visibility = Visibility.Hidden;
        }
        //“分类一览”点击事件
        private void List_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Hidden;
            ListPanel.Visibility = Visibility.Visible;
        }

        //装备类型点击事件
        private void Type_Click(object sender, RoutedEventArgs e)
        {
            RadioButton xx = sender as RadioButton;
            equiptype = Convert.ToInt32(xx.Tag);
            equipview.View.Refresh();
        }

        //表格行点击事件
        private void MainList_MLBD(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            int num = ((Soubi)dg.SelectedValue).Number;
            Open_Window(num);
        }
    }
}
