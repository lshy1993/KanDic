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
        public List<string> strlist = new List<string>();

        public SoubiDetail(int num)
        {
            InitializeComponent();
            equip = KanDic.Viewer.Equipment.equips[num];
            Detail_Init();
            MainDetail.DataContext = equip;
            DataBox.DataContext = strlist;
            this.Title = equip.Name;
        }

        public void Detail_Init()
        {
            if (equip.Power != 0)
            {
                strlist.Add("/Cache/icon/info/hl.PNG");
                strlist.Add(equip.Power.ToString());
            }
            if (equip.Torpedo != 0)
            {
                strlist.Add("/Cache/icon/info/lz.PNG");
                strlist.Add(equip.Torpedo.ToString());
            }
            if (equip.Bomb != 0)
            {
                strlist.Add("/Cache/icon/info/bz.PNG");
                strlist.Add(equip.Bomb.ToString());
            }
            if (equip.Air != 0)
            {
                strlist.Add("/Cache/icon/info/dk.PNG");
                strlist.Add(equip.Air.ToString());
            }
            if (equip.Antisub != 0)
            {
                strlist.Add("/Cache/icon/info/fq.PNG");
                strlist.Add(equip.Antisub.ToString());
            }
            if (equip.Search != 0)
            {
                strlist.Add("/Cache/icon/info/sd.PNG");
                strlist.Add(equip.Search.ToString());
            }
            if (equip.Hitrate != 0)
            {
                strlist.Add("/Cache/icon/info/mz.PNG");
                strlist.Add(equip.Hitrate.ToString());
            }
            if (equip.Dodge != 0)
            {
                strlist.Add("/Cache/icon/info/hb.PNG");
                strlist.Add(equip.Dodge.ToString());
            }
            if (equip.Range != null)
            {
                strlist.Add("/Cache/icon/info/sc.PNG");
                strlist.Add(equip.Range);
            }
        }
    }
}
