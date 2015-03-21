﻿using System;
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
    /// Equipment.xaml 的交互逻辑
    /// </summary>
    public partial class Equipment : UserControl
    {
        public System.Windows.Window mainwindow;

        public string equipgroup, equipteam;

        public static Soubi[] equips;

        public Equipment()
        {
            equips = new Soubi[151];
            Load_Soubi();
            equipteam = "1";
            equipgroup = "1";
            InitializeComponent();
        }

        #region 打开装备详细窗口
        private void Show_Detail(object sender, MouseButtonEventArgs e)
        {
            bool IsOpened = false;
            Image xx = (Image)sender;
            int num = (Convert.ToInt32(equipgroup) - 1) * 50 + (Convert.ToInt32(equipteam) - 1) * 10 + Convert.ToInt32(xx.Tag);
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

        private void SoubiTag_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton xx = sender as RadioButton;
            equipgroup = (string)xx.Tag;
            //TabPanel.IsChecked = true;
            Album.DataContext = new TabNums(equipgroup, equipteam, equips);
            //Album.DataContext = new TabNums((string)xx.Tag, equipteam, equips);
        }

        private void NumberTag_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton xx = sender as RadioButton;
            equipteam = (string)xx.Tag;
            Album.DataContext = new TabNums(equipgroup, equipteam, equips);
            //Album.DataContext = new TabNums(equipgroup, (string)xx.Tag, equips);
        }

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
        }
        #endregion

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
                case "电探":
                    return "/Cache/icon/soubi/88.PNG";
                case "三式弹":
                    return "/Cache/icon/soubi/90.PNG";
                case "彻甲弹":
                    return "/Cache/icon/soubi/92.PNG";
                case "应急修理要员":
                    return "/Cache/icon/soubi/94.PNG";
                case "対空機銃":
                    return "/Cache/icon/soubi/96.PNG";
                case "主炮类":
                    return "/Cache/icon/soubi/98.PNG";
                case "水雷":
                    return "/Cache/icon/soubi/100.PNG";
                case "水听":
                    return "/Cache/icon/soubi/102.PNG";
                case "机关部强化类":
                    return "/Cache/icon/soubi/104.PNG";
                case "大发动艇":
                    return "/Cache/icon/soubi/106.PNG";
                case "カ号观测机":
                    return "/Cache/icon/soubi/108.PNG";
                case "三式指挥联络机（对潜）":
                    return "/Cache/icon/soubi/110.PNG";
                case "追加装甲":
                    return "/Cache/icon/soubi/112.PNG";
                case "探照灯":
                    return "/Cache/icon/soubi/114.PNG";
                case "运输桶":
                    return "/Cache/icon/soubi/116.PNG";
                case "舰艇修理设施":
                    return "/Cache/icon/soubi/118.PNG";
                case "照明弹":
                    return "/Cache/icon/soubi/120.PNG";
                case "舰队司令部设施":
                    return "/Cache/icon/soubi/122.PNG";
                case "舰载机熟练员":
                    return "/Cache/icon/soubi/124.PNG";
                case "高射装置类":
                    return "/Cache/icon/soubi/126.PNG";
                case "WG42":
                    return "/Cache/icon/soubi/128.PNG";
                case "熟练见张员":
                    return "/Cache/icon/soubi/130.PNG";
                default:
                    return "";
            }
        }
    }
}
