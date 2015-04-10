using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KanDic
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public System.Windows.Threading.DispatcherTimer App_Timer = new System.Windows.Threading.DispatcherTimer();
        public string[] ColorType111 = { "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
        public List<string> ColorType = new List<string>();
        public int ColorNumber;

        protected override void OnStartup(StartupEventArgs e)
        {
            // get the theme from the current application
            var theme = MahApps.Metro.ThemeManager.DetectAppStyle(Application.Current);

            string xx = ConfigurationManager.AppSettings["colors"];
            for (int i = 0; i < 23; i++)
            {
                if (xx[i] == 49) ColorType.Add(ColorType111[i]);
            }

            ColorNumber = 0;

            // now set the Green accent and dark theme
            MahApps.Metro.ThemeManager.ChangeAppStyle(Application.Current,
                                        MahApps.Metro.ThemeManager.GetAccent(ColorType[ColorNumber]),
                                        MahApps.Metro.ThemeManager.GetAppTheme("BaseDark"));

            int minute = Convert.ToInt32(ConfigurationManager.AppSettings["minute"]);
            int second = Convert.ToInt32(ConfigurationManager.AppSettings["second"]);
            if (minute != 0 && second != 0)
            {
                App_Timer.Interval = new TimeSpan(0, 0, minute, second, 0);
                App_Timer.Tick += new EventHandler(WindowColor_Change);
                App_Timer.Start();
            }

            base.OnStartup(e);
        }

        private void WindowColor_Change(object sender, EventArgs e)
        {
            ColorNumber++;
            if (ColorNumber >= ColorType.Count) ColorNumber = 0;
            MahApps.Metro.ThemeManager.ChangeAppStyle(Application.Current,
                                        MahApps.Metro.ThemeManager.GetAccent(ColorType[ColorNumber]),
                                        MahApps.Metro.ThemeManager.GetAppTheme("BaseDark"));
        }
        
    }
}
