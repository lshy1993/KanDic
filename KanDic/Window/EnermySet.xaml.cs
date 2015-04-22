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
using System.Windows.Shapes;
using KanData;
using KanDic.Resources;
using MahApps.Metro.Controls;
using System.Xml;
using System.Reflection;

namespace KanDic.Window
{
    /// <summary>
    /// Enermy.xaml 的交互逻辑
    /// </summary>
    public partial class EnermySet
    {
        public Enemy shinkai;
        public static KanData.Enemy[] enemys = new KanData.Enemy[200];
        public XmlDocument EnermyList = new XmlDocument();
        public string[] namelist;

        public EnermySet(string x)
        {
            InitializeComponent();

            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanDic.Resources.Data.Enermy.xml");
            EnermyList.Load(sStream);

            namelist = x.Split(' ');
            for (int i = 0; i < namelist.Length; i++)
            {
                shinkai = new Enemy();
                Load_Enermy(namelist[i]);
                DataPanel.Children.Add(new KanDic.Viewer.EnermyPanel(shinkai));
                //Draw_Icon(shinkai,i);
            }
        }

        private void Draw_Icon(Enemy x,int nownum)
        {
            Image tempim = new Image();
            tempim.Stretch = Stretch.None;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"/Cache/test.png", UriKind.Relative);
            bi.EndInit();
            tempim.Source = bi;
            Rectangle temprec = new Rectangle();
            temprec.Height = 1;
            temprec.Margin = new Thickness(0, 5, 0, 5);
            temprec.Fill = new SolidColorBrush(Colors.Gray);
            ImagePanel.Children.Add(tempim);
            if (nownum < namelist.Length - 1) ImagePanel.Children.Add(temprec);
        }

        private void Load_Enermy(string x)
        {
            foreach (XmlNode temp in EnermyList.DocumentElement.ChildNodes)
            {
                if (temp.FirstChild.InnerText == x)
                {
                    Add_Enermy(temp);
                    break;
                }
            }
        }

        private void Add_Enermy(XmlNode x)
        {
            foreach (XmlNode yy in x.ChildNodes)
            {
                string name1 = yy.Name;
                typeof(Enemy).GetProperty(name1).SetValue(shinkai, yy.InnerText, null);
            }
        }
    }
}
