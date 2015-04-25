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
using KanDic.Resources;
using KanData;
using System.Xml;
using System.Reflection;

namespace KanDic.Viewer
{
    /// <summary>
    /// Expedition.xaml 的交互逻辑
    /// </summary>
    public partial class ExpPanel : UserControl
    {
        public static Expedition[] exps = new Expedition[41];
        public int expgroup, selectnum;
        public bool kira;

        public ExpPanel()
        {
            InitializeComponent();
            expgroup = 1;
            Rows_Init(expgroup);
            SetTime(null);
            SetImage(null);
        }

        #region binding每一行
        private void Rows_Init(int x)
        {
            row1.DataContext = null;
            row2.DataContext = null;
            row3.DataContext = null;
            row4.DataContext = null;
            row5.DataContext = null;
            row6.DataContext = null;
            row7.DataContext = null;
            row8.DataContext = null;

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

        //远征行按下事件
        private void row_Click(object sender, RoutedEventArgs e)
        {
            Button bt1 = (Button)sender;
            int y = Convert.ToInt32(bt1.Name.Substring(3));
            selectnum = y + (expgroup - 1) * 8;
            ExpDetail.DataContext = exps[selectnum];
            SetTime("0");
            SetImage("0");
        }

        //不同地图按下事件
        private void MapSelect_Click(object sender, RoutedEventArgs e)
        {
            RadioButton im = (RadioButton)sender;
            expgroup = Convert.ToInt32(im.Tag); 
            Rows_Init(expgroup);
            SetTime(null);
            SetImage(null);
        }

        //大成功按下事件
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            kira = (bool)((CheckBox)sender).IsChecked;
            for (int i = 1; i < 41; i++)
            {
                if (exps[i] != null)
                {
                    if(kira)
                        exps[i].GetMulty();
                    else
                        exps[i].RemoveMulty();
                }
            }
            Rows_Init(expgroup);
        }

        //显示远征时间
        private void SetTime(string x)
        {
            if (x != null)
                ExpTime.Text = string.Format("{0:T}", new TimeSpan(exps[selectnum].Hour, exps[selectnum].Minute, 0));
            else
                ExpTime.Text = string.Format("{0:T}", new TimeSpan());
        }

        //显示得到物品图片
        private void SetImage(string x)
        {
            if (x == null) return;
            if (exps[selectnum].ItemName1 != null)
            {
                string temp = "/Cache/icon/expand/" + exps[selectnum].ItemName1 + ".PNG";
                Image1.Source = new BitmapImage(new Uri(temp, UriKind.Relative));
            }
            if (exps[selectnum].ItemName2 != null)
            {
                string temp = "/Cache/icon/expand/" + exps[selectnum].ItemName2 + ".PNG";
                Image2.Source = new BitmapImage(new Uri(temp, UriKind.Relative));
            }
        }
    }
}
