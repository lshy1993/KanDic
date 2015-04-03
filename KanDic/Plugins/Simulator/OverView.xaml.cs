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
    public partial class OverView : UserControl
    {
        public Keisanki calc;
        public MoniKan[] ships;
        public int level;
        public Calculator xx;

        public OverView(MoniKan[] example,int lv)
        {
            InitializeComponent();
            calc = new Keisanki(example);
            ships = example;
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
            sd1.Text = calc.OldSearch().ToString("F3");
            sd2.Text = calc.NewSearch(fivelevel).ToString("F3");
            sd3.Text = calc.SimpleSearch(level).ToString("F3");
            All.Text =  "等级合计：" + calc.GetLevelAll().ToString();
            Average.Text = "平均：" + calc.GetLevelAverage().ToString("F2");
            for (int i = 1; i <= 6; i++)
            {
                if (calc.IfExist(i))
                {
                    ResultPanel.Children.Add(new OverViewRow(calc,i));
                    Rectangle rec = new Rectangle();
                    rec.Height = 1;
                    rec.Fill = new SolidColorBrush(Colors.Gray);
                    ResultPanel.Children.Add(rec);
                }
            }
        }

        private void Formation_Changed(object sender, SelectionChangedEventArgs e)
        {
            //Calculator xx = (Calculator)Calculator.GetWindow(this);
            xx.formation = ((ComboBox)sender).SelectedIndex;
        }

        private void Status_Changed(object sender, SelectionChangedEventArgs e)
        {
            //Calculator xx = (Calculator)Calculator.GetWindow(this);
            xx.status = ((ComboBox)sender).SelectedIndex;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            xx = (Calculator)Calculator.GetWindow(this);
            Formation.SelectedIndex = xx.formation;
            Status.SelectedIndex = xx.status;
        }
    }
}
