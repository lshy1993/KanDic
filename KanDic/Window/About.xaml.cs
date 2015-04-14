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
using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Configuration;

namespace KanDic
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : MetroWindow
    {
        public About()
        {
            InitializeComponent();
            appver.Text = "Version " + ConfigurationManager.AppSettings["appver"];
            dataver.Text = "V" + ConfigurationManager.AppSettings["dataver"];
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {

            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;

        }

        private void Mahapp_MLBD(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://mahapps.com/");
        }

        private void PLogo_MLBD(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://kancolle.aemedia.org");
        }
    }
}
