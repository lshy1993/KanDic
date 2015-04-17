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
using KanDic;
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

        public static Soubi[] equips;

        CollectionViewSource equipview = new CollectionViewSource();
        ObservableCollection<Soubi> customers = new ObservableCollection<Soubi>();

        public Equipment()
        {
            equips = new Soubi[151];
            Load_Soubi();
            equipteam = 1;
            equipgroup = 1;
            equiptype = 1;
            InitializeComponent();
            equipview.Filter += equipview_Filter;
            equipview.Source = customers;
            MainList.DataContext = equipview;
        }

        void equipview_Filter(object sender, FilterEventArgs e)
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

        #region 读取xml并生成Soubi类
        private void Load_Soubi()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanDic.Resources.Data.Soubi.xml");
            XmlDocument ShipList = new XmlDocument();
            ShipList.Load(sStream);
            XmlElement ShipInfo = ShipList.DocumentElement;
            foreach (XmlNode temp in ShipInfo.ChildNodes)
            {
                Set_Soubi(temp);
            }
        }

        private void Set_Soubi(XmlNode x)
        {
            string name1;
            int num = Convert.ToInt32(x.FirstChild.InnerText);
            equips[num] = new Soubi();
            foreach (XmlNode yy in x.ChildNodes)
            {
                name1 = yy.Name;
                var prop = typeof(Soubi).GetProperty(name1);
                if (prop.PropertyType.Equals(typeof(int)))
                {
                    typeof(Soubi).GetProperty(name1).SetValue(equips[num], Convert.ToInt32(yy.InnerText), null);
                }else
                {
                    typeof(Soubi).GetProperty(name1).SetValue(equips[num], yy.InnerText, null);
                }
            }
            equips[num].Image = "/Cache/equipment/" + string.Format("{0:D3}", equips[num].Number) + ".png";
            equips[num].Icon = "/Cache/icon/soubi/" + equips[num].Icon + ".PNG";
            customers.Add(equips[num]);
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

        private void Album_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Visible;
            ListPanel.Visibility = Visibility.Hidden;
        }

        private void List_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Hidden;
            ListPanel.Visibility = Visibility.Visible;
        }

        private void Type_Click(object sender, RoutedEventArgs e)
        {
            RadioButton xx = sender as RadioButton;
            equiptype = Convert.ToInt32(xx.Tag);
            equipview.View.Refresh();
        }

        private void MainList_MLBD(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            int num = ((Soubi)dg.SelectedValue).Number;
            Open_Window(num);
        }
    }
}
