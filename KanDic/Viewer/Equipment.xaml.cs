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

namespace KanDic.Viewer
{
    /// <summary>
    /// Equipment.xaml 的交互逻辑
    /// </summary>
    public partial class Equipment : UserControl
    {
        public class EquipImage
        {
            public string File1 { get; set; }
            public string File2 { get; set; }
            public string File3 { get; set; }
            public string File4 { get; set; }
            public string File5 { get; set; }
            public string File6 { get; set; }
            public string File7 { get; set; }
            public string File8 { get; set; }
            public string File9 { get; set; }
            public string File10 { get; set; }
            public string Number1 { get; set; }
            public string Number2 { get; set; }
            public string Number3 { get; set; }
            public string Number4 { get; set; }
            public string Number5 { get; set; }
            public string Number6 { get; set; }
            public string Number7 { get; set; }
            public string Number8 { get; set; }
            public string Number9 { get; set; }
            public string Number10 { get; set; }
        }

        public class TabNumber
        {
            public string Tab1 { get; set; }
            public string Tab2 { get; set; }
            public string Tab3 { get; set; }
            public string Tab4 { get; set; }
            public string Tab5 { get; set; }
        }

        public System.Windows.Window mainwindow;

        public Equipment()
        {
            InitializeComponent();
            EquipImage page = new EquipImage()
            {
                File1 = "/Cache/equipment/001.png",
                File2 = "/Cache/equipment/002.png",
                File3 = "/Cache/equipment/003.png",
                File4 = "/Cache/equipment/004.png",
                File5 = "/Cache/equipment/005.png",
                File6 = "/Cache/equipment/006.png",
                File7 = "/Cache/equipment/007.png",
                File8 = "/Cache/equipment/008.png",
                File9 = "/Cache/equipment/009.png",
                File10 = "/Cache/equipment/010.png",
                Number1 = "No.001",
                Number2 = "No.002",
                Number3 = "No.003",
                Number4 = "No.004",
                Number5 = "No.005",
                Number6 = "No.006",
                Number7 = "No.007",
                Number8 = "No.008",
                Number9 = "No.009",
                Number10 = "No.010"
            };
            TabNumber tab = new TabNumber()
            {
                Tab1 = "001-010",
                Tab2 = "011-020",
                Tab3 = "021-030",
                Tab4 = "031-040",
                Tab5 = "041-050"
            };
            MainContent.DataContext = page;
            TabCtrl.DataContext = tab;
        }

        private void Show_Detail(object sender, MouseButtonEventArgs e)
        {
            bool IsOpened = false;

            /*foreach (System.Windows.Window element in Application.Current.Windows)
            {
                string type = element.GetType().ToString();
                if (type == "KanDic.StartWindow") mainwindow = element;
                if (type == "KanDic.Window.KanDetail")
                {
                    element.WindowState = WindowState.Normal;
                    element.Activate();
                    IsOpened = true;
                    break;
                }
            };
            if (!IsOpened)
            {
                Window.KanDetail win = new Window.KanDetail();
                win.Owner = mainwindow;
                win.ShowActivated = true;
                win.Activate();
                win.Show();
            }*/
        }
    }
}
