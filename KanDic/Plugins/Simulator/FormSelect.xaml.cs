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

namespace KanDic.Plugins.Simulator
{
    /// <summary>
    /// FormSelect.xaml 的交互逻辑
    /// </summary>
    public partial class FormSelect : UserControl
    {
        public FormSelect()
        {
            InitializeComponent();
        }

        public void SetButton(int formation, int teamnum)
        {
            Change_Button(formation);
            FormationPoint fp = new FormationPoint(formation, teamnum, false);
            MainCanvas.Children.Add(fp);
        }

        private void Change_Button(int x)
        {
            switch (x)
            {
                case 0:
                    button.SetResourceReference(Button.StyleProperty, "formation1");
                    break;
                case 1:
                    button.SetResourceReference(Button.StyleProperty, "formation2");
                    break;
                case 2:
                    button.SetResourceReference(Button.StyleProperty, "formation3");
                    break;
                case 3:
                    button.SetResourceReference(Button.StyleProperty, "formation4");
                    break;
                case 4:
                    button.SetResourceReference(Button.StyleProperty, "formation5");
                    break;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            BattleMain bt = (BattleMain)BattleMain.GetWindow(this);
            bt.GoAnimation();
        }
    }
}
