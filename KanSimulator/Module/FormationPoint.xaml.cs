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

namespace KanSimulator.Module
{
    /// <summary>
    /// FormationPoint.xaml 的交互逻辑
    /// </summary>
    public partial class FormationPoint : UserControl
    {
        public FormationPoint(int formation, int num, bool isenemy)
        {
            InitializeComponent();
            string url = isenemy ? "/Image/Battle/Point_Red.png" : "/Image/Battle/Point_Green.png";
            P1.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            P2.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            P3.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            P4.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            P5.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            P6.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            switch (formation * 10 + num)
            {
                case 1://单纵,1船
                    SetPosition(P1, -7, -7);
                    break;
                case 2://单纵,2船
                    SetPosition(P1, 5, -7);
                    SetPosition(P2, -19, -7);
                    break;
                case 3://单纵,3船
                    SetPosition(P1, 17, -7);
                    SetPosition(P2, -7, -7);
                    SetPosition(P3, -31, -7);
                    break;
                case 4://单纵,4船
                    SetPosition(P1, 28, -7);
                    SetPosition(P2, 5, -7);
                    SetPosition(P3, -19, -7);
                    SetPosition(P4, -42, -7);
                    break;
                case 5://单纵,5船
                    SetPosition(P1, 39, -7);
                    SetPosition(P2, 17, -7);
                    SetPosition(P3, -7, -7);
                    SetPosition(P4, -31, -7);
                    SetPosition(P5, -53, -7);
                    break;
                case 6://单纵,6船
                    SetPosition(P1, 51, -7);
                    SetPosition(P2, 28, -7);
                    SetPosition(P3, 5, -7);
                    SetPosition(P4, -19, -7);
                    SetPosition(P5, -42, -7);
                    SetPosition(P6, -65, -7);
                    break;
                case 14://复纵,4船
                    SetPosition(P1, -19, -18);
                    SetPosition(P2, 5, -18);
                    SetPosition(P3, -19, 4);
                    SetPosition(P4, 5, 4);
                    break;
                case 15://复纵,5船
                    SetPosition(P1, -31, -18);
                    SetPosition(P2, -7, -18);
                    SetPosition(P3, 17, -18);
                    SetPosition(P4, -31, 4);
                    SetPosition(P5, -7, 4);
                    break;
                case 16://复纵,6船
                    SetPosition(P1, -31, -18);
                    SetPosition(P2, -7, -18);
                    SetPosition(P3, 17, -18);
                    SetPosition(P4, -31, 4);
                    SetPosition(P5, -7, 4);
                    SetPosition(P6, 17, 4);
                    break;
                case 25://轮形,5船
                    SetPosition(P1, -31, -7);
                    SetPosition(P2, -7, -7);
                    SetPosition(P3, 17, -7);
                    SetPosition(P4, -7, -29);
                    SetPosition(P5, -7, 15);
                    break;
                case 26://轮形,6船
                    SetPosition(P1, -42, -7);
                    SetPosition(P2, -19, -7);
                    SetPosition(P3, 5, -7);
                    SetPosition(P4, 28, -7);
                    SetPosition(P5, -7, -40);
                    SetPosition(P6, -7, 26);
                    break;
                case 34://梯形,4船
                    SetPosition(P1, -29.5, 15.5);
                    SetPosition(P2, -14.5, 0.5);
                    SetPosition(P3, 0.5, -14.5);
                    SetPosition(P4, 15.5, -29.5);
                    break;
                case 35://梯形,5船
                    SetPosition(P1, -37, 23);
                    SetPosition(P2, -22, 8);
                    SetPosition(P3, -7, -7);
                    SetPosition(P4, 8, -22);
                    SetPosition(P5, 23, -37);
                    break;
                case 36://梯形,6船
                    SetPosition(P1, -44.5, 30.5);
                    SetPosition(P2, -29.5, 15.5);
                    SetPosition(P3, -14.5, 0.5);
                    SetPosition(P4, 0.5, -14.5);
                    SetPosition(P5, 15.5, -29.5);
                    SetPosition(P6, 30.5, -44.5);
                    break;
                case 44://单横,4船
                    SetPosition(P1, -7, -40);
                    SetPosition(P2, -7, -18);
                    SetPosition(P3, -7, 4);
                    SetPosition(P4, -7, 26);
                    break;
                case 45://单横,5船
                    SetPosition(P1, -7, -51);
                    SetPosition(P2, -7, -29);
                    SetPosition(P3, -7, -7);
                    SetPosition(P4, -7, 15);
                    SetPosition(P5, -7, 37);
                    break;
                case 46://单横,6船
                    SetPosition(P1, -7, -62);
                    SetPosition(P2, -7, -40);
                    SetPosition(P3, -7, -18);
                    SetPosition(P4, -7, 4);
                    SetPosition(P5, -7, 26);
                    SetPosition(P6, -7, 48);
                    break;
            }
        }

        private void SetPosition(UIElement target, double x,double y)
        {
            Canvas.SetTop(target, 54 + x * 0.6);
            Canvas.SetLeft(target, 59 + y * 0.6);
            target.Visibility = Visibility.Visible;
        }
    }
}
