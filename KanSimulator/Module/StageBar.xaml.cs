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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KanSimulator.Module
{
    /// <summary>
    /// StageBar.xaml 的交互逻辑
    /// </summary>
    public partial class StageBar : UserControl
    {
        public StageBar()
        {
            InitializeComponent();
        }

        public void FadeIn(double x)
        {
            DoubleAnimationUsingKeyFrames da = new DoubleAnimationUsingKeyFrames();
            da.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            da.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5 + x))));
            if (x != 0) da.Completed += (sender, e) => { FadeOut(); };
            StageCanvas.BeginAnimation(OpacityProperty, da);
        }

        public void FadeOut()
        {
            DoubleAnimationUsingKeyFrames da = new DoubleAnimationUsingKeyFrames();
            da.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            StageCanvas.BeginAnimation(OpacityProperty, da);
        }

        public void SetStage(int x)
        {
            string url = "/Cache/battle/Stage_";
            string urlbg = "/Cache/battle/StageBar.png";
            string urlgear = "/Cache/battle/StageGear.png";
            switch (x)
            {
                case 0://夜战
                    url += "Yasen.png";
                    urlbg = "/Cache/battle/StageBar_Night.png";
                    urlgear = "/Cache/battle/StageGear_Night.png";
                    break;
                case 1://索敌
                    url += "Sakuteki.png";
                    break;
                case 2://航空战
                    url += "Koukusen.png";
                    break;
                case 3://炮击战
                    url += "Hougekisen.png";
                    break;
                case 4://雷击战
                    url += "Raigekisen.png";
                    break;
                case 5://脱离判定 
                    url += "Run.png";
                    break;
                case 6://战斗结果
                    url += "Result.png";
                    break;
                    //其他部分添加处
            }
            StageBG.Source = new BitmapImage(new Uri(urlbg, UriKind.Relative));
            StageGear.Source = new BitmapImage(new Uri(urlgear, UriKind.Relative));
            StageText.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            SpinAnimation();
        }

        private void SpinAnimation()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(4));
            da.From = 0;
            da.To = 360;
            da.RepeatBehavior = RepeatBehavior.Forever;
            RotateTransform.BeginAnimation(RotateTransform.AngleProperty, da);
        }
    }
}
