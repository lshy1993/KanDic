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
    /// Expedition.xaml 的交互逻辑
    /// </summary>
    public partial class Expedition : UserControl
    {
        public ExpInfo[] exps = new ExpInfo[41];
        public int expgroup;

        public Expedition()
        {
            Load_Exp();
            InitializeComponent();
            expgroup = 1;
            Rows_Init(expgroup);
        }

        #region binding每一行
        private void Rows_Init(int x)
        {
            row1.DataContext = exps[x * 8 - 7];
            row2.DataContext = exps[x * 8 - 6];
            row3.DataContext = exps[x * 8 - 5];
            row4.DataContext = exps[x * 8 - 4];
            row5.DataContext = exps[x * 8 - 3];
            row6.DataContext = exps[x * 8 - 2];
            row7.DataContext = exps[x * 8 - 1];
            row8.DataContext = exps[x * 8];
        }
        #endregion

        #region 读取xml并生成ExpInfo类
        private void Load_Exp()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanDic.Resources.Data.Expedition.xml");
            XmlDocument ExpList = new XmlDocument();
            ExpList.Load(sStream);
            XmlElement ExpInfo = ExpList.DocumentElement;
            foreach (XmlNode temp in ExpInfo.ChildNodes)
            {
                Set_Exp(temp);
            }
        }

        private void Set_Exp(XmlNode x)
        {
            string name1;
            int num = Convert.ToInt32(x.FirstChild.FirstChild.InnerText);
            exps[num] = new ExpInfo();
            foreach (XmlNode yy in x.ChildNodes)
            {
                name1 = yy.Name;
                typeof(ExpInfo).GetProperty(name1).SetValue(exps[num], yy.InnerText, null);
            }
            exps[num].Hard = "/KanDic;component/Cache/icon/expand/" + exps[num].Hard + ".PNG";
            exps[num].ItemName1 = "/KanDic;component/Cache/icon/expand/" + exps[num].ItemName1 + ".PNG";
            exps[num].ItemName2 = "/KanDic;component/Cache/icon/expand/" + exps[num].ItemName2 + ".PNG";
        }
        #endregion

        private void row_Click(object sender, RoutedEventArgs e)
        {
            Button bt1 = (Button)sender;
            int y = Convert.ToInt32(bt1.Name.Substring(3));
            y = y + (expgroup - 1) * 8;
            ExpDetail.DataContext = exps[y];
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton im = (RadioButton)sender;
            expgroup = Convert.ToInt32(im.Tag); 
            Rows_Init(expgroup);
        }
    }
}
