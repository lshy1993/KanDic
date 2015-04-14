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
using System.Configuration;

namespace KanDic
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : MetroWindow
    {
        public string colors;
        public bool autoupdate;
        public int minute, second;
        public string[] ColorType1 = { "Purple", "Red", "Green", "Blue", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
        public string[] ColorType = { "CC6459DF", "CCE51400", "CC60A917", "CC119EDA", "CCFA6800", "CCA4C400", "CC008A00", "CC00ABA9", "CC1BA1E2", "CC0050EF", "CC6A00FF", "CCAA00FF", "CCF472D0", "CCD80073", "CCA20025", "CCF0A30A", "CCFEDE06", "CC825A2C", "CC6D8764", "CC647687", "CC76608A", "CC87794E", "CCA0522D" };
        
        public Setting()
        {
            InitializeComponent();
            minute = 0; second = 0;
            Minute.Value = Convert.ToInt32(ConfigurationManager.AppSettings["minute"]);
            Second.Value = Convert.ToInt32(ConfigurationManager.AppSettings["second"]);
            CheckUpdate.IsChecked = ConfigurationManager.AppSettings["autoupdate"] == "True";
            colors = ConfigurationManager.AppSettings["colors"];
            for (int i = 0; i < 23; i++)
            {
                CheckBox cb = new CheckBox();
                SkinBox.Children.Add(cb);
                cb.Tag = i.ToString();
                cb.Click += cb_Click;
                cb.Background = new SolidColorBrush(getargb(ColorType[i]));
                cb.IsChecked = colors[i] == 49 ? true : false;
            }
        }

        private Color getargb(string x)
        {
            byte alpha = Convert.ToByte(x.Substring(0, 2), 16);
            byte red = Convert.ToByte(x.Substring(2, 2), 16);
            byte green = Convert.ToByte(x.Substring(4, 2), 16);
            byte blue = Convert.ToByte(x.Substring(6, 2), 16);
            return Color.FromArgb(alpha, red, green, blue);
        }

        private void cb_Click(object sender, RoutedEventArgs e)
        {
            CheckBox temp = sender as CheckBox;
            int x = Convert.ToInt32(temp.Tag);
            string y = "";
            for (int i = 0; i < colors.Length; i++)
            {
                if (i != x)
                {
                    y += colors[i];
                }
                else
                {
                    y += (bool)temp.IsChecked ? "1" : "0";
                }
                
            }
            colors = y;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            autoupdate = (bool)cb.IsChecked;
        }

        private void Minute_Changed(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            minute = (int)nud.Value;
        }
        private void Second_Changed(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            second = (int)nud.Value;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["autoupdate"].Value = autoupdate.ToString();
            cfa.AppSettings.Settings["colors"].Value = colors;
            cfa.AppSettings.Settings["second"].Value = second.ToString();
            cfa.AppSettings.Settings["minute"].Value = minute.ToString();
            cfa.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
