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
    /// HitNumber.xaml 的交互逻辑
    /// </summary>
    public partial class HitNumber : UserControl
    {
        public HitNumber()
        {
            InitializeComponent();
        }

        public void HitAnimation(int x, bool iscri)
        {
            string str = x.ToString();
            //□□□□(4321)向前占位
            string url = x > 0 ? geturl(str[0] - 48, iscri) : "/Image/Battle/Num_miss.PNG";
            Num4.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            Num3.Source = str.Length < 2 ? null : new BitmapImage(new Uri(geturl(str[1] - 48, iscri), UriKind.Relative));
            Num2.Source = str.Length < 3 ? null : new BitmapImage(new Uri(geturl(str[2] - 48, iscri), UriKind.Relative));
            Num1.Source = str.Length < 4 ? null : new BitmapImage(new Uri(geturl(str[3] - 48, iscri), UriKind.Relative));
            Critical.Visibility = iscri ? Visibility.Visible : Visibility.Collapsed;
            int offset = iscri ? -14 : 0;//Critical时黄色数字的上偏移
            //■□□□
            DoubleAnimationUsingKeyFrames da_num4_top = new DoubleAnimationUsingKeyFrames();
            da_num4_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_num4_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset - 13.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2))));
            da_num4_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_num4_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset - 5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4))));
            da_num4_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            DoubleAnimationUsingKeyFrames da_num4_opa = new DoubleAnimationUsingKeyFrames();
            da_num4_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            da_num4_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            da_num4_opa.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2))));
            //□■□□
            DoubleAnimationUsingKeyFrames da_num3_top = new DoubleAnimationUsingKeyFrames();
            da_num3_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.1))));
            da_num3_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset - 13.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_num3_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4))));
            da_num3_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset - 5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            da_num3_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            DoubleAnimationUsingKeyFrames da_num3_opa = new DoubleAnimationUsingKeyFrames();
            da_num3_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.1))));
            da_num3_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            da_num3_opa.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2))));
            //□□■□
            DoubleAnimationUsingKeyFrames da_num2_top = new DoubleAnimationUsingKeyFrames();
            da_num2_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2))));
            da_num2_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset - 13.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4))));
            da_num2_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            da_num2_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset - 5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            da_num2_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7))));
            DoubleAnimationUsingKeyFrames da_num2_opa = new DoubleAnimationUsingKeyFrames();
            da_num2_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.17))));
            da_num2_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            da_num2_opa.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2))));
            //□□□■
            DoubleAnimationUsingKeyFrames da_num1_top = new DoubleAnimationUsingKeyFrames();
            da_num1_top.KeyFrames.Add(new DiscreteDoubleKeyFrame(offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            da_num1_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset - 13.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            da_num1_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
            da_num1_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset - 5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7))));
            da_num1_top.KeyFrames.Add(new LinearDoubleKeyFrame(offset, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));
            DoubleAnimationUsingKeyFrames da_num1_opa = new DoubleAnimationUsingKeyFrames();
            da_num1_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.23))));
            da_num1_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            da_num1_opa.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2))));
            //CriticalHit
            DoubleAnimationUsingKeyFrames da_cri_left = new DoubleAnimationUsingKeyFrames();
            da_cri_left.KeyFrames.Add(new DiscreteDoubleKeyFrame(-17.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.1))));
            da_cri_left.KeyFrames.Add(new LinearDoubleKeyFrame(-23.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.16))));
            da_cri_left.KeyFrames.Add(new LinearDoubleKeyFrame(-11.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.22))));
            da_cri_left.KeyFrames.Add(new LinearDoubleKeyFrame(-22.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.28))));
            da_cri_left.KeyFrames.Add(new LinearDoubleKeyFrame(-12.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.34))));
            da_cri_left.KeyFrames.Add(new LinearDoubleKeyFrame(-19.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4))));
            da_cri_left.KeyFrames.Add(new LinearDoubleKeyFrame(-15.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.46))));
            da_cri_left.KeyFrames.Add(new LinearDoubleKeyFrame(-18.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.52))));
            da_cri_left.KeyFrames.Add(new LinearDoubleKeyFrame(-16.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.64))));
            da_cri_left.KeyFrames.Add(new LinearDoubleKeyFrame(-17.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.7))));
            DoubleAnimationUsingKeyFrames da_cri_opa = new DoubleAnimationUsingKeyFrames();
            da_cri_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.1))));
            da_cri_opa.KeyFrames.Add(new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            da_cri_opa.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2))));
            //动画绑定
            Num1.BeginAnimation(Canvas.TopProperty, da_num1_top);
            Num1.BeginAnimation(OpacityProperty, da_num1_opa);

            Num2.BeginAnimation(Canvas.TopProperty, da_num2_top);
            Num2.BeginAnimation(OpacityProperty, da_num2_opa);

            Num3.BeginAnimation(Canvas.TopProperty, da_num3_top);
            Num3.BeginAnimation(OpacityProperty, da_num3_opa);

            Num4.BeginAnimation(Canvas.TopProperty, da_num4_top);
            Num4.BeginAnimation(OpacityProperty, da_num4_opa);

            Critical.BeginAnimation(Canvas.LeftProperty, da_cri_left);
            Critical.BeginAnimation(OpacityProperty, da_cri_opa);
        }

        private string geturl(int x, bool iscri)
        {
            string result = "/Image/Battle/";
            result += iscri ? "NumYellow_" : "NumRed_";
            result += x;
            result += ".png";
            return result;
        }
    }
}
