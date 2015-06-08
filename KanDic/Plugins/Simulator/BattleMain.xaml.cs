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
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace KanDic.Plugins.Simulator
{
    /// <summary>
    /// BattleMain.xaml 的交互逻辑
    /// </summary>
    public partial class BattleMain : System.Windows.Window
    {
        public int teamnum;

        public BattleMain()
        {
            InitializeComponent();
            teamnum = 6;
            SetFormationSelect();
        }

        #region 设置可选阵型
        private void SetFormationSelect()
        {
            FormSelect fs1 = new FormSelect(0, teamnum);
            FormSelectCanvan.Children.Add(fs1);
            Canvas.SetTop(fs1, 61);
            Canvas.SetLeft(fs1, 387);

            if (teamnum >= 4)
            {
                FormSelect fs2 = new FormSelect(1, teamnum);
                FormSelectCanvan.Children.Add(fs2);
                Canvas.SetTop(fs2, 61);
                Canvas.SetLeft(fs2, 517);
            }

            if (teamnum >= 5)
            {
                FormSelect fs3 = new FormSelect(2, teamnum);
                FormSelectCanvan.Children.Add(fs3);
                Canvas.SetTop(fs3, 61);
                Canvas.SetLeft(fs3, 649);
            }

            if (teamnum >= 4)
            {
                FormSelect fs4 = new FormSelect(3, teamnum);
                FormSelectCanvan.Children.Add(fs4);
                Canvas.SetTop(fs4, 220);
                Canvas.SetLeft(fs4, 455);
            }

            if (teamnum >= 4)
            {
                FormSelect fs5 = new FormSelect(4, teamnum);
                FormSelectCanvan.Children.Add(fs5);
                Canvas.SetTop(fs5, 220);
                Canvas.SetLeft(fs5, 586);
            }
        }
        #endregion

        #region 阵型设置按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartAnimationCanvas.Visibility = Visibility.Visible;
            Random ra = new Random(DateTime.Now.Millisecond);
            int status = ra.Next(65535) % 4;
            int formation = ra.Next(DateTime.Now.Second) % 5 + 1;
            int form = ra.Next(DateTime.Now.Second + 1117) % 5 + 1;
            switch (status)
            {
                case 0://同航战
                    Font1.Source = new BitmapImage(new Uri("/Cache/battle/font1-1.png", UriKind.Relative));
                    Font2.Source = new BitmapImage(new Uri("/Cache/battle/font2-1.png", UriKind.Relative));
                    FormGood.Source = null;
                    break;
                case 1://返航战
                    Font1.Source = new BitmapImage(new Uri("/Cache/battle/font1-2.png", UriKind.Relative));
                    Font2.Source = new BitmapImage(new Uri("/Cache/battle/font2-1.png", UriKind.Relative));
                    FormGood.Source = null;
                    break;
                case 2://T优
                    Font1.Source = new BitmapImage(new Uri("/Cache/battle/font1-3.png", UriKind.Relative));
                    Font2.Source = new BitmapImage(new Uri("/Cache/battle/font2-2.png", UriKind.Relative));
                    FormGood.Source = new BitmapImage(new Uri("/Cache/battle/Ty.png", UriKind.Relative));
                    break;
                case 3://T劣
                    Font1.Source = new BitmapImage(new Uri("/Cache/battle/font1-3.png", UriKind.Relative));
                    Font2.Source = new BitmapImage(new Uri("/Cache/battle/font2-2.png", UriKind.Relative));
                    FormGood.Source = new BitmapImage(new Uri("/Cache/battle/Tl.png", UriKind.Relative));
                    break;
            }
            TeamForm.Source = new BitmapImage(new Uri("/Cache/battle/formation" + formation + ".png", UriKind.Relative));
            EnemyForm.Source = new BitmapImage(new Uri("/Cache/battle/form" + form + ".png", UriKind.Relative));
            
            BattleStart();
        }
        #endregion

        #region 阵型显示动画
        private void BattleStart()
        {
            DoubleAnimationUsingKeyFrames da_height = new DoubleAnimationUsingKeyFrames();
            da_height.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_height.KeyFrames.Add(new LinearDoubleKeyFrame(111, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_height.KeyFrames.Add(new LinearDoubleKeyFrame(111, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.4))));
            da_height.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.6))));

            DoubleAnimationUsingKeyFrames da_line1 = new DoubleAnimationUsingKeyFrames();
            da_line1.KeyFrames.Add(new LinearDoubleKeyFrame(402, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_line1.KeyFrames.Add(new LinearDoubleKeyFrame(346.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_line1.KeyFrames.Add(new LinearDoubleKeyFrame(346.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.4))));
            da_line1.KeyFrames.Add(new LinearDoubleKeyFrame(402, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.6))));

            DoubleAnimationUsingKeyFrames da_line2 = new DoubleAnimationUsingKeyFrames();
            da_line2.KeyFrames.Add(new LinearDoubleKeyFrame(78, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_line2.KeyFrames.Add(new LinearDoubleKeyFrame(22.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_line2.KeyFrames.Add(new LinearDoubleKeyFrame(22.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.4))));
            da_line2.KeyFrames.Add(new LinearDoubleKeyFrame(78, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.6))));

            DoubleAnimationUsingKeyFrames da_form1 = new DoubleAnimationUsingKeyFrames();
            da_form1.KeyFrames.Add(new DiscreteDoubleKeyFrame(-270, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_form1.KeyFrames.Add(new LinearDoubleKeyFrame(396, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7))));
            da_form1.KeyFrames.Add(new LinearDoubleKeyFrame(396, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.2))));
            da_form1.KeyFrames.Add(new LinearDoubleKeyFrame(800, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.6))));

            DoubleAnimationUsingKeyFrames da_form2 = new DoubleAnimationUsingKeyFrames();
            da_form2.KeyFrames.Add(new DiscreteDoubleKeyFrame(800, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_form2.KeyFrames.Add(new LinearDoubleKeyFrame(126, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7))));
            da_form2.KeyFrames.Add(new LinearDoubleKeyFrame(126, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.2))));
            da_form2.KeyFrames.Add(new LinearDoubleKeyFrame(-270, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.6))));
            //第一个字
            DoubleAnimationUsingKeyFrames da_font1h = new DoubleAnimationUsingKeyFrames();
            da_font1h.KeyFrames.Add(new DiscreteDoubleKeyFrame(300, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7))));
            da_font1h.KeyFrames.Add(new LinearDoubleKeyFrame(141, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            DoubleAnimationUsingKeyFrames da_font1w = new DoubleAnimationUsingKeyFrames();
            da_font1w.KeyFrames.Add(new DiscreteDoubleKeyFrame(274.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7))));
            da_font1w.KeyFrames.Add(new LinearDoubleKeyFrame(129, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            DoubleAnimationUsingKeyFrames da_font1l = new DoubleAnimationUsingKeyFrames();
            da_font1l.KeyFrames.Add(new DiscreteDoubleKeyFrame(42.75, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7))));
            da_font1l.KeyFrames.Add(new LinearDoubleKeyFrame(150.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            da_font1l.KeyFrames.Add(new LinearDoubleKeyFrame(150.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.2))));
            da_font1l.KeyFrames.Add(new LinearDoubleKeyFrame(-504.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.6))));
            DoubleAnimationUsingKeyFrames da_font1t = new DoubleAnimationUsingKeyFrames();
            da_font1t.KeyFrames.Add(new DiscreteDoubleKeyFrame(90, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7))));
            da_font1t.KeyFrames.Add(new LinearDoubleKeyFrame(169.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            //第二个字
            DoubleAnimationUsingKeyFrames da_font2h = new DoubleAnimationUsingKeyFrames();
            da_font2h.KeyFrames.Add(new DiscreteDoubleKeyFrame(300, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            da_font2h.KeyFrames.Add(new LinearDoubleKeyFrame(141, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.3))));
            DoubleAnimationUsingKeyFrames da_font2w = new DoubleAnimationUsingKeyFrames();
            da_font2w.KeyFrames.Add(new DiscreteDoubleKeyFrame(274.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            da_font2w.KeyFrames.Add(new LinearDoubleKeyFrame(129, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.3))));
            DoubleAnimationUsingKeyFrames da_font2l = new DoubleAnimationUsingKeyFrames();
            da_font2l.KeyFrames.Add(new DiscreteDoubleKeyFrame(262.75, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            da_font2l.KeyFrames.Add(new LinearDoubleKeyFrame(335.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.3))));
            da_font2l.KeyFrames.Add(new LinearDoubleKeyFrame(335.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.2))));
            da_font2l.KeyFrames.Add(new LinearDoubleKeyFrame(-319.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.6))));
            DoubleAnimationUsingKeyFrames da_font2t = new DoubleAnimationUsingKeyFrames();
            da_font2t.KeyFrames.Add(new DiscreteDoubleKeyFrame(90, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            da_font2t.KeyFrames.Add(new LinearDoubleKeyFrame(169.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.3))));
            //第三个字
            DoubleAnimationUsingKeyFrames da_font3h = new DoubleAnimationUsingKeyFrames();
            da_font3h.KeyFrames.Add(new DiscreteDoubleKeyFrame(300, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2))));
            da_font3h.KeyFrames.Add(new LinearDoubleKeyFrame(141, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.5))));
            DoubleAnimationUsingKeyFrames da_font3w = new DoubleAnimationUsingKeyFrames();
            da_font3w.KeyFrames.Add(new DiscreteDoubleKeyFrame(274.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2))));
            da_font3w.KeyFrames.Add(new LinearDoubleKeyFrame(129, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.5))));
            DoubleAnimationUsingKeyFrames da_font3l = new DoubleAnimationUsingKeyFrames();
            da_font3l.KeyFrames.Add(new DiscreteDoubleKeyFrame(482.75, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2))));
            da_font3l.KeyFrames.Add(new LinearDoubleKeyFrame(520.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.5))));
            da_font3l.KeyFrames.Add(new LinearDoubleKeyFrame(520.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.2))));
            da_font3l.KeyFrames.Add(new LinearDoubleKeyFrame(-134.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.6))));
            DoubleAnimationUsingKeyFrames da_font3t = new DoubleAnimationUsingKeyFrames();
            da_font3t.KeyFrames.Add(new DiscreteDoubleKeyFrame(90, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2))));
            da_font3t.KeyFrames.Add(new LinearDoubleKeyFrame(169.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.5))));
            //T优或T劣
            DoubleAnimationUsingKeyFrames da_opacity = new DoubleAnimationUsingKeyFrames();
            da_opacity.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.6))));
            da_opacity.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.9))));
            DoubleAnimationUsingKeyFrames da_opah = new DoubleAnimationUsingKeyFrames();
            da_opah.KeyFrames.Add(new DiscreteDoubleKeyFrame(134, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.6))));
            da_opah.KeyFrames.Add(new LinearDoubleKeyFrame(67, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.9))));
            DoubleAnimationUsingKeyFrames da_opaw = new DoubleAnimationUsingKeyFrames();
            da_opaw.KeyFrames.Add(new DiscreteDoubleKeyFrame(512, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.6))));
            da_opaw.KeyFrames.Add(new LinearDoubleKeyFrame(256, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.9))));
            DoubleAnimationUsingKeyFrames da_opal = new DoubleAnimationUsingKeyFrames();
            da_opal.KeyFrames.Add(new DiscreteDoubleKeyFrame(144, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.6))));
            da_opal.KeyFrames.Add(new LinearDoubleKeyFrame(272, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.9))));
            da_opal.KeyFrames.Add(new LinearDoubleKeyFrame(272, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.2))));
            da_opal.KeyFrames.Add(new LinearDoubleKeyFrame(-383, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.6))));
            DoubleAnimationUsingKeyFrames da_opat = new DoubleAnimationUsingKeyFrames();
            da_opat.KeyFrames.Add(new DiscreteDoubleKeyFrame(238, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.6))));
            da_opat.KeyFrames.Add(new LinearDoubleKeyFrame(271.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.9))));
            //加载动画
            RecB.BeginAnimation(HeightProperty, da_height);
            RecT.BeginAnimation(HeightProperty, da_height);
            RecB.BeginAnimation(Canvas.TopProperty, da_line1);
            RecT.BeginAnimation(Canvas.TopProperty, da_line2);

            EnemyForm.BeginAnimation(Canvas.LeftProperty,da_form1);
            TeamForm.BeginAnimation(Canvas.LeftProperty, da_form2);

            Font1.BeginAnimation(HeightProperty, da_font1h);
            Font1.BeginAnimation(WidthProperty, da_font1w);
            Font1.BeginAnimation(Canvas.LeftProperty, da_font1l);
            Font1.BeginAnimation(Canvas.TopProperty, da_font1t);

            Font2.BeginAnimation(HeightProperty, da_font2h);
            Font2.BeginAnimation(WidthProperty, da_font2w);
            Font2.BeginAnimation(Canvas.LeftProperty, da_font2l);
            Font2.BeginAnimation(Canvas.TopProperty, da_font2t);

            Font3.BeginAnimation(HeightProperty, da_font3h);
            Font3.BeginAnimation(WidthProperty, da_font3w);
            Font3.BeginAnimation(Canvas.LeftProperty, da_font3l);
            Font3.BeginAnimation(Canvas.TopProperty, da_font3t);

            FormGood.BeginAnimation(OpacityProperty, da_opacity);
            FormGood.BeginAnimation(HeightProperty, da_opah);
            FormGood.BeginAnimation(WidthProperty, da_opaw);
            FormGood.BeginAnimation(Canvas.LeftProperty, da_opal);
            FormGood.BeginAnimation(Canvas.TopProperty, da_opat);
        }
        #endregion
    }
}
