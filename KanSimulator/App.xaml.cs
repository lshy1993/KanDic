using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro;

namespace KanSimulator
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e){
            //获取当前主题
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            //设置主题
            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("Blue"),
                                        ThemeManager.GetAppTheme("BaseDark"));
        }
    }
}
