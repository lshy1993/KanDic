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

namespace KanDic.Viewer
{
    /// <summary>
    /// KanColle.xaml 的交互逻辑
    /// </summary>
    public partial class KanColle : UserControl
    {
        public System.Windows.Window mainwindow;

        public string shipgroup,shipteam;

        public static Kan[] ships;

        public KanColle()
        {
            ships = new Kan[250];
            Load_Kan();
            shipteam = "1";
            shipgroup = "1";
            InitializeComponent();
        }

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
            };
            if (!IsOpened)
            {
                MahApps.Metro.Controls.MetroWindow win = new Window.KanDetail(num);
                win.Owner = mainwindow;
                win.Show();
            }
        }

        private void ShipTag_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton xx = sender as RadioButton;
            shipgroup = (string)xx.Tag;
            TabPanel.IsChecked = true;
            Album.DataContext = new TabNum((string)xx.Tag, shipteam, ships);
        }

        private void NumberTag_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton xx = sender as RadioButton;
            shipteam = (string)xx.Tag;
            Album.DataContext = new TabNum(shipgroup, (string)xx.Tag ,ships);
        }

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
            for (int i = 1; i < 250; i++)
            {
                //if (ships[i] == null) ships[i] = ships[1];
            }
        }

        private void Set_Kan(XmlNode x)
        {
            string name1, name2;
            int num = Convert.ToInt32(x.FirstChild.FirstChild.NextSibling.NextSibling.InnerText);
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
                    if (name1 == "BattleInfo") { typeof(BattleInfo).GetProperty(name2).SetValue(b, xx.InnerText, null); }
                    if (name1 == "BuildInfo") { typeof(BuildInfo).GetProperty(name2).SetValue(c, xx.InnerText, null); }
                    if (name1 == "EquipInfo") { typeof(EquipInfo).GetProperty(name2).SetValue(d, xx.InnerText, null); }
                    if (name1 == "MaxInfo") { typeof(MaxInfo).GetProperty(name2).SetValue(e, xx.InnerText, null); }
                    if (name1 == "SourceInfo") { typeof(SourceInfo).GetProperty(name2).SetValue(f, xx.InnerText, null); }
                    if (name1 == "SupplyInfo") { typeof(SupplyInfo).GetProperty(name2).SetValue(g, xx.InnerText, null); }
                    if (name1 == "UpdateInfo") { typeof(UpdateInfo).GetProperty(name2).SetValue(h, xx.InnerText, null); }
                    if (name1 == "ResolveInfo") { typeof(ResolveInfo).GetProperty(name2).SetValue(i, xx.InnerText, null); }
                }
                ships[num] = new Kan(a,b,c,d,e,f,g,h,i);
                //Console.WriteLine(ships[num].BasicInfo.Name);
            }
        }

    }
}
