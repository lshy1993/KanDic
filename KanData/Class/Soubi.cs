using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanData
{
    public class Soubi
    {
        public int Number{ get; set; }
        public string Name{ get; set; }
        public string Content{ get; set; }
        public string Image{ get; set; }
        public string Icon{ get; set; }
        public string Rare{ get; set; }
        public string Type{ get; set; }

        public string CanEquip { get; set; }

        public int Power{ get; set; }
        public int Torpedo{ get; set; }
        public int Bomb{ get; set; }
        public int Air{ get; set; }
        public int Antisub{ get; set; }
        public int Search{ get; set; }
        public int Hitrate{ get; set; }
        public int Dodge { get; set; }
        public int Defence { get; set; }
        public string Range{ get; set; }

        public string Fuel{ get; set; }
        public string Ammo{ get; set; }
        public string Steel{ get; set; }
        public string Aluminium{ get; set; }

        public string Tuijian { get; set; }
        public string Mishu { get; set; }
        public string Remark { get; set; }

        public Soubi() { }
    }
}
