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

namespace KanDic
{
    /// <summary>
    /// Confirm.xaml 的交互逻辑
    /// </summary>
    public partial class Confirm : System.Windows.Window
    {
        public Confirm(string x)
        {
            InitializeComponent();
            maintext.Text = "【提督方略】已更新！(v" + x + ")";
        }

        private void Yun_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            System.Diagnostics.Process.Start("http://pan.baidu.com/share/home?uk=1610972107");
            Application.Current.Shutdown();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            Info.FileName = "KanUpdate.exe";
            Info.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Process.Start(Info);
            Application.Current.Shutdown();
        }
    }
}
