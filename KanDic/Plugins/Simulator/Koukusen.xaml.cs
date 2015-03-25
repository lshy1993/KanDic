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
    /// Koukusen.xaml 的交互逻辑
    /// </summary>
    public partial class Koukusen : UserControl
    {
        public List<string> airforce = new List<string>() { "正規空母", "軽空母", "水上機母艦", "航空戦艦", "航空巡洋艦", "装甲空母" };
        public MoniKan[] example = new MoniKan[7];

        public Koukusen(MoniKan[] xx)
        {
            InitializeComponent();
            example = xx;
            grid_init();
        }

        private void grid_init()
        {
            for (int i = 1; i <= 6; i++)
            {
                if (example[i] == null || airforce.Find(x => x == example[i].Type) == null) { }
                else
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        string name;
                        double zhikong, huoli, damage1, damage2;
                        name = example[i].soubi[j].Name + "(" + example[i].Carrys[j] + ")";
                        switch (example[i].soubi[j].Type)
                        {
                            case "艦上戦闘機":
                                zhikong = (int)(example[i].soubi[j].Air * Math.Sqrt(example[i].Carrys[j]));
                                setgrid(i, j, name, "制空" + zhikong.ToString(), "b4ffba");
                                break;
                            case "艦上爆撃機":
                                huoli = (int)(example[i].soubi[j].Bomb * Math.Sqrt(example[i].Carrys[j]) + 25);
                                setgrid(i, j, name, "火力" + huoli.ToString(), "ffdddd");
                                break;
                            case "艦上攻撃機":
                                damage1 = (int)(0.8 * (example[i].soubi[j].Torpedo * Math.Sqrt(example[i].Carrys[j]) + 25));
                                damage2 = (int)(1.5 * (example[i].soubi[j].Torpedo * Math.Sqrt(example[i].Carrys[j]) + 25));
                                setgrid(i, j, name, "通常" + damage1.ToString() + "  " + "强击" + damage2.ToString(), "d1e2ff");
                                break;
                            case "艦上偵察機":
                                setgrid(i, j, "", name, "fff380");
                                break;
                        }
                    }
                }
            }
        }

        private void setgrid(int row, int colunm, string text1, string text2, string color)
        {
            Border bod = new Border();
            bod.BorderBrush = new SolidColorBrush(Colors.Gray);
            bod.BorderThickness = new Thickness(1);
            bod.Margin = new Thickness(1);
            Grid gg = new Grid();
            gg.RowDefinitions.Add(new RowDefinition());
            gg.RowDefinitions.Add(new RowDefinition());
            byte red = Convert.ToByte(color.Substring(0, 2), 16);
            byte green = Convert.ToByte(color.Substring(2, 2), 16);
            byte blue = Convert.ToByte(color.Substring(4, 2), 16);
            gg.Background = new SolidColorBrush(Color.FromRgb(red, green, blue));

            TextBlock tb = new TextBlock();
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.Foreground = new SolidColorBrush(Colors.Black);
            tb.Text = text1;
            gg.Children.Add(tb);

            tb = new TextBlock();
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.Foreground = new SolidColorBrush(Colors.Black);
            tb.Text = text2;
            tb.SetValue(Grid.RowProperty, 1);
            gg.Children.Add(tb);

            bod.Child = gg;
            bod.SetValue(Grid.ColumnProperty, colunm);
            bod.SetValue(Grid.RowProperty, row);
            MainGrid.Children.Add(bod);
        }

    }
}
