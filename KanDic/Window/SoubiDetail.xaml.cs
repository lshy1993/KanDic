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
using KanDic.Viewer;
using KanDic.Resources;
using MahApps.Metro.Controls;

namespace KanDic.Window
{
    /// <summary>
    /// SoubiDetail.xaml 的交互逻辑
    /// </summary>
    public partial class SoubiDetail
    {
        public Soubi equip = new Soubi();

        public SoubiDetail(int num)
        {
            InitializeComponent();
            equip = KanDic.Viewer.Equipment.equips[num];
            Detail_Init();
            MainDetail.DataContext = equip;
            this.Title = equip.Name;
        }

        public void Detail_Init()
        {
            equip.Shu1 = Set_Icon(equip.Shu1);
            equip.Shu2 = Set_Icon(equip.Shu2);
            equip.Shu3 = Set_Icon(equip.Shu3);
            equip.Shu4 = Set_Icon(equip.Shu4);
            equip.Shu5 = Set_Icon(equip.Shu5);
            equip.Shu6 = Set_Icon(equip.Shu6);
        }

        public string Set_Icon(string x)
        {
            switch (x)
            {
                case "火力":
                    return "/Cache/icon/info/hl.PNG";
                case "雷裝":
                    return "/Cache/icon/info/lz.PNG";
                case "爆裝":
                    return "/Cache/icon/info/bz.PNG";
                case "对空":
                    return "/Cache/icon/info/dk.PNG";
                case "对潜":
                    return "/Cache/icon/info/dq.PNG";
                case "索敌":
                    return "/Cache/icon/info/sd.PNG";
                case "命中":
                    return "/Cache/icon/info/mz.PNG";
                case "回避":
                    return "/Cache/icon/info/hb.PNG";
                case "射程":
                    return "/Cache/icon/info/sc.PNG";
                case "装甲":
                    return "/Cache/icon/info/zz.PNG";
                default:
                    return "/Cache/noicon.png";
            }

        }
    }
}
