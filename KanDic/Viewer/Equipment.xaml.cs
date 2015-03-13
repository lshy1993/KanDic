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

        private void Load_Soubi()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanDic.Resources.Data.Equip.xml");
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
                typeof(Soubi).GetProperty(name1).SetValue(equips[num], yy.InnerText, null);
            }
            equips[num].FileName = "/Cache/equipment/" + equips[num].Number + ".png";
        }
    }
}
