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
using KanDic.Resources;

namespace KanDic.Plugins.Simulator
{
    /// <summary>
    /// Over.xaml 的交互逻辑
    /// </summary>
    public partial class Over : UserControl
    {
        public Keisanki calc;
        public int level;

        public Over(MoniKan[] example,int lv)
        {
            InitializeComponent();
            calc = new Keisanki(example);
            level = lv;
            Result_Init();
        }

        private void Result_Init()
        {
            int fivelevel = level;
            if (level % 5 != 0)
            {
                fivelevel = level + 5 - (level % 5);
            }
            sd1.Text = calc.GetOldSearch().ToString("F3");
            sd2.Text = (calc.GetNewSearch() - fivelevel * 0.61).ToString("F3");
            sd3.Text = (calc.GetSimpleSearch() - level * 0.4).ToString("F3");
            All.Text =  "等级合计：" + calc.GetLevelAll().ToString();
            Average.Text = "平均：" + calc.GetLevelAverage().ToString("F2");
        }
    }
}
