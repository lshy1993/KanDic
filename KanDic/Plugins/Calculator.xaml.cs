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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using KanDic.Viewer;
using KanDic.Resources;

namespace KanDic.Plugins
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Calculator : MetroWindow
    {
        public Kan[] ships;
        public MoniKan[] example = new MoniKan[6];
        public Soubi[] equips;
        public int page,tagnum,posnum;

        public Calculator()
        {
            InitializeComponent();
            ships = KanDic.Viewer.KanColle.ships;
            equips = KanDic.Viewer.Equipment.equips;
            page = 1;
            Dock1.NumImage.Source = new BitmapImage(new Uri("/Cache/Calculate/1.PNG", UriKind.Relative));
            Dock2.NumImage.Source = new BitmapImage(new Uri("/Cache/Calculate/2.PNG", UriKind.Relative));
            Dock3.NumImage.Source = new BitmapImage(new Uri("/Cache/Calculate/3.PNG", UriKind.Relative));
            Dock4.NumImage.Source = new BitmapImage(new Uri("/Cache/Calculate/4.PNG", UriKind.Relative));
            Dock5.NumImage.Source = new BitmapImage(new Uri("/Cache/Calculate/5.PNG", UriKind.Relative));
            Dock6.NumImage.Source = new BitmapImage(new Uri("/Cache/Calculate/6.PNG", UriKind.Relative));
            ShipList_Change(page);
            SoubiList_Change(page);
        }

        #region 生成行
        private void ShipList_Change(int x)
        {
            row1.DataContext = ships[x * 10 - 9];
            row2.DataContext = ships[x * 10 - 8];
            row3.DataContext = ships[x * 10 - 7];
            row4.DataContext = ships[x * 10 - 6];
            row5.DataContext = ships[x * 10 - 5];
            row6.DataContext = ships[x * 10 - 4];
            row7.DataContext = ships[x * 10 - 3];
            row8.DataContext = ships[x * 10 - 2];
            row9.DataContext = ships[x * 10 - 1];
            row10.DataContext = ships[x * 10];
        }

        private void SoubiList_Change(int x)
        {
            soubirow1.DataContext = equips[x * 10 - 9];
            soubirow2.DataContext = equips[x * 10 - 8];
            soubirow3.DataContext = equips[x * 10 - 7];
            soubirow4.DataContext = equips[x * 10 - 6];
            soubirow5.DataContext = equips[x * 10 - 5];
            soubirow6.DataContext = equips[x * 10 - 4];
            soubirow7.DataContext = equips[x * 10 - 3];
            soubirow8.DataContext = equips[x * 10 - 2];
            soubirow9.DataContext = equips[x * 10 - 1];
            soubirow10.DataContext = equips[x * 10];
        }
        #endregion

        #region 显示消失动画
        private void ShipList_Hide(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 245;
            da.To = 700;
            da.Duration = TimeSpan.FromSeconds(0.2);
            da.Completed += new EventHandler(Shadow_Hide);
            ShipList.BeginAnimation(Canvas.LeftProperty, da);
        }

        private void ShipView_Hide(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 490;
            da.To = 700;
            da.Duration = TimeSpan.FromSeconds(0.2);
            da.Completed += new EventHandler(Shadow_Hide2);
            ShipView.BeginAnimation(Canvas.LeftProperty, da);
        }

        private void ShipDetail_Hide(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 210;
            da.To = 700;
            da.Duration = TimeSpan.FromSeconds(0.2);
            da.Completed += new EventHandler(Shadow_Hide3);
            ShipDetail.BeginAnimation(Canvas.LeftProperty, da);
        }

        private void SoubiList_Hide(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 210;
            da.To = 700;
            da.Duration = TimeSpan.FromSeconds(0.2);
            da.Completed += new EventHandler(Shadow_Hide4);
            SoubiList.BeginAnimation(Canvas.LeftProperty, da);
            da.From = 0;
            da.To = 210;
            da.Duration = TimeSpan.FromSeconds(0.2);
            ShipDetail.BeginAnimation(Canvas.LeftProperty, da);
        }

        private void SoubiChange_Hide(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 480;
            da.To = 700;
            da.Duration = TimeSpan.FromSeconds(0.2);
            da.Completed += new EventHandler(Shadow_Hide5);
            SoubiChange.BeginAnimation(Canvas.LeftProperty, da);
        }

        private void Shadow_Hide(object sender, EventArgs e)
        {
            SelectPanel.Visibility = Visibility.Hidden;
        }
        private void Shadow_Hide2(object sender, EventArgs e)
        {
            ConfirmPanel.Visibility = Visibility.Hidden;
        }
        private void Shadow_Hide3(object sender, EventArgs e)
        {
            DetailPanel.Visibility = Visibility.Hidden;
        }
        private void Shadow_Hide4(object sender, EventArgs e)
        {
            SoubiPanel.Visibility = Visibility.Hidden;
        }
        private void Shadow_Hide5(object sender, EventArgs e)
        {
            ChangePanel.Visibility = Visibility.Hidden;
        }
        #endregion

        #region 按下变更按钮
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da1 = new DoubleAnimation();
            da1.From = 10;
            da1.To = -170;
            da1.Duration = TimeSpan.FromSeconds(0.15);
            DoubleAnimation da2 = new DoubleAnimation();
            da2.From = 10;
            da2.To = -170;
            da2.Duration = TimeSpan.FromSeconds(0.15);

            if (posnum == 1)
            {
                if (Dock1.InfoBox.DataContext == null)
                {
                    Dock1.MaskLeft.BeginAnimation(Canvas.LeftProperty, da1);
                    Dock1.MaskRight.BeginAnimation(Canvas.RightProperty, da2);
                }
                Dock1.InfoBox.DataContext = example[1];
            }
            if (posnum == 2)
            {
                if (Dock2.InfoBox.DataContext == null)
                {
                    Dock2.MaskLeft.BeginAnimation(Canvas.LeftProperty, da1);
                    Dock2.MaskRight.BeginAnimation(Canvas.RightProperty, da2);
                }
                Dock2.InfoBox.DataContext = example[2];
            }
            if (posnum == 3)
            {
                if (Dock3.InfoBox.DataContext == null)
                {
                    Dock3.MaskLeft.BeginAnimation(Canvas.LeftProperty, da1);
                    Dock3.MaskRight.BeginAnimation(Canvas.RightProperty, da2);
                }
                Dock3.InfoBox.DataContext = example[3];
            }
            if (posnum == 4)
            {
                if (Dock4.InfoBox.DataContext == null)
                {
                    Dock4.MaskLeft.BeginAnimation(Canvas.LeftProperty, da1);
                    Dock4.MaskRight.BeginAnimation(Canvas.RightProperty, da2);
                }
                Dock4.InfoBox.DataContext = example[4];
            }
            if (posnum == 5)
            {
                if (Dock5.InfoBox.DataContext == null)
                {
                    Dock5.MaskLeft.BeginAnimation(Canvas.LeftProperty, da1);
                    Dock5.MaskRight.BeginAnimation(Canvas.RightProperty, da2);
                }
                Dock5.InfoBox.DataContext = example[5];
            }
            if (posnum == 6)
            {
                if (Dock6.InfoBox.DataContext == null)
                {
                    Dock6.MaskLeft.BeginAnimation(Canvas.LeftProperty, da1);
                    Dock6.MaskRight.BeginAnimation(Canvas.RightProperty, da2);
                }
                Dock6.InfoBox.DataContext = example[6];
            }
            ConfirmPanel.Visibility = Visibility.Hidden;
            SelectPanel.Visibility = Visibility.Hidden;
        }
        #endregion

        #region 计算结果展开动画
        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 455;
            da.To = 700;
            da.Duration = TimeSpan.FromSeconds(0.2);
            this.BeginAnimation(HeightProperty, da);
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 700;
            da.To = 455;
            da.Duration = TimeSpan.FromSeconds(0.2);
            this.BeginAnimation(HeightProperty, da);
        }
        #endregion

        private void row0_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da1 = new DoubleAnimation();
            da1.From = -170;
            da1.To = 10;
            da1.Duration = TimeSpan.FromSeconds(0.15);
            DoubleAnimation da2 = new DoubleAnimation();
            da2.From = -170;
            da2.To = 0;
            da2.Duration = TimeSpan.FromSeconds(0.15);

            if (posnum == 1)
            {
                if (Dock1.InfoBox.DataContext != null)
                {
                    Dock1.MaskLeft.BeginAnimation(Canvas.LeftProperty, da1);
                    Dock1.MaskRight.BeginAnimation(Canvas.RightProperty, da2);
                }
                Dock1.InfoBox.DataContext = null;
            }
            if (posnum == 2)
            {
                if (Dock2.InfoBox.DataContext != null)
                {
                    Dock2.MaskLeft.BeginAnimation(Canvas.LeftProperty, da1);
                    Dock2.MaskRight.BeginAnimation(Canvas.RightProperty, da2);
                }
                Dock2.InfoBox.DataContext = null;
            }
            if (posnum == 3)
            {
                if (Dock3.InfoBox.DataContext != null)
                {
                    Dock3.MaskLeft.BeginAnimation(Canvas.LeftProperty, da1);
                    Dock3.MaskRight.BeginAnimation(Canvas.RightProperty, da2);
                }
                Dock3.InfoBox.DataContext = null;
            }
            if (posnum == 4)
            {
                if (Dock4.InfoBox.DataContext != null)
                {
                    Dock4.MaskLeft.BeginAnimation(Canvas.LeftProperty, da1);
                    Dock4.MaskRight.BeginAnimation(Canvas.RightProperty, da2);
                }
                Dock4.InfoBox.DataContext = null;
            }
            if (posnum == 5)
            {
                if (Dock5.InfoBox.DataContext != null)
                {
                    Dock5.MaskLeft.BeginAnimation(Canvas.LeftProperty, da1);
                    Dock5.MaskRight.BeginAnimation(Canvas.RightProperty, da2);
                }
                Dock5.InfoBox.DataContext = null;
            }
            if (posnum == 6)
            {
                if (Dock6.InfoBox.DataContext != null)
                {
                    Dock6.MaskLeft.BeginAnimation(Canvas.LeftProperty, da1);
                    Dock6.MaskRight.BeginAnimation(Canvas.RightProperty, da2);
                }
                Dock6.InfoBox.DataContext = null;
            }
            SelectPanel.Visibility = Visibility.Hidden;

        }
    }
}
