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
using Microsoft.Win32;
using System.Xml;
using KanDic.Resources;

namespace KanDic.Plugins
{
    /// <summary>
    /// MusicPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class MusicPlayer : UserControl
    {
        public System.Windows.Threading.DispatcherTimer BGM_Timer = new System.Windows.Threading.DispatcherTimer();

        public double SoundVolume;
        public bool IsStoped;
        public int CurrentNum;

        public VisualBrush PauseBrush = new VisualBrush();
        public VisualBrush PlayBrush = new VisualBrush();
        public VisualBrush SoundBrush = new VisualBrush();
        public VisualBrush MuteBrush = new VisualBrush();

        public XmlDocument BGMList = new XmlDocument();
        public XmlNode MP3Info;

        public MP3[] BGMS;

        public MusicPlayer()
        {
            InitializeComponent();

            PlayBrush_Init();
            PauseBrush_Init();
            SoundBrush_Init();
            MuteBrush_Init();

            Load_List();
            CurrentNum = 0;
            
            BGM_Player.Source = new Uri(Get_Path(CurrentNum),UriKind.Relative);

            BGM_Player.Play();
            BGM_Seeker.AddHandler(Slider.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(Timer_Pause), true);
            BGM_Seeker.AddHandler(Slider.MouseLeftButtonUpEvent, new MouseButtonEventHandler(BGM_Locate), true);
        }

        private void Load_List()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanDic.Resources.Data.Music.xml");
            BGMList.Load(sStream);
            MP3Info = BGMList.DocumentElement;
            BGMS = new MP3[MP3Info.ChildNodes.Count];
            foreach (XmlNode temp in MP3Info.ChildNodes)
            {
                int x = System.Int32.Parse(temp.FirstChild.InnerText);
                string y, z, a;
                y = temp.FirstChild.NextSibling.InnerText;
                z = temp.FirstChild.NextSibling.NextSibling.InnerText;
                a = temp.FirstChild.NextSibling.NextSibling.NextSibling.InnerText;
                BGMS[x - 1] = new MP3(x, y, z, a);
            }
        }

        private string Get_Path(int x)
        {
            return "Cache/bgm/" + BGMS[x].filename + ".mp3";
        }

        private void BGM_Opened(object sender, RoutedEventArgs e)
        {
            BGM_Seeker.Maximum = BGM_Player.NaturalDuration.TimeSpan.TotalMilliseconds;
            BGM_Name.Text = BGMS[CurrentNum].name;
            BGM_Timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            BGM_Timer.Tick += new EventHandler(Seeker_Locate);
            BGM_Timer.Start();
            IsStoped = false;
        }

        private void BGM_Ended(object sender, RoutedEventArgs e)
        {
            BGM_Timer.Stop();
            CurrentNum++;
            if (CurrentNum > 66) CurrentNum = 0;
            BGM_Seeker.Value = 0;
            BGM_Player.Position = TimeSpan.Zero;
            if (System.IO.File.Exists(Get_Path(CurrentNum)))
            { 
                BGM_Player.Source = new Uri(Get_Path(CurrentNum), UriKind.Relative);
            }
            else
            {
                BGM_Ended(sender,e);
            }
            BGM_Player.Play();
            BGM_Timer.Start();
        }

        private void BGM_Pause(object sender, RoutedEventArgs e)
        {
            if (IsStoped)
            {
                this.BGM_Status.Background = PauseBrush;
                BGM_Timer.Start();
                BGM_Player.Play();
                IsStoped = false;
            }
            else
            {
                this.BGM_Status.Background = PlayBrush;
                BGM_Timer.Stop();
                BGM_Player.Pause();
                IsStoped = true;
            }
        }

        private void BGM_Select(object sender, RoutedEventArgs e)
        {
        }

        private void BGM_Self(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "音乐文件|*.mp3";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "mp3";
            if ((bool)openFileDialog.ShowDialog().GetValueOrDefault())
            {
                string fileName = openFileDialog.FileName;
                BGM_Player.Source = new Uri(fileName);
                BGM_Player.Play();
            }
        }

        private void BGM_Locate(object sender, RoutedEventArgs e)
        {
            int SliderValue = (int)BGM_Seeker.Value;
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            BGM_Player.Position = ts;
            BGM_Timer.Start();
        }

        private void Seeker_Locate(object sender, EventArgs e)
        {
            BGM_Time.Text = BGM_Player.Position.ToString(@"mm\:ss");
            BGM_Seeker.Value = BGM_Player.Position.TotalMilliseconds;
        }

        private void Info_Update(object sender, EventArgs e)
        {
            BGM_Time.Text = new TimeSpan(0, 0, 0, 0, (int)BGM_Seeker.Value).ToString(@"mm\:ss");
        }

        private void Timer_Pause(object sender, EventArgs e)
        {
            BGM_Timer.Stop();
        }

        private void Volume_Changed(object sender, RoutedEventArgs e)
        {
            BGM_Player.Volume = (double)Volume_Slider.Value / 100;
            if (BGM_Player.Volume == 0)
            {
                this.Sound_Status.Background = MuteBrush;
            }
            else
            {
                this.Sound_Status.Background = SoundBrush;
            }
        }

        private void Volume_Mute(object sender, RoutedEventArgs e)
        {
            if (BGM_Player.Volume == 0)
            {
                this.Sound_Status.Background = SoundBrush;
                BGM_Player.Volume = SoundVolume;
                Volume_Slider.Value = SoundVolume * 100;
            }
            else
            {
                this.Sound_Status.Background = MuteBrush;
                SoundVolume = (double)Volume_Slider.Value / 100;
                BGM_Player.Volume = 0;
                Volume_Slider.Value = 0;
            }
        }

        private void PauseBrush_Init()
        {
            TransformGroup trfg = new TransformGroup();
            ScaleTransform sctr = new ScaleTransform();
            TranslateTransform tstr = new TranslateTransform();

            PauseBrush.Visual = (Canvas)this.TryFindResource("appbar_control_pause");
            PauseBrush.Stretch = Stretch.Uniform;
            sctr.ScaleX = 0.5;
            sctr.ScaleY = 0.5;
            trfg.Children.Add(sctr);
            tstr.X = 0.25;
            tstr.Y = 0.25;
            trfg.Children.Add(tstr);
            PauseBrush.RelativeTransform = trfg;
        }

        private void PlayBrush_Init()
        {
            TransformGroup trfg = new TransformGroup();
            ScaleTransform sctr = new ScaleTransform();
            TranslateTransform tstr = new TranslateTransform();
            PlayBrush.Visual = (Canvas)this.TryFindResource("appbar_control_play");
            PlayBrush.Stretch = Stretch.Uniform;
            sctr.ScaleX = 0.5;
            sctr.ScaleY = 0.5;
            trfg.Children.Add(sctr);
            tstr.X = 0.25;
            tstr.Y = 0.25;
            trfg.Children.Add(tstr);
            PlayBrush.RelativeTransform = trfg;
        }

        private void SoundBrush_Init()
        {
            TransformGroup trfg = new TransformGroup();
            ScaleTransform sctr = new ScaleTransform();
            TranslateTransform tstr = new TranslateTransform();
            SoundBrush.Visual = (Canvas)this.TryFindResource("appbar_sound_3");
            SoundBrush.Stretch = Stretch.Fill;
            sctr.ScaleX = 0.5;
            sctr.ScaleY = 0.5;
            trfg.Children.Add(sctr);
            tstr.X = 0.25;
            tstr.Y = 0.25;
            trfg.Children.Add(tstr);
            SoundBrush.RelativeTransform = trfg;
        }

        private void MuteBrush_Init()
        {
            TransformGroup trfg = new TransformGroup();
            ScaleTransform sctr = new ScaleTransform();
            TranslateTransform tstr = new TranslateTransform();
            MuteBrush.Visual = (Canvas)this.TryFindResource("appbar_sound_mute");
            MuteBrush.Stretch = Stretch.Fill;
            sctr.ScaleX = 0.5;
            sctr.ScaleY = 0.5;
            trfg.Children.Add(sctr);
            tstr.X = 0.25;
            tstr.Y = 0.25;
            trfg.Children.Add(tstr);
            MuteBrush.RelativeTransform = trfg;
        }
    }
}
