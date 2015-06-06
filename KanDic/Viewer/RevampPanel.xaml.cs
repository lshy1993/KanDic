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
using KanData;
using KanDic.Plugins;

namespace KanDic.Viewer
{
    /// <summary>
    /// Revamp.xaml 的交互逻辑
    /// </summary>
    public partial class RevampPanel : UserControl
    {
        public static List<Revamp> revamplist;
        public int xingqi;

        public RevampPanel()
        {
            InitializeComponent();
            xingqi = (int)DateTime.Now.DayOfWeek;
            ShowWeek(xingqi);
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            xingqi--;
            if (xingqi == -1) xingqi = 6;
            ShowWeek(xingqi);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            xingqi++;
            if (xingqi == 7) xingqi = 0;
            ShowWeek(xingqi);
        }

        private void ShowWeek(int weeknum)
        {
            NowDay.Text = GetWeek(weeknum);
            Prev.Content = GetWeek(weeknum - 1);
            Next.Content = GetWeek(weeknum + 1);
            MainContent.Children.Clear();
            for (int i = 0; i < revamplist.Count; i++)
            {
                if (revamplist[i].Week[weeknum]) MainContent.Children.Add(new RevampBox(revamplist[i]));
            }
        }

        private string GetWeek(int weekday)
        {
            string week;
            switch (weekday)
            {
                case -1:
                    week = "星期六";
                    break;
                case 0:
                    week = "星期日";
                    break;
                case 1:
                    week = "星期一";
                    break;
                case 2:
                    week = "星期二";
                    break;
                case 3:
                    week = "星期三";
                    break;
                case 4:
                    week = "星期四";
                    break;
                case 5:
                    week = "星期五";
                    break;
                case 6:
                    week = "星期六";
                    break;
                case 7:
                    week = "星期日";
                    break;
                default:
                    week = "";
                    break;
            }
            return week;
        }
    }
}
