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
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : System.Windows.Window
    {
        public string nickname = null;

        public Register()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            nickname = TB.Text;
            if (string.IsNullOrEmpty(nickname))
            {
                OKB.IsEnabled = false;
            }
            else
            {
                OKB.IsEnabled = true;
            }
        }

        private void OKB_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
