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
using KanDic.Viewer;
using KanDic.Resources;
using MahApps.Metro.Controls;
using Visifire.Charts;

namespace KanDic.Window
{
    /// <summary>
    /// KanDetail.xaml 的交互逻辑
    /// </summary>
    public partial class KanDetail
    {
        public Kan kanmusu;
        public bool isdrag;
        public Point oldPoint = new Point();
        public double minleft, mintop, maxleft, maxtop ,xPos, yPos ,xmove, ymove;

        public KanDetail(int num)
        {
            InitializeComponent();
            kanmusu = KanDic.Viewer.KanColle.ships[num];
            MainDetail.DataContext = kanmusu;
            Detail_Init();
        }

        #region 数据生成-缺图标
        public void Detail_Init()
        {
            this.Title = kanmusu.Name;
            //是否大建
            if (kanmusu.OnlyHuge == "True")
            {
                IfHuge.Text = "大";
                Color color = (Color)ColorConverter.ConvertFromString("Red");
                SolidColorBrush brush = new SolidColorBrush(color);
                IfHugeBack.Background = brush;
            }
            //建造时间
            TextBlock temptb = new TextBlock();
            temptb.VerticalAlignment = VerticalAlignment.Center;
            temptb.HorizontalAlignment = HorizontalAlignment.Center;
            temptb.Text = String.Format("{0:00}:{1:00}:{2:00}", kanmusu.Hour, kanmusu.Minute, 0);
            TimePanel.Children.Add(temptb);
            //补给量
            Draw_Supply(Supply1, false);
            Draw_Supply(Supply2, false);
            Draw_Supply(Supply3, true);
            Draw_Supply(Supply4, true);
            MainData.DataContext = kanmusu;
            //SobiList.DataContext = new SobiIcon(kanmusu.Equip1, kanmusu.Equip2, kanmusu.Equip3, kanmusu.Equip4);
            Show_Chart();
        }
        #endregion

        #region 绘制补给格
        private void Draw_Supply(StackPanel temp,bool ifall)
        {
            for (int i = 1; i <= 10; i++)
            {
                Border tempbd = new Border();
                tempbd.BorderThickness = new Thickness(1);
                tempbd.BorderBrush = new SolidColorBrush(Color.FromRgb(197, 184, 170));
                tempbd.Width = 5;
                tempbd.Height = 10;
                tempbd.Margin = new Thickness(1, 1, 0, 0);
                if (i <= 8 && !ifall)
                {
                    //tempbd.Background = new SolidColorBrush(Color.FromRgb(153, 255, 102));
                    tempbd.Background = new SolidColorBrush(Color.FromRgb(18, 255, 0));
                    temp.Children.Add(tempbd);
                }
                else
                {
                    tempbd.Background = new SolidColorBrush(Color.FromRgb(229, 216, 202));
                    temp.Children.Add(tempbd);
                }
            }
        }
        #endregion

        #region Visifire表格生成
        private void Show_Chart()
        {
            hl.YValue = Convert.ToInt32(kanmusu.Power);
            lz.YValue = Convert.ToInt32(kanmusu.Torpedo);
            dk.YValue = Convert.ToInt32(kanmusu.Air);
            hb.YValue = Convert.ToInt32(kanmusu.Dodge);
            nj.YValue = Convert.ToInt32(kanmusu.HP);
        }
        #endregion

        #region 窗口生成后，调节图片可移动范围
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(MainImage, ImageBox.ActualWidth / 2 - MainImage.ActualWidth / 2);
            Canvas.SetTop(MainImage, 0);
            minleft = ImageBox.ActualWidth - MainImage.ActualWidth;
            mintop = ImageBox.ActualHeight - MainImage.ActualHeight;
            maxleft = 0;
            maxtop = 0;
        }
        #endregion

        #region 鼠标模拟拖动
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isdrag = true;
            oldPoint = e.GetPosition(ImageBox);
            xPos = Canvas.GetLeft(MainImage);
            yPos = Canvas.GetTop(MainImage);
            //Console.WriteLine(oldPoint);
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isdrag) return;
            xmove = e.GetPosition(ImageBox).X - oldPoint.X;
            ymove = e.GetPosition(ImageBox).Y - oldPoint.Y;
            if (xPos + xmove > minleft && xPos + xmove < maxleft) MainImage.SetValue(Canvas.LeftProperty, xPos + xmove);
            if (yPos + ymove > mintop && yPos + ymove < maxtop) MainImage.SetValue(Canvas.TopProperty, yPos + ymove);
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isdrag = false;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            isdrag = false;
        }
        #endregion

        #region 调节窗口大小
        private void ImageBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ImageBox.ActualWidth < MainImage.ActualWidth)
            {
                minleft = ImageBox.ActualWidth - MainImage.ActualWidth;
                maxleft = 0;
            }
            else
            {
                minleft = 0;
                maxleft = ImageBox.ActualWidth - MainImage.ActualWidth;
            }
            if (ImageBox.ActualHeight < MainImage.ActualHeight)
            {
                mintop = ImageBox.ActualHeight - MainImage.ActualHeight;
                maxtop = 0;
            }else
            {
                mintop = 0;
                maxtop = ImageBox.ActualHeight - MainImage.ActualHeight;
            }
        }
        #endregion

        #region 开启关闭最大值
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox xx = (CheckBox)sender;
            if ((bool)xx.IsChecked)
            {
                MainData.DataContext = new NewKan(kanmusu, true);
                hl.YValue = Convert.ToInt32(kanmusu.MaxPower);
                lz.YValue = Convert.ToInt32(kanmusu.MaxTorpedo);
                dk.YValue = Convert.ToInt32(kanmusu.MaxAir);
                hb.YValue = Convert.ToInt32(kanmusu.MaxDodge);
                nj.YValue = Convert.ToInt32(kanmusu.MaxHP);
            }
            else
            {
                MainData.DataContext = new NewKan(kanmusu, false);
                hl.YValue = Convert.ToInt32(kanmusu.Power);
                lz.YValue = Convert.ToInt32(kanmusu.Torpedo);
                dk.YValue = Convert.ToInt32(kanmusu.Air);
                hb.YValue = Convert.ToInt32(kanmusu.Dodge);
                nj.YValue = Convert.ToInt32(kanmusu.HP);
            }
        }
        #endregion

        #region 打开改造后窗口
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool IsOpened = false;
            Kan[] ships = KanDic.Viewer.KanColle.ships;
            int num = kanmusu.LinkNumber;
            if (num == 0) return;
            foreach (System.Windows.Window element in Application.Current.Windows)
            {
                string type = element.GetType().ToString();
                if (type == "KanDic.Window.KanDetail")
                {
                    if (element.Title == ships[num].Name)
                    {
                        IsOpened = true;
                        element.Activate();
                        break;
                    }
                }
            }
            if (!IsOpened)
            {
                Window.KanDetail win = new Window.KanDetail(num);
                win.Owner = this.Owner;
                win.Show();
            }
        }
        #endregion
    }
}
