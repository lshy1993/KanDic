using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using log4net;
using MahApps.Metro;

namespace KanDic
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public DispatcherTimer App_Timer = new DispatcherTimer();
        //Mahapp.Metro提供的颜色库
        public string[] ColorType111 = {"Purple", "Red", "Green", "Blue", "Orange", "Lime", "Emerald",
                                           "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink",
                                           "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive",
                                           "Steel", "Mauve", "Taupe", "Sienna" };
        public List<string> ColorType = new List<string>();
        public int ColorNumber;
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnStartup(StartupEventArgs e)
        {
            //获取当前主题
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            //设置变色编号
            string xx = ConfigurationManager.AppSettings["colors"];
            for (int i = 0; i < 23; i++)
            {
                if (xx[i] == 49) ColorType.Add(ColorType111[i]);
            }
            ColorNumber = 0;
            //获取设置的变色时间间隔
            int minute = Convert.ToInt32(ConfigurationManager.AppSettings["minute"]);
            int second = Convert.ToInt32(ConfigurationManager.AppSettings["second"]);
            if (minute != 0 && second != 0)
            {
                App_Timer.Interval = new TimeSpan(0, 0, minute, second, 0);
                App_Timer.Tick += new EventHandler(WindowColor_Change);
                App_Timer.Start();
            }
            //设置主题，Dark或White版
            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent(ColorType[ColorNumber]),
                                        ThemeManager.GetAppTheme("BaseDark"));
            //日志开始记录
            log4net.Config.XmlConfigurator.Configure();
            base.OnStartup(e);
            log.Info("==软件开启=====================>>>");
        }

        private void WindowColor_Change(object sender, EventArgs e)
        {
            ColorNumber++;
            if (ColorNumber >= ColorType.Count) ColorNumber = 0;
            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent(ColorType[ColorNumber]),
                                        ThemeManager.GetAppTheme("BaseDark"));
        }

        protected override void OnExit(ExitEventArgs e)
        {
            log.Info("<<<========================End==");
            base.OnExit(e);
        }
        
    }
}
