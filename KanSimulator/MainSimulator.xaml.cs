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
using KanData;
using KanSimulator.Class;
using KanSimulator.Module;

namespace KanSimulator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainSimulator : MetroWindow
    {
        public List<Kan> ships;//排序后的船只list
        public List<Soubi> equips;//排序后的装备list
        public MoniKan[] example = new MoniKan[7];//储存队列中6只船，0作废
        public int shippage, soubipage, rownum, posnum, soubinum;
        //shippage船只页数，soubipage装备页数，rownum记录按下的行数，posnum队列中船只编号，soubinum装备格编号
        public int formation, status;//阵型选择，交战状态

        public class Level
        {
            public int LV { get; set; }
            public Level(){}
        };//司令部等级
        public Level commanderlv = new Level();

        public List<string> airplane = new List<string>() { "艦上戦闘機", "艦上爆撃機", "艦上攻撃機", "艦上偵察機", "水上爆撃機", "水上偵察機" };
        public List<string> area = new List<string>() { "1-1", "1-2", "1-3", "1-4", "1-5", "2-1", "2-2", "2-3", "2-4", "2-5", "3-1", "3-2", "3-3", "3-4", "3-5", "4-1", "4-2", "4-3", "4-4", "4-5", "5-1", "5-2", "5-3", "5-4", "5-5", "6-1", "6-2" };

        public MainSimulator()
        {
            InitializeComponent();
            //从KanData中读取数据
            KanData.DataInit di = new DataInit();
            Set_Ship(di.kanlist);
            Set_Soubi(di.soubilist);
            //初始化
            commanderlv.LV = 1;
            LevelPanel.DataContext = commanderlv;
            shippage = 1;
            soubipage = 1;
            formation = 0;
            status = 0;
            Dock1.NumImage.Source = new BitmapImage(new Uri("/Image/1.PNG", UriKind.Relative));
            Dock2.NumImage.Source = new BitmapImage(new Uri("/Image/2.PNG", UriKind.Relative));
            Dock3.NumImage.Source = new BitmapImage(new Uri("/Image/3.PNG", UriKind.Relative));
            Dock4.NumImage.Source = new BitmapImage(new Uri("/Image/4.PNG", UriKind.Relative));
            Dock5.NumImage.Source = new BitmapImage(new Uri("/Image/5.PNG", UriKind.Relative));
            Dock6.NumImage.Source = new BitmapImage(new Uri("/Image/6.PNG", UriKind.Relative));
            ShipList_Change(shippage);
            SoubiList_Change(soubipage);
            ComboBox_Init();
        }

        #region 形成船List
        private void Set_Ship(List<Kan> xx)
        {
            ships = new List<Kan>();
            bool[] isadded = new bool[500];
            int link, links;
            for (int i = 0; i < 500; i++)
            {
                isadded[i] = false;
            }
            for (int i = 0; i < xx.Count; i++)
            {
                //若不在isadded列表中 则加入
                if (xx[i] != null)
                {
                    if (!isadded[xx[i].Number]) ships.Add(xx[i]);//判断是否已加入表

                    link = xx[i].LinkNumber;//获取【改】编号
                    if (link != 0 && isadded[link])//判断是否有存在、防止二次加入表
                    {
                        ships.Add(xx[link]);
                        isadded[link] = true;

                        links = xx[link].LinkNumber;//获取【改二】编号
                        if (links != 0)//判断是否存在【改二】一并加入
                        {
                            ships.Add(xx[links]);
                            isadded[links] = true;
                        }
                    }
                }
            }
            for (int i = 0; i < ships.Count; i++)
            {
                ships[i].ImageNormal = "/KanDic;component/Cache/ships/" + ships[i].FileName + ".swf/Image 5.jpg";
                ships[i].ImageSmall = "/KanDic;component/Cache/ships/" + ships[i].FileName + ".swf/Image 1.jpg";
            }
        }
        #endregion

        #region 形成装备List
        private void Set_Soubi(List<Soubi> xx)
        {
            equips = new List<Soubi>();
            for (int i = 0; i < xx.Count(); i++)
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
            example[posnum] = null;
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
            //example[posnum] = new MoniKan(ships[shippage * 10 - 11 + rownum]);
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
            soubi1.seticon(example[posnum].soubi[1].Icon);

            soubi2.Carry.Text = (airplane.Find(x => x == example[posnum].soubi[2].Type) == null) ? "" : example[posnum].Carrys[2].ToString();
            soubi2.NameText.Text = example[posnum].soubi[2].Name;
            soubi2.seticon(example[posnum].soubi[2].Icon);

            soubi3.Carry.Text = (airplane.Find(x => x == example[posnum].soubi[3].Type) == null) ? "" : example[posnum].Carrys[3].ToString();
            soubi3.NameText.Text = example[posnum].soubi[3].Name;
            soubi3.seticon(example[posnum].soubi[3].Icon);

            soubi4.Carry.Text = (airplane.Find(x => x == example[posnum].soubi[4].Type) == null) ? "" : example[posnum].Carrys[4].ToString();
            soubi4.NameText.Text = example[posnum].soubi[4].Name;
            soubi4.seticon(example[posnum].soubi[4].Icon);
        }
        #endregion

        #region 关卡初始化与选择
        private void ComboBox_Init()
        {
            BattleNum.ItemsSource = area;
            BattleNum.SelectedIndex = 0;
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

        #region 计算结果选择按钮
        private void Over_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();
            ResultPanel.Children.Add(new OverView(example, commanderlv.LV));
        }
        private void Koukusen_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();
            ResultPanel.Children.Add(new Koukusen(example));
        }
        private void Raigekisen_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();
            ResultPanel.Children.Add(new Raigekisen(example));
        }
        private void Hougekisen_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();
            ResultPanel.Children.Add(new Hougekisen(example));
        }
        private void Yasen_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();
            ResultPanel.Children.Add(new Yasen(example));
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

        //计算公式确认
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            Help hp = new Help();
            hp.Owner = this;
            hp.Show();
        }

        //模拟动态战斗
        private void Battle_Click(object sender, RoutedEventArgs e)
        {
            BattleMain bm = new BattleMain();
            bm.Owner = this;
            bm.ShowDialog();
        }
    }
}
