using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanData
{
    public class Expedition
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Hard { set; get; }
        public string Content { set; get; }
        public int Hour { set; get; }
        public int Minute { set; get; }
        public int GetFuel { set; get; }
        public int GetAmmo { set; get; }
        public int GetSteel { set; get; }
        public int GetAluminum { set; get; }
        public string CostFuel { set; get; }
        public string CostAmmo { set; get; }
        public int Item1 { set; get; }
        public string ItemName1 { set; get; }
        public int Item2 { set; get; }
        public string ItemName2 { set; get; }
        public int Exp { set; get; }
        public int Lv { set; get; }
        public string Set { set; get; }
        public string Remark { set; get; }

        int a, b, c, d;

        public Expedition() { }

        public void GetMulty()
        {
            a = GetFuel;
            b = GetSteel;
            c = GetAmmo;
            d = GetAluminum;
            GetFuel = (int)(a * 1.5);
            GetSteel = (int)(b * 1.5);
            GetAmmo = (int)(c * 1.5);
            GetAluminum = (int)(d * 1.5);
        }

        public void RemoveMulty()
        {
            GetFuel = a;
            GetSteel = b;
            GetAmmo = c;
            GetAluminum = d;
        }
    }
}
