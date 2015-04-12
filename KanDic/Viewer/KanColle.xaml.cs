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
    /// KanColle.xaml 的交互逻辑
    /// </summary>
    public partial class KanColle : UserControl
    {
        public System.Windows.Window mainwindow;

        public string shipgroup,shipteam;
        public static Kan[] ships = new Kan[500];

        CollectionViewSource shipview = new CollectionViewSource();
        ObservableCollection<NewKan> customers = new ObservableCollection<NewKan>();

        public KanColle()
        {
            Load_Kan();
            shipteam = "1";
            shipgroup = "1";
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            shipview.Filter += shipview_Filter;
            shipview.Source = customers;
            MainList.DataContext = shipview;
        }

        void shipview_Filter(object sender, FilterEventArgs e)
        {
            if (!string.IsNullOrEmpty(Keyword.Text))
            {
                e.Accepted = ((NewKan)e.Item).Name.Contains(Keyword.Text) || ((NewKan)e.Item).Hiragana.Contains(Keyword.Text) || ((NewKan)e.Item).RomaName.Contains(Keyword.Text);
            }
        }

        #region 打开舰娘详细窗口
        private void Show_Detail(object sender, MouseButtonEventArgs e)
        {
            bool IsOpened = false;
            Image xx = (Image)sender;
            int num = (Convert.ToInt32(shipgroup) - 1) * 50 + (Convert.ToInt32(shipteam) - 1) * 10 + Convert.ToInt32(xx.Tag);
            if (ships[num] == null) return;
            foreach (System.Windows.Window element in Application.Current.Windows)
            {
                string type = element.GetType().ToString();
                if (type == "KanDic.StartWindow") mainwindow = element;
                if (type == "KanDic.Window.KanDetail")
                {
                    if (element.Title == ships[num].BasicInfo.Name)
                    {
                        element.WindowState = WindowState.Normal;
                        IsOpened = true;
                        break;
                    }
                }
            }
            if (!IsOpened)
            {
                MahApps.Metro.Controls.MetroWindow win = new Window.KanDetail(num);
                win.Owner = mainwindow;
                win.Show();
            }
        }
        #endregion

        #region 读取xml并生成Kan类
        private void Load_Kan()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanDic.Resources.Data.Ship.xml");
            XmlDocument ShipList = new XmlDocument();
            ShipList.Load(sStream);
            XmlElement ShipInfo = ShipList.DocumentElement;
            foreach (XmlNode temp in ShipInfo.ChildNodes)
            {
                Set_Kan(temp);
            }
        }

        private void Set_Kan(XmlNode x)
        {
            string name1, name2;
            BasicInfo a = new BasicInfo();
            BattleInfo b = new BattleInfo();
            BuildInfo c = new BuildInfo();
            EquipInfo d = new EquipInfo();
            MaxInfo e = new MaxInfo();
            SourceInfo f = new SourceInfo();
            SupplyInfo g = new SupplyInfo();
            UpdateInfo h = new UpdateInfo();
            ResolveInfo i = new ResolveInfo(); 
            foreach (XmlNode yy in x.ChildNodes)
            {
                name1 = yy.Name;
                //Console.WriteLine(name1);
                foreach (XmlNode xx in yy.ChildNodes)
                {
                    name2 = xx.Name;
                    //if (xx.InnerText == null) xx.InnerText = " ";
                    if (name1 == "BasicInfo") { typeof(BasicInfo).GetProperty(name2).SetValue(a, xx.InnerText, null); }
                    if (name1 == "BattleInfo")
                    {
                        if (name2 == "Speed" || name2 == "Range") typeof(BattleInfo).GetProperty(name2).SetValue(b, xx.InnerText, null);
                        else typeof(BattleInfo).GetProperty(name2).SetValue(b, Convert.ToInt32(xx.InnerText), null);
                    }
                    if (name1 == "BuildInfo") { typeof(BuildInfo).GetProperty(name2).SetValue(c, xx.InnerText, null); }
                    if (name1 == "EquipInfo") { typeof(EquipInfo).GetProperty(name2).SetValue(d, xx.InnerText, null); }
                    if (name1 == "MaxInfo")
                    {
                        if (name2 == "Speed" || name2 == "Range") typeof(MaxInfo).GetProperty(name2).SetValue(e, xx.InnerText, null);
                        else typeof(MaxInfo).GetProperty(name2).SetValue(e, Convert.ToInt32(xx.InnerText), null);
                    }
                    if (name1 == "SourceInfo") { typeof(SourceInfo).GetProperty(name2).SetValue(f, Convert.ToInt32(xx.InnerText), null); }
                    if (name1 == "SupplyInfo") { typeof(SupplyInfo).GetProperty(name2).SetValue(g, Convert.ToInt32(xx.InnerText), null); }
                    if (name1 == "UpdateInfo") { typeof(UpdateInfo).GetProperty(name2).SetValue(h, xx.InnerText, null); }
                    if (name1 == "ResolveInfo") { typeof(ResolveInfo).GetProperty(name2).SetValue(i,  Convert.ToInt32(xx.InnerText), null); }
                }
                //Console.WriteLine(ships[num].BasicInfo.Name);
            }
            int num = Convert.ToInt32(a.Number);
            ships[num] = new Kan(a,b,c,d,e,f,g,h,i);
            ships[num].BasicInfo.FileName = "/Cache/ships/" + ships[num].BasicInfo.FileName + ".swf/Images/Image 5.jpg";
            if (ships[num].BasicInfo.Name != null) customers.Add(new NewKan(ships[num]));
        }
        #endregion

        private void ShipTag_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton xx = sender as RadioButton;
            shipgroup = (string)xx.Tag;
            AlbumPanel.DataContext = new TabNum((string)xx.Tag, shipteam, ships);
        }

        private void NumberTag_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton xx = sender as RadioButton;
            shipteam = (string)xx.Tag;
            AlbumPanel.DataContext = new TabNum(shipgroup, (string)xx.Tag, ships);
        }

        private void Alubm_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Visible;
            ListPanel.Visibility = Visibility.Hidden;
            ClassPanel.Visibility = Visibility.Hidden;
        }

        private void List_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Hidden;
            ListPanel.Visibility = Visibility.Visible;
            ClassPanel.Visibility = Visibility.Hidden;
        }

        private void Class_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Hidden;
            ListPanel.Visibility = Visibility.Hidden;
            ClassPanel.Visibility = Visibility.Visible;
        }

        private void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            shipview.View.Refresh();
        }
    }
}
