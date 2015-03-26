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
using KanDic.Plugins.Simulator;

namespace KanDic.Plugins
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Calculator : MetroWindow
    {
        public List<Kan> ships;
        public List<Soubi> equips;
        public MoniKan[] example = new MoniKan[7];
        public int shippage, soubipage, rownum, posnum, soubinum;//page一页，rownum哪一行，posnum哪只船，soubinum哪格装备

        public class Level
        {
            public int LV { get; set; }
            public Level(){}
        };//司令部等级
        Level commanderlv = new Level();

        public List<string> airplane = new List<string>() { "艦上戦闘機", "艦上爆撃機", "艦上攻撃機", "艦上偵察機", "水上爆撃機", "水上偵察機" };

        public Calculator()
        {
            InitializeComponent();
            Set_Ship(KanDic.Viewer.KanColle.ships);
            Set_Soubi(KanDic.Viewer.Equipment.equips);
            commanderlv.LV = 1;
            LevelPanel.DataContext = commanderlv;
            shippage = 1;
            soubipage = 1;
            Dock1.NumImage.Source = new BitmapImage(new Uri("/Cache/Calculate/1.PNG", UriKind.Relative));
            Dock2.NumImage.Source = new BitmapImage(new Uri("/Cache/Calculate/2.PNG", UriKind.Relative));
            Dock3.NumImage.Source = new BitmapImage(new Uri("/Cache/Calculate/3.PNG", UriKind.Relative));
            Dock4.NumImage.Source = new BitmapImage(new Uri("/Cache/Calculate/4.PNG", UriKind.Relative));
            Dock5.NumImage.Source = new BitmapImage(new Uri("/Cache/Calculate/5.PNG", UriKind.Relative));
            Dock6.NumImage.Source = new BitmapImage(new Uri("/Cache/Calculate/6.PNG", UriKind.Relative));
            ShipList_Change(shippage);
            SoubiList_Change(soubipage);
        }

        #region 形成船List
        private void Set_Ship(Kan[] xx)
        {
            ships = new List<Kan>();
            bool[] isadded = new bool[500];
            int link, links;
            for (int i = 1; i < 500; i++)
            {
                isadded[i] = false;
            }
            for (int i = 1; i < 500; i++)
            {
                //若不在isadded列表中 则加入
                if (xx[i] != null && !isadded[i])
                {
                    if (xx[i].BasicInfo.Name != null) ships.Add(xx[i]);

                    //判断是否有存在【改】
                    if (xx[i].UpdateInfo.LinkNumber != null)
                    {
                        link = Convert.ToInt32(xx[i].UpdateInfo.LinkNumber);

                        //防止读取到【改】的船二次加入列表
                        if (!isadded[link]) ships.Add(xx[link]);
                        isadded[link] = true;

                        //判断是否存在【改二】一并加入
                        if (xx[link].UpdateInfo.LinkNumber != null)
                        {
                            links = Convert.ToInt32(xx[link].UpdateInfo.LinkNumber);
                            ships.Add(xx[links]);
                            isadded[links] = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region 形成装备List
        private void Set_Soubi(Soubi[] xx)
        {
            equips = new List<Soubi>();
            for (int i = 1; i < xx.Count(); i++)
            {
                if (xx[i] != null) equips.Add(xx[i]);
            }
            equips.Sort((x,y) =>{
                string aa = x.Icon.Substring(18, x.Icon.Length - 22);
                string bb = y.Icon.Substring(18, y.Icon.Length - 22);
                if (int.Parse(aa) > int.Parse(bb))
                    return 1;
                else if (int.Parse(aa) == int.Parse(bb))
                    return 0;
                else
                    return -1;
            });
        }
        #endregion

        #region 生成船按钮
        public void ShipList_Change(int x)
        {
            PageNum.Text = x.ToString();
            if ( x * 10 - 10 < ships.Count )
                row1.DataContext = ships[x * 10 - 10];
            else
                row1.DataContext = null;
            if ( x * 10 - 9 < ships.Count )
                row2.DataContext = ships[x * 10 - 9];
            else
                row2.DataContext = null;
            if ( x * 10 - 8 < ships.Count )
                row3.DataContext = ships[x * 10 - 8];
            else
                row3.DataContext = null;
            if ( x * 10 - 7 < ships.Count )
                row4.DataContext = ships[x * 10 - 7];
            else
                row4.DataContext = null;
            if ( x * 10 - 6 < ships.Count )
                row5.DataContext = ships[x * 10 - 6];
            else
                row5.DataContext = null;
            if ( x * 10 - 5 < ships.Count )
                row6.DataContext = ships[x * 10 - 5];
            else
                row6.DataContext = null;
            if ( x * 10 - 4 < ships.Count )
                row7.DataContext = ships[x * 10 - 4];
            else
                row7.DataContext = null;
            if ( x * 10 - 3 < ships.Count )
                row8.DataContext = ships[x * 10 - 3];
            else
                row8.DataContext = null;
            if ( x * 10 - 2 < ships.Count )
                row9.DataContext = ships[x * 10 - 2];
            else
                row9.DataContext = null;
            if ( x * 10 - 1 < ships.Count )
                row10.DataContext = ships[x * 10 - 1];
            else
                row10.DataContext = null;
        }
        #endregion

        #region 生成装备按钮
        public void SoubiList_Change(int x)
        {
            PageNums.Text = soubipage.ToString();
            if (x * 10 - 10 < equips.Count)
                soubirow1.DataContext = equips[x * 10 - 10];
            else
                soubirow1.DataContext = null;
            if (x * 10 - 9 < equips.Count)
                soubirow2.DataContext = equips[x * 10 - 9];
            else
                soubirow2.DataContext = null;
            if (x * 10 - 8 < equips.Count)
                soubirow3.DataContext = equips[x * 10 - 8];
            else
                soubirow3.DataContext = null;
            if (x * 10 - 7 < equips.Count)
                soubirow4.DataContext = equips[x * 10 - 7];
            else
                soubirow4.DataContext = null;
            if (x * 10 - 6 < equips.Count)
                soubirow5.DataContext = equips[x * 10 - 6];
            else
                soubirow5.DataContext = null;
            if (x * 10 - 5 < equips.Count)
                soubirow6.DataContext = equips[x * 10 - 5];
            else
                soubirow6.DataContext = null;
            if (x * 10 - 4 < equips.Count)
                soubirow7.DataContext = equips[x * 10 - 4];
            else
                soubirow7.DataContext = null;
            if (x * 10 - 3 < equips.Count)
                soubirow8.DataContext = equips[x * 10 - 3];
            else
                soubirow8.DataContext = null;
            if (x * 10 - 2 < equips.Count)
                soubirow9.DataContext = equips[x * 10 - 2];
            else
                soubirow9.DataContext = null;
            if (x * 10 - 1 < equips.Count)
                soubirow10.DataContext = equips[x * 10 - 1];
            else
                soubirow10.DataContext = null;
        }
        #endregion 

        #region 生成装备对比
        public void Compare_Init()
        {
            Soubi yuan = new Soubi();
            Soubi hou = new Soubi();
            yuan = example[posnum].soubi[soubinum];
            hou = equips[soubipage * 10 - 11 + rownum];
            Compare_List(yuan,BeforeChange);
            Compare_List(hou,AfterChange);
        }

        private void Compare_List(Soubi x,StackPanel target)
        {
            List<string> strlist = new List<string>();
            strlist.Clear();
            strlist.Add(x.Icon);
            strlist.Add(x.Name);
            if (x.Power != 0)
            {
                strlist.Add("火力+" + x.Power.ToString());
            }
            if (x.Torpedo != 0)
            {
                strlist.Add("雷装+" + x.Torpedo.ToString());
            }
            if (x.Bomb != 0)
            {
                strlist.Add("爆装+" + x.Bomb.ToString());
            }
            if (x.Air != 0)
            {
                strlist.Add("对空+" + x.Air.ToString());
            }
            if (x.Antisub != 0)
            {
                strlist.Add("对潜+" + x.Antisub.ToString());
            }
            if (x.Search != 0)
            {
                strlist.Add("索敌+" + x.Search.ToString());
            }
            if (x.Hitrate != 0)
            {
                strlist.Add("命中+" + x.Hitrate.ToString());
            }
            if (x.Dodge != 0)
            {
                strlist.Add("回避+" + x.Dodge.ToString());
            }
            target.DataContext = strlist;
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

        #region 按下舰船变更按钮
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            example[posnum] = new MoniKan(ships[shippage * 10 - 11 + rownum]);
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
            Result_Refresh();
            ConfirmPanel.Visibility = Visibility.Hidden;
            SelectPanel.Visibility = Visibility.Hidden;
        }
        #endregion

        #region 按下装备更换按钮
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            example[posnum].soubi[soubinum] = equips[soubipage * 10 - 11 + rownum];
            SoubiButton_Init();
            DataBox.DataContext = new ShowData(example[posnum]);
            Result_Refresh();
            SoubiList_Hide(null,null);
            ChangePanel.Visibility = Visibility.Hidden;
        }

        public void SoubiButton_Init()
        {
            soubi1.Carry.Text = (airplane.Find(x => x == example[posnum].soubi[1].Type) == null) ? "" : example[posnum].Carrys[1].ToString();
            soubi1.NameText.Text = example[posnum].soubi[1].Name;
            soubi1.Icon.Source = (example[posnum].soubi[1].Icon == null) ? null : new BitmapImage(new Uri(example[posnum].soubi[1].Icon, UriKind.Relative));

            soubi2.Carry.Text = (airplane.Find(x => x == example[posnum].soubi[2].Type) == null) ? "" : example[posnum].Carrys[2].ToString();
            soubi2.NameText.Text = example[posnum].soubi[2].Name;
            soubi2.Icon.Source = (example[posnum].soubi[2].Icon == null) ? null : new BitmapImage(new Uri(example[posnum].soubi[2].Icon, UriKind.Relative));

            soubi3.Carry.Text = (airplane.Find(x => x == example[posnum].soubi[3].Type) == null) ? "" : example[posnum].Carrys[3].ToString();
            soubi3.NameText.Text = example[posnum].soubi[3].Name;
            soubi3.Icon.Source = (example[posnum].soubi[3].Icon == null) ? null : new BitmapImage(new Uri(example[posnum].soubi[3].Icon, UriKind.Relative));

            soubi4.Carry.Text = (airplane.Find(x => x == example[posnum].soubi[4].Type) == null) ? "" : example[posnum].Carrys[4].ToString();
            soubi4.NameText.Text = example[posnum].soubi[4].Name;
            soubi4.Icon.Source = (example[posnum].soubi[4].Icon == null) ? null : new BitmapImage(new Uri(example[posnum].soubi[4].Icon, UriKind.Relative));
        }
        #endregion

        #region 计算结果栏展开
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

        #region 取下舰娘动画
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
            example[posnum] = null;
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
            Result_Refresh();
            SelectPanel.Visibility = Visibility.Hidden;
        }
        #endregion

        #region 页数变更按钮
        private void Page_Prev(object sender, MouseButtonEventArgs e)
        {
            if (shippage > 1)
            {
                shippage--;
                ShipList_Change(shippage);
            }
        }

        private void Page_Next(object sender, MouseButtonEventArgs e)
        {
            if (shippage < ships.Count() / 10 + 1)
            {
                shippage++;
                ShipList_Change(shippage);
            }
        }

        private void Pages_Prev(object sender, MouseButtonEventArgs e)
        {
            if (soubipage > 1)
            {
                soubipage--;
                SoubiList_Change(soubipage);
            }
        }

        private void Pages_Next(object sender, MouseButtonEventArgs e)
        {
            if (soubipage < equips.Count() / 10 + 1)
            {
                soubipage++;
                SoubiList_Change(soubipage);
            }
        }
        #endregion

        #region 各项数值计算
        private void Over_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();
            ResultPanel.Children.Add(new Over(example, commanderlv.LV));
        }
        private void Koukusen_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();
            ResultPanel.Children.Add(new Koukusen(example));
        }
        private void Raigekisen_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();
            ResultPanel.Children.Add(new Koukusen(example));
        }
        private void Hougekisen_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();
            ResultPanel.Children.Add(new Koukusen(example));
        }
        private void Yasen_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();
            ResultPanel.Children.Add(new Koukusen(example));
        }
        #endregion

        private void Result_Refresh()
        {
            if ((bool)rb1.IsChecked) Over_Click(null, null);
            if ((bool)rb2.IsChecked) Koukusen_Click(null, null);
            if ((bool)rb3.IsChecked) Raigekisen_Click(null, null);
            if ((bool)rb4.IsChecked) Hougekisen_Click(null, null);
            if ((bool)rb5.IsChecked) Yasen_Click(null, null);
        }

        private void LevelPanel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if ((bool)rb1.IsChecked) Over_Click(null, null);
        }
    }
}
