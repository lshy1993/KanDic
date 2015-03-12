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
        public string[] ColorType = { "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
        public int ColorNumber;

        protected override void OnStartup(StartupEventArgs e)
        {
            // get the theme from the current application
            var theme = MahApps.Metro.ThemeManager.DetectAppStyle(Application.Current);

            // now set the Green accent and dark theme
            MahApps.Metro.ThemeManager.ChangeAppStyle(Application.Current,
                                        MahApps.Metro.ThemeManager.GetAccent("Blue"),
                                        MahApps.Metro.ThemeManager.GetAppTheme("BaseDark"));

            ColorNumber = 2;

            App_Timer.Interval = new TimeSpan(0, 0, 5, 0, 0);
            App_Timer.Tick += new EventHandler(WindowColor_Change);
            App_Timer.Start();

            base.OnStartup(e);
        }

        private void WindowColor_Change(object sender, EventArgs e)
        {
            ColorNumber++;
            if (ColorNumber > 22) ColorNumber = 0;

            MahApps.Metro.ThemeManager.ChangeAppStyle(Application.Current,
                                        MahApps.Metro.ThemeManager.GetAccent(ColorType[ColorNumber]),
                                        MahApps.Metro.ThemeManager.GetAppTheme("BaseDark"));
        }
        
    }
}
