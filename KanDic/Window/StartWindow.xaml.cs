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
using Microsoft.Win32;
using MahApps.Metro.Controls;

namespace KanDic
{
    /// <summary>
    /// StartWindow.xaml 的交互逻辑
    /// </summary>

    public partial class StartWindow : MetroWindow
    {

        public StartWindow()
        {
            InitializeComponent();

            WelcomePanel_Init();
            MainPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            MahApps.Metro.Controls.Tile temp1 = sender as MahApps.Metro.Controls.Tile;
            int tabnum = temp1.TiltFactor - 1;
            if (tabnum < 7)
            {
                this.TabCtrl.SelectedIndex = tabnum;
                this.WelcomPanel.Visibility = Visibility.Collapsed;
                this.MainPanel.Visibility = Visibility.Visible;
            }
            else
            {
                KanDic.Plugins.Calculator win = new KanDic.Plugins.Calculator();
                win.Show();
            }
        }

        private void Web_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://kancolle.aemedia.org/forum-45-1.html");
        }

        private void Tile_MouseEnter(object sender, MouseEventArgs e)
        {
            MahApps.Metro.Controls.Tile temp1 = sender as MahApps.Metro.Controls.Tile;
            ThicknessAnimationUsingKeyFrames ani = new ThicknessAnimationUsingKeyFrames();
            ani.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(-2, -2, 8, 8), TimeSpan.FromSeconds(0.05)));
            temp1.BeginAnimation(MahApps.Metro.Controls.Tile.MarginProperty, ani);
        }

        private void Tile_MouseLeave(object sender, MouseEventArgs e)
        {
            MahApps.Metro.Controls.Tile temp1 = sender as MahApps.Metro.Controls.Tile;
            ThicknessAnimationUsingKeyFrames ani = new ThicknessAnimationUsingKeyFrames();
            ani.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(4, 4, 4, 4), TimeSpan.FromSeconds(0.05)));
            temp1.BeginAnimation(MahApps.Metro.Controls.Tile.MarginProperty, ani);
        }

        private void MainPanel_Back(object sender, RoutedEventArgs e)
        {
            this.MainPanel.Visibility = Visibility.Collapsed;
            this.WelcomPanel.Visibility = Visibility.Visible;
        }

        private void WelcomePanel_Init()
        {
            /*foreach (MahApps.Metro.Controls.Tile element in WelcomPanel.Children)
            {
                element.MouseEnter += new System.Windows.Input.MouseEventHandler(Tile_MouseEnter);
                element.MouseLeave += new System.Windows.Input.MouseEventHandler(Tile_MouseLeave);
            }*/
        }

        private void KanColle_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.MainPanel.Visibility = Visibility.Collapsed;
            this.WelcomPanel.Visibility = Visibility.Visible;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            About ab = new About();
            ab.Owner = this;
            ab.ShowDialog();
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            Setting st = new Setting();
            st.Owner = this;
            st.ShowDialog();
        }

        private void Huang_Click(object sender, RoutedEventArgs e)
        {
            Almanac al = new Almanac();
            al.Owner = this;
            al.Show();
        }
    }
}
