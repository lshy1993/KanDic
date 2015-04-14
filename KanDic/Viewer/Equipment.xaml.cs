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

        #region 打开装备详细窗口
        private void Show_Detail(object sender, MouseButtonEventArgs e)
        {
            bool IsOpened = false;
            Image xx = (Image)sender;
            int num = (equipgroup - 1) * 50 + (equipteam - 1) * 10 + Convert.ToInt32(xx.Tag);
            if (equips[num] == null) return;
            foreach (System.Windows.Window element in Application.Current.Windows)
            {
                string type = element.GetType().ToString();
                if (type == "KanDic.StartWindow") mainwindow = element;
                if (type == "KanDic.Window.SoubiDetail")
                {
                    if (element.Title == equips[num].Name)
                    {
                        element.WindowState = WindowState.Normal;
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
                if (yy.InnerText == "True" || yy.InnerText == "False")
                {
                    typeof(Soubi).GetProperty(name1).SetValue(equips[num], Convert.ToBoolean(yy.InnerText), null);
                }
                else
                {
                    int i ;
                    if (Int32.TryParse(yy.InnerText, out i))
                    {
                        typeof(Soubi).GetProperty(name1).SetValue(equips[num], i, null);
                    }
                    else
                    {
                        typeof(Soubi).GetProperty(name1).SetValue(equips[num], yy.InnerText, null);
                    }
                }
            }
            equips[num].Image = "/Cache/equipment/" + string.Format("{0:D3}", equips[num].Number) + ".png";
            equips[num].Icon = geticon(equips[num].Type);
            customers.Add(equips[num]);
        }
        #endregion

        #region 判断装备（准备废除）
        private string geticon(string x)
        {
            switch (x)
            {
                case "小口径主砲":
                    return "/Cache/icon/soubi/68.PNG";
                case "中口径主砲":
                    return "/Cache/icon/soubi/70.PNG";
                case "大口径主砲":
                    return "/Cache/icon/soubi/72.PNG";
                case "副砲":
                    return "/Cache/icon/soubi/74.PNG";
                case "魚雷":
                    return "/Cache/icon/soubi/76.PNG";
                case "特殊潜航艇":
                    return "/Cache/icon/soubi/76.PNG";
                case "潜水艦魚雷":
                    return "/Cache/icon/soubi/76.PNG";
                case "艦上戦闘機":
                    return "/Cache/icon/soubi/78.PNG";
                case "艦上爆撃機":
                    return "/Cache/icon/soubi/80.PNG";
                case "艦上攻撃機":
                    return "/Cache/icon/soubi/82.PNG";
                case "艦上偵察機":
                    return "/Cache/icon/soubi/84.PNG";
                case "水上爆撃機":
                    return "/Cache/icon/soubi/86.PNG";
                case "水上偵察機":
                    return "/Cache/icon/soubi/86.PNG";
                case "小型電探":
                    return "/Cache/icon/soubi/88.PNG";
                case "大型電探":
                    return "/Cache/icon/soubi/88.PNG";
                case "対空強化弾":
                    return "/Cache/icon/soubi/90.PNG";
                case "対艦強化弾":
                    return "/Cache/icon/soubi/92.PNG";
                case "応急修理要員":
                    return "/Cache/icon/soubi/94.PNG";
                case "対空機銃":
                    return "/Cache/icon/soubi/96.PNG";
                case "主炮类":
                    return "/Cache/icon/soubi/98.PNG";
                case "爆雷":
                    return "/Cache/icon/soubi/100.PNG";
                case "ソナー":
                    return "/Cache/icon/soubi/102.PNG";
                case "大型ソナー":
                    return "/Cache/icon/soubi/102.PNG";
                case "機関部強化":
                    return "/Cache/icon/soubi/104.PNG";
                case "上陸用舟艇":
                    return "/Cache/icon/soubi/106.PNG";
                case "オートジャイロ":
                    return "/Cache/icon/soubi/108.PNG";
                case "対潜哨戒機":
                    return "/Cache/icon/soubi/110.PNG";
                case "追加装甲(中型)":
                    return "/Cache/icon/soubi/112.PNG";
                case "追加装甲(大型)":
                    return "/Cache/icon/soubi/112.PNG";
                case "探照灯":
                    return "/Cache/icon/soubi/114.PNG";
                case "簡易輸送部材":
                    return "/Cache/icon/soubi/116.PNG";
                case "艦艇修理施設":
                    return "/Cache/icon/soubi/118.PNG";
                case "照明弾":
                    return "/Cache/icon/soubi/120.PNG";
                case "艦隊司令部施設":
                    return "/Cache/icon/soubi/122.PNG";
                case "航空要員":
                    return "/Cache/icon/soubi/124.PNG";
                case "高射装置":
                    return "/Cache/icon/soubi/126.PNG";
                case "対地装備":
                    return "/Cache/icon/soubi/128.PNG";
                case "水上艦要員":
                    return "/Cache/icon/soubi/130.PNG";
                default:
                    return "";
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
    }
}
