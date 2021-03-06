﻿using System;
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

namespace KanSimulator.Module
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
            EnterTextAnimation();
        }

        #region 0-动画-中央【假想遭遇】
        private void EnterTextAnimation()
        {
            //初始化
            CenterText.Source = new BitmapImage(new Uri("/Image/Battle/LineText_Encounter.png", UriKind.Relative));
            Canvas.SetTop(CenterText, 140);
            CenterBG.Source = new BitmapImage(new Uri("/Image/Battle/LineBG_Red.png", UriKind.Relative));
            //背景块
            DoubleAnimationUsingKeyFrames da_bg_h = new DoubleAnimationUsingKeyFrames();
            da_bg_h.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.25))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.55))));
            da_bg_h.Completed += (sender, e) => { SetFormationSelection(); };//下一动画
            DoubleAnimationUsingKeyFrames da_bg_top = new DoubleAnimationUsingKeyFrames();
            da_bg_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(240, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(140, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(140, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.25))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(240, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.55))));
            //字
            DoubleAnimationUsingKeyFrames da_text_left = new DoubleAnimationUsingKeyFrames();
            da_text_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(60, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(40, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(20, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.95))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.25))));
            DoubleAnimationUsingKeyFrames da_text_opa = new DoubleAnimationUsingKeyFrames();
            da_text_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.95))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.25))));

            //绑定动画
            CenterBG.BeginAnimation(HeightProperty, da_bg_h);
            CenterBG.BeginAnimation(Canvas.TopProperty, da_bg_top);

            CenterText.BeginAnimation(Canvas.LeftProperty, da_text_left);
            CenterText.BeginAnimation(OpacityProperty, da_text_opa);
        }
        #endregion

        #region 1-初始化-【选择阵型】
        private void SetFormationSelection()
        {
            FormSelectionCanvas.Visibility = Visibility.Visible;
            MainText.Text = "请选择阵型……";
            fs1.SetButton(0, teamnum);
            fs1.Opacity = 0;
            if (teamnum >= 4)
            {
                fs2.SetButton(1, teamnum);
                fs2.Opacity = 0;
            }
            if (teamnum >= 5)
            {
                fs3.SetButton(2, teamnum);
                fs3.Opacity = 0;
            }
            if (teamnum >= 4)
            {
                fs4.SetButton(3, teamnum);
                fs4.Opacity = 0;
            }
            if (teamnum >= 4)
            {
                fs5.SetButton(4, teamnum);
                fs5.Opacity = 0;
            }
            FormSelectionCanvas.Visibility = Visibility.Visible;
            FormationSelectionFadeIn();
        }
        #endregion

        #region 1-动画-【选择阵型】淡入
        private void FormationSelectionFadeIn()
        {
            DoubleAnimationUsingKeyFrames da = new DoubleAnimationUsingKeyFrames();
            da.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));

            DoubleAnimationUsingKeyFrames da_textbg_bottom = new DoubleAnimationUsingKeyFrames();
            da_textbg_bottom.KeyFrames.Add(new DiscreteDoubleKeyFrame(-93, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_textbg_bottom.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));

            DoubleAnimationUsingKeyFrames da_text_bottom = new DoubleAnimationUsingKeyFrames();
            da_text_bottom.KeyFrames.Add(new DiscreteDoubleKeyFrame(-88, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_text_bottom.KeyFrames.Add(new LinearDoubleKeyFrame(5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));

            fs1.BeginAnimation(OpacityProperty, da);
            fs2.BeginAnimation(OpacityProperty, da);
            fs3.BeginAnimation(OpacityProperty, da);
            fs4.BeginAnimation(OpacityProperty, da);
            fs5.BeginAnimation(OpacityProperty, da);
            TextBG.BeginAnimation(Canvas.BottomProperty, da_textbg_bottom);
            MainText.BeginAnimation(Canvas.BottomProperty,da_text_bottom);
        }
        #endregion

        #region 2-动画-中央【突入敌方海域】
        public void EnterAreaAnimation()
        {
            //隐藏阵型选择
            DoubleAnimationUsingKeyFrames da = new DoubleAnimationUsingKeyFrames();
            da.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.15))));
            da.Completed += (sender, e) => { FormSelectionCanvas.Visibility = Visibility.Collapsed; };
            FormSelectionCanvas.BeginAnimation(OpacityProperty, da);
            //初始化
            CenterText.Source = new BitmapImage(new Uri("/Image/Battle/LineText_EnterArea.png", UriKind.Relative));
            Canvas.SetTop(CenterText, 140);
            CenterBG.Source = new BitmapImage(new Uri("/Image/Battle/LineBG_Red.png", UriKind.Relative));
            //背景块
            DoubleAnimationUsingKeyFrames da_bg_h = new DoubleAnimationUsingKeyFrames();
            da_bg_h.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.25))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.55))));
            da_bg_h.Completed += (sender, e) => { SetShipList(); };//下一动画
            DoubleAnimationUsingKeyFrames da_bg_top = new DoubleAnimationUsingKeyFrames();
            da_bg_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(240, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(140, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(140, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.25))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(240, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.55))));
            //字
            DoubleAnimationUsingKeyFrames da_text_left = new DoubleAnimationUsingKeyFrames();
            da_text_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(-20, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(-40, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.95))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(-80, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.25))));
            DoubleAnimationUsingKeyFrames da_text_opa = new DoubleAnimationUsingKeyFrames();
            da_text_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.95))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.25))));

            //绑定动画
            CenterBG.BeginAnimation(HeightProperty, da_bg_h);
            CenterBG.BeginAnimation(Canvas.TopProperty, da_bg_top);

            CenterText.BeginAnimation(Canvas.LeftProperty, da_text_left);
            CenterText.BeginAnimation(OpacityProperty, da_text_opa);
        }
        #endregion

        //*3-【战斗开始】
        
        //↓4-【待完善】我方初始化

        #region 4-初始化-【我方列阵】
        private void SetShipList()
        {
            for (int i = 0; i < 6; i++)
            {
                ShipBox sb = new ShipBox("ljesxnctkmmf", i, false);
                this.RegisterName("ship" + i, sb);
                ShipCanvas.Children.Add(sb);
                Canvas.SetLeft(sb, -170);
                Canvas.SetTop(sb, 70 + i * 46);
            }
            ShipListAnimation();
        }
        #endregion

        #region 4-动画-【我方列阵】
        private void ShipListAnimation()
        {
            //船移动动画
            DoubleAnimationUsingKeyFrames da_ship1_left = new DoubleAnimationUsingKeyFrames();
            da_ship1_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(-170, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_ship1_left.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2))));
            da_ship1_left.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2))));
            DoubleAnimationUsingKeyFrames da_ship2_left = new DoubleAnimationUsingKeyFrames();
            da_ship2_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(-170, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.1))));
            da_ship2_left.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            DoubleAnimationUsingKeyFrames da_ship3_left = new DoubleAnimationUsingKeyFrames();
            da_ship3_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(-170, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2))));
            da_ship3_left.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4))));
            DoubleAnimationUsingKeyFrames da_ship4_left = new DoubleAnimationUsingKeyFrames();
            da_ship4_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(-170, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_ship4_left.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            DoubleAnimationUsingKeyFrames da_ship5_left = new DoubleAnimationUsingKeyFrames();
            da_ship5_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(-170, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4))));
            da_ship5_left.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            DoubleAnimationUsingKeyFrames da_ship6_left = new DoubleAnimationUsingKeyFrames();
            da_ship6_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(-170, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            da_ship6_left.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7))));
            //下一个动画
            da_ship1_left.Completed += (sender, e) => { SakutekiAnimation(); };
            //绑定动画
            ShipBox sb1 = this.FindName("ship0") as ShipBox;
            sb1.BeginAnimation(Canvas.LeftProperty, da_ship1_left);
            ShipBox sb2 = this.FindName("ship1") as ShipBox;
            if (sb2 != null) sb2.BeginAnimation(Canvas.LeftProperty, da_ship2_left);
            ShipBox sb3 = this.FindName("ship2") as ShipBox;
            if (sb3 != null) sb3.BeginAnimation(Canvas.LeftProperty, da_ship3_left);
            ShipBox sb4 = this.FindName("ship3") as ShipBox;
            if (sb4 != null) sb4.BeginAnimation(Canvas.LeftProperty, da_ship4_left);
            ShipBox sb5 = this.FindName("ship4") as ShipBox;
            if (sb5 != null) sb5.BeginAnimation(Canvas.LeftProperty, da_ship5_left);
            ShipBox sb6 = this.FindName("ship5") as ShipBox;
            if (sb6 != null) sb6.BeginAnimation(Canvas.LeftProperty, da_ship6_left);
        }
        #endregion

        #region 5-动画-中央【索敌开始】
        private void SakutekiAnimation()
        {
            //初始化
            CenterText.Source = new BitmapImage(new Uri("/Image/Battle/LineText_StartSakuteki.png", UriKind.Relative));
            Canvas.SetTop(CenterText, 180);
            CenterBG.Source = new BitmapImage(new Uri("/Image/Battle/LineBG_Green.png", UriKind.Relative));
            //阶段显示
            StageBar.SetStage(1);
            StageBar.FadeIn(1);
            //背景块
            DoubleAnimationUsingKeyFrames da_bg_h = new DoubleAnimationUsingKeyFrames();
            da_bg_h.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.75))));
            da_bg_h.Completed += (sender, e) => { SakutekiResultAnimation(); };
            DoubleAnimationUsingKeyFrames da_bg_top = new DoubleAnimationUsingKeyFrames();
            da_bg_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(240, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(140, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(140, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(240, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.75))));
            //字
            DoubleAnimationUsingKeyFrames da_text_left = new DoubleAnimationUsingKeyFrames();
            da_text_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(100, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(60, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(-60, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.95))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(-100, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
            DoubleAnimationUsingKeyFrames da_text_opa = new DoubleAnimationUsingKeyFrames();
            da_text_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.95))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
            //绑定动画
            CenterBG.BeginAnimation(HeightProperty, da_bg_h);
            CenterBG.BeginAnimation(Canvas.TopProperty, da_bg_top);

            CenterText.BeginAnimation(Canvas.LeftProperty, da_text_left);
            CenterText.BeginAnimation(OpacityProperty, da_text_opa);
        }
        #endregion

        //*6-【飞机索敌】

        //↓7-【待完善】索敌判定

        #region 7-动画-上方【索敌结果】
        private void SakutekiResultAnimation()
        {
            //初始化
            CenterSubText.Source = new BitmapImage(new Uri("/Image/Battle/LineText_Find.png", UriKind.Relative));
            double w = CenterSubText.ActualWidth / 2;
            //背景块
            DoubleAnimationUsingKeyFrames da_bg_h = new DoubleAnimationUsingKeyFrames();
            da_bg_h.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.75))));
            DoubleAnimationUsingKeyFrames da_bg_top = new DoubleAnimationUsingKeyFrames();
            da_bg_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(70, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(-30, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(-30, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(70, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.75))));
            da_bg_top.Completed += (sender, e) => { TekimiyuAnimation(); SetEnemyList(); };
            //字
            DoubleAnimationUsingKeyFrames da_text_left = new DoubleAnimationUsingKeyFrames();
            da_text_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(w - 311, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.35))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(w - 184, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.55))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(w - 123, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.3))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(w + 7, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.65))));
            DoubleAnimationUsingKeyFrames da_text_opa = new DoubleAnimationUsingKeyFrames();
            da_text_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.25))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.35))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.1))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
            //副字
            DoubleAnimationUsingKeyFrames da_sub_left = new DoubleAnimationUsingKeyFrames();
            da_sub_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.55))));
            da_sub_left.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
            da_sub_left.KeyFrames.Add(new LinearDoubleKeyFrame(-400, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.7))));
            DoubleAnimationUsingKeyFrames da_sub_opa = new DoubleAnimationUsingKeyFrames();
            da_sub_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_sub_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.55))));
            da_sub_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.35))));
            da_sub_opa.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.7))));
            //绑定动画
            CenterSubBG.BeginAnimation(HeightProperty, da_bg_h);
            CenterSubBG.BeginAnimation(Canvas.TopProperty, da_bg_top);

            CenterSubText.BeginAnimation(Canvas.LeftProperty, da_text_left);
            CenterSubText.BeginAnimation(OpacityProperty, da_text_opa);

            CenterSub.BeginAnimation(Canvas.LeftProperty, da_sub_left);
            CenterSub.BeginAnimation(OpacityProperty, da_sub_opa);
        }
        #endregion

        #region 8-动画-中央【发现敌舰队】
        private void TekimiyuAnimation()
        {
            //初始化
            CenterText.Source = new BitmapImage(new Uri("/Image/Battle/LineText_TekikantaiMiyu.png", UriKind.Relative));
            Canvas.SetTop(CenterText, 165);
            CenterBG.Source = new BitmapImage(new Uri("/Image/Battle/LineBG_Green.png", UriKind.Relative));
            double w = 400 - CenterText.ActualWidth / 2;
            //背景块
            DoubleAnimationUsingKeyFrames da_bg_h = new DoubleAnimationUsingKeyFrames();
            da_bg_h.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
            da_bg_h.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.75))));
            da_bg_h.Completed += (sender, e) => {  };
            DoubleAnimationUsingKeyFrames da_bg_top = new DoubleAnimationUsingKeyFrames();
            da_bg_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(240, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(140, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(140, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
            da_bg_top.KeyFrames.Add(new LinearDoubleKeyFrame(240, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.75))));
            //字
            DoubleAnimationUsingKeyFrames da_text_left = new DoubleAnimationUsingKeyFrames();
            da_text_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(w + 300, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(w + 260, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(w + 140, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.95))));
            da_text_left.KeyFrames.Add(new LinearDoubleKeyFrame(w + 100, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
            DoubleAnimationUsingKeyFrames da_text_opa = new DoubleAnimationUsingKeyFrames();
            da_text_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.95))));
            da_text_opa.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.45))));
            //绑定动画
            CenterBG.BeginAnimation(HeightProperty, da_bg_h);
            CenterBG.BeginAnimation(Canvas.TopProperty, da_bg_top);

            CenterText.BeginAnimation(Canvas.LeftProperty, da_text_left);
            CenterText.BeginAnimation(OpacityProperty, da_text_opa);
        }
        #endregion

        //↓8-【待完善】敌方初始化

        #region 8-初始化-【敌方列阵】
        private void SetEnemyList()
        {
            for (int i = 0; i < 6; i++)
            {
                ShipBox sb = new ShipBox("ljesxnctkmmf", i, true);
                this.RegisterName("enemy" + i, sb);
                sb.Opacity = 0;
                ShipCanvas.Children.Add(sb);
                Canvas.SetLeft(sb, 630);
                Canvas.SetTop(sb, 130 + i * 46);
            }
            EnemyListAnimation();
        }
        #endregion

        #region 8-动画-【敌方列阵】
        private void EnemyListAnimation()
        {
            DoubleAnimationUsingKeyFrames da_enemy_opa = new DoubleAnimationUsingKeyFrames();
            da_enemy_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_enemy_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_enemy_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.8))));
            //下一个动画
            da_enemy_opa.Completed += (sender, e) => { KoukusenAnimation(); };
            //绑定动画
            ShipBox sb1 = this.FindName("enemy0") as ShipBox;
            sb1.BeginAnimation(OpacityProperty, da_enemy_opa);
            ShipBox sb2 = this.FindName("enemy1") as ShipBox;
            if (sb2 != null) sb2.BeginAnimation(OpacityProperty, da_enemy_opa);
            ShipBox sb3 = this.FindName("enemy2") as ShipBox;
            if (sb3 != null) sb3.BeginAnimation(OpacityProperty, da_enemy_opa);
            ShipBox sb4 = this.FindName("enemy3") as ShipBox;
            if (sb4 != null) sb4.BeginAnimation(OpacityProperty, da_enemy_opa);
            ShipBox sb5 = this.FindName("enemy4") as ShipBox;
            if (sb5 != null) sb5.BeginAnimation(OpacityProperty, da_enemy_opa);
            ShipBox sb6 = this.FindName("enemy5") as ShipBox;
            if (sb6 != null) sb6.BeginAnimation(OpacityProperty, da_enemy_opa);
        }
        #endregion

        //*9-【航空战初始化】

        //↓9-【待完善】航空战动画

        #region 9-动画-【航空战】
        private void KoukusenAnimation(){
            //初始化
            //阶段显示
            StageBar.SetStage(2);
            StageBar.FadeIn(3);
        }
        #endregion
        
        //*10-【开幕雷击】

        #region 11-初始化-双方阵型
        private void SetFormation()
        {
            StartAnimationCanvas.Visibility = Visibility.Visible;
            Random ra = new Random(DateTime.Now.Millisecond);
            int status = ra.Next(65535) % 4;
            int formation = ra.Next(DateTime.Now.Second) % 5 + 1;
            int form = ra.Next(DateTime.Now.Second + 1117) % 5 + 1;
            int offset = 0;//T字战的文字偏移
            switch (status)
            {
                case 0://同航战
                    Font1.Source = new BitmapImage(new Uri("/Image/Battle/Font1-1.png", UriKind.Relative));
                    Font2.Source = new BitmapImage(new Uri("/Image/Battle/Font2-1.png", UriKind.Relative));
                    FormGood.Source = null;
                    break;
                case 1://返航战
                    Font1.Source = new BitmapImage(new Uri("/Image/Battle/Font1-2.png", UriKind.Relative));
                    Font2.Source = new BitmapImage(new Uri("/Image/Battle/Font2-1.png", UriKind.Relative));
                    FormGood.Source = null;
                    break;
                case 2://T优
                    Font1.Source = new BitmapImage(new Uri("/Image/Battle/Font1-3.png", UriKind.Relative));
                    Font2.Source = new BitmapImage(new Uri("/Image/Battle/Font2-2.png", UriKind.Relative));
                    FormGood.Source = new BitmapImage(new Uri("/Image/Battle/TGood.png", UriKind.Relative));
                    offset = -25;
                    break;
                case 3://T劣
                    Font1.Source = new BitmapImage(new Uri("/Image/Battle/Font1-3.png", UriKind.Relative));
                    Font2.Source = new BitmapImage(new Uri("/Image/Battle/Font2-2.png", UriKind.Relative));
                    FormGood.Source = new BitmapImage(new Uri("/Image/Battle/TBad.png", UriKind.Relative));
                    offset = -25;
                    break;
            }
            TeamForm.Source = new BitmapImage(new Uri("/Image/Battle/Formation" + formation + ".png", UriKind.Relative));
            EnemyForm.Source = new BitmapImage(new Uri("/Image/Battle/FormationEnemy" + form + ".png", UriKind.Relative));
            FormationAnimation(offset);
        }
        #endregion

        #region 11-动画-双方阵型
        public void FormationAnimation(int offset)
        {
            //背景黑块
            DoubleAnimationUsingKeyFrames da_height = new DoubleAnimationUsingKeyFrames();
            da_height.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_height.KeyFrames.Add(new LinearDoubleKeyFrame(111, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_height.KeyFrames.Add(new LinearDoubleKeyFrame(111, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.4))));
            da_height.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.6))));
            DoubleAnimationUsingKeyFrames da_line1 = new DoubleAnimationUsingKeyFrames();
            da_line1.KeyFrames.Add(new DiscreteDoubleKeyFrame(402, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_line1.KeyFrames.Add(new LinearDoubleKeyFrame(346.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_line1.KeyFrames.Add(new LinearDoubleKeyFrame(346.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.4))));
            da_line1.KeyFrames.Add(new LinearDoubleKeyFrame(402, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.6))));
            DoubleAnimationUsingKeyFrames da_line2 = new DoubleAnimationUsingKeyFrames();
            da_line2.KeyFrames.Add(new DiscreteDoubleKeyFrame(78, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_line2.KeyFrames.Add(new LinearDoubleKeyFrame(22.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_line2.KeyFrames.Add(new LinearDoubleKeyFrame(22.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.4))));
            da_line2.KeyFrames.Add(new LinearDoubleKeyFrame(78, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.6))));
            //阵型文字
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
            da_font1t.KeyFrames.Add(new DiscreteDoubleKeyFrame(90 + offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7))));
            da_font1t.KeyFrames.Add(new LinearDoubleKeyFrame(169.5 + offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
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
            da_font2t.KeyFrames.Add(new DiscreteDoubleKeyFrame(90 + offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            da_font2t.KeyFrames.Add(new LinearDoubleKeyFrame(169.5 + offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.3))));
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
            da_font3t.KeyFrames.Add(new DiscreteDoubleKeyFrame(90 + offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2))));
            da_font3t.KeyFrames.Add(new LinearDoubleKeyFrame(169.5 + offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.5))));
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

        //*12-【炮击战】

        #region 动画-出击！
        public void GoAnimation()
        {
            DoubleAnimationUsingKeyFrames da = new DoubleAnimationUsingKeyFrames();
            da.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.15))));
            da.Completed += (sender, e) => { FormSelectionCanvas.Visibility = Visibility.Collapsed; };
            FormSelectionCanvas.BeginAnimation(OpacityProperty, da);
            //背景黑
            DoubleAnimationUsingKeyFrames da_black_h = new DoubleAnimationUsingKeyFrames();
            da_black_h.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_black_h.KeyFrames.Add(new LinearDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            DoubleAnimationUsingKeyFrames da_black_top = new DoubleAnimationUsingKeyFrames();
            da_black_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(240, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_black_top.KeyFrames.Add(new LinearDoubleKeyFrame(140, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            //线条
            DoubleAnimationUsingKeyFrames da_bar_left = new DoubleAnimationUsingKeyFrames();
            da_bar_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(800, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_bar_left.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            DoubleAnimationUsingKeyFrames da_bar_top = new DoubleAnimationUsingKeyFrames();
            da_bar_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(197.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7))));
            da_bar_top.KeyFrames.Add(new LinearDoubleKeyFrame(211.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.75))));
            da_bar_top.KeyFrames.Add(new LinearDoubleKeyFrame(203.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));
            da_bar_top.KeyFrames.Add(new LinearDoubleKeyFrame(206.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.85))));
            //第一个【出击】
            DoubleAnimationUsingKeyFrames da_text1_h = new DoubleAnimationUsingKeyFrames();
            da_text1_h.KeyFrames.Add(new DiscreteDoubleKeyFrame(304.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text1_h.KeyFrames.Add(new LinearDoubleKeyFrame(193, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            DoubleAnimationUsingKeyFrames da_text1_w = new DoubleAnimationUsingKeyFrames();
            da_text1_w.KeyFrames.Add(new DiscreteDoubleKeyFrame(576, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text1_w.KeyFrames.Add(new LinearDoubleKeyFrame(365, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            DoubleAnimationUsingKeyFrames da_text1_opa = new DoubleAnimationUsingKeyFrames();
            da_text1_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text1_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            DoubleAnimationUsingKeyFrames da_text1_left = new DoubleAnimationUsingKeyFrames();
            da_text1_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(112, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text1_left.KeyFrames.Add(new LinearDoubleKeyFrame(217.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            DoubleAnimationUsingKeyFrames da_text1_top = new DoubleAnimationUsingKeyFrames();
            da_text1_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(87.75, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_text1_top.KeyFrames.Add(new LinearDoubleKeyFrame(143.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            //第二个【出击】
            DoubleAnimationUsingKeyFrames da_text2_h = new DoubleAnimationUsingKeyFrames();
            da_text2_h.KeyFrames.Add(new DiscreteDoubleKeyFrame(304.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            da_text2_h.KeyFrames.Add(new LinearDoubleKeyFrame(193, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));
            DoubleAnimationUsingKeyFrames da_text2_w = new DoubleAnimationUsingKeyFrames();
            da_text2_w.KeyFrames.Add(new DiscreteDoubleKeyFrame(576, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            da_text2_w.KeyFrames.Add(new LinearDoubleKeyFrame(365, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));
            DoubleAnimationUsingKeyFrames da_text2_opa = new DoubleAnimationUsingKeyFrames();
            da_text2_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            da_text2_opa.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));
            DoubleAnimationUsingKeyFrames da_text2_left = new DoubleAnimationUsingKeyFrames();
            da_text2_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(112, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            da_text2_left.KeyFrames.Add(new LinearDoubleKeyFrame(217.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));
            DoubleAnimationUsingKeyFrames da_text2_top = new DoubleAnimationUsingKeyFrames();
            da_text2_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(87.75, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            da_text2_top.KeyFrames.Add(new LinearDoubleKeyFrame(143.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));
            //整体淡出
            DoubleAnimationUsingKeyFrames da_fadeout = new DoubleAnimationUsingKeyFrames();
            da_fadeout.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_fadeout.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.8))));
            da_fadeout.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.2))));
            //下一个动画【中央线】
            da_fadeout.Completed += (sender, e) => { StageBar.SetStage(1); StageBar.FadeIn(1); };
            //绑定动画
            GoBlack.BeginAnimation(HeightProperty, da_black_h);
            GoBlack.BeginAnimation(Canvas.TopProperty, da_black_top);

            GoBar.BeginAnimation(Canvas.LeftProperty, da_bar_left);
            GoBar.BeginAnimation(Canvas.TopProperty, da_bar_top);

            GoText1.BeginAnimation(HeightProperty, da_text1_h);
            GoText1.BeginAnimation(WidthProperty, da_text1_w);
            GoText1.BeginAnimation(OpacityProperty, da_text1_opa);
            GoText1.BeginAnimation(Canvas.LeftProperty, da_text1_left);
            GoText1.BeginAnimation(Canvas.TopProperty, da_text1_top);

            GoText2.BeginAnimation(HeightProperty, da_text2_h);
            GoText2.BeginAnimation(WidthProperty, da_text2_w);
            GoText2.BeginAnimation(OpacityProperty, da_text2_opa);
            GoText2.BeginAnimation(Canvas.LeftProperty, da_text2_left);
            GoText2.BeginAnimation(Canvas.TopProperty, da_text2_top);

            GoAnimationCanvas.BeginAnimation(OpacityProperty, da_fadeout);
        }
        #endregion

        //以下为临时测试按钮
        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            string temp = bt.Tag.ToString();
            Random ra = new Random(DateTime.Now.Millisecond);
            if (temp == "1")
            {
                ShipBox sb = (ShipBox)this.FindName("ship1");
                sb.HitAnimation(ra.Next(999), false);
            }
            else
            {
                ShipBox sb = (ShipBox)this.FindName("enemy2");
                sb.HitAnimation(ra.Next(999), true);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
