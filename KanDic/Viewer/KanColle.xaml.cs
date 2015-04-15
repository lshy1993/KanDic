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

        public int shipgroup,shipteam;
        public static Kan[] ships = new Kan[500];
        public bool iffinalon;

        CollectionViewSource shipview = new CollectionViewSource();
        ObservableCollection<NewKan> customers = new ObservableCollection<NewKan>();

        public KanColle()
        {
            Load_Kan();
            shipteam = 1;
            shipgroup = 1;
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

        
        private void Show_Detail(object sender, MouseButtonEventArgs e)
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
                        element.WindowState = WindowState.Normal;
                        IsOpened = true;
                        break;
                    }
                }
            }
            if (!IsOpened)
            {
                Window.KanDetail win = new Window.KanDetail(num);
                win.Show();
            }
        }
        #endregion

        #region 读取xml并生成Kan类
        private void Load_Kan()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanDic.Resources.Data.Ships.xml");
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
            string name1;
            int num = Convert.ToInt32(x.FirstChild.InnerText);
            ships[num] = new Kan();

            foreach (XmlNode yy in x.ChildNodes)
            {
                name1 = yy.Name;
                var prop = typeof(Kan).GetProperty(name1);
                if (prop.PropertyType.Equals(typeof(int)))
                {
                    if (!string.IsNullOrEmpty(yy.InnerText))
                    {
                        prop.SetValue(ships[num], Convert.ToInt32(yy.InnerText), null);
                    }
                }
                else
                {
                    prop.SetValue(ships[num], yy.InnerText, null);
                }
            }
            ships[num].ImageNormal = "/Cache/ships/" + ships[num].FileName + ".swf/Image 5.jpg";
            ships[num].ImageSmall = "/Cache/ships/" + ships[num].FileName + ".swf/Image 1.jpg";
            ships[num].IsFinal = ships[num].LinkNumber == 0;
            if (ships[num].Name == null)
            {
                ships[num] = null;
            }
            else
            {
                customers.Add(new NewKan(ships[num], false));
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

        private void Alubm_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Visible;
            ListPanel.Visibility = Visibility.Hidden;
            ClassPanel.Visibility = Visibility.Hidden;
            ModeAddition.Visibility = Visibility.Collapsed;
            TypeAddition.Visibility = Visibility.Collapsed;
        }

        private void List_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Hidden;
            ListPanel.Visibility = Visibility.Visible;
            ClassPanel.Visibility = Visibility.Hidden;
            ModeAddition.Visibility = Visibility.Visible;
            TypeAddition.Visibility = Visibility.Collapsed;
        }

        private void Class_Click(object sender, RoutedEventArgs e)
        {
            AlbumPanel.Visibility = Visibility.Hidden;
            ListPanel.Visibility = Visibility.Hidden;
            ClassPanel.Visibility = Visibility.Visible;
            ModeAddition.Visibility = Visibility.Collapsed;
            TypeAddition.Visibility = Visibility.Visible;
        }

        private void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            shipview.View.Refresh();
        }

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

        private void Final_Click(object sender, RoutedEventArgs e)
        {
            iffinalon = (bool)((CheckBox)sender).IsChecked;
            shipview.View.Refresh();
        }

        private void MainList_MLBD(object sender, RoutedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            int num = ((NewKan)dg.SelectedValue).Number;
            Open_Window(num);
        }
    }
}
