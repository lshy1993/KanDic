using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanSimulator.Class
{
    class ShowData
    {
        public int HP { get; set; }
        public int Power { get; set; }
        public int Defence { get; set; }
        public int Torpedo { get; set; }
        public int Air { get; set; }
        public int Carry { get; set; }
        public int Antisub { get; set; }
        public string Speed { get; set; }
        public int Search { get; set; }
        public string Range { get; set; }
        public int Lucky { get; set; }
        public int Dodge { get; set; }

        public ShowData(MoniKan xx)
        {
            HP = xx.HP;
            Defence = xx.Defence;
            Dodge = xx.Dodge + xx.soubi[1].Dodge + xx.soubi[2].Dodge + xx.soubi[3].Dodge + xx.soubi[4].Dodge;
            Carry = xx.Carry;
            Speed = xx.Speed;
            Range = xx.Range;
            Power = xx.Power + xx.soubi[1].Power + xx.soubi[2].Power + xx.soubi[3].Power + xx.soubi[4].Power;
            Torpedo = xx.Torpedo + xx.soubi[1].Torpedo + xx.soubi[2].Torpedo + xx.soubi[3].Torpedo + xx.soubi[4].Torpedo;
            Air = xx.Air + xx.soubi[1].Air + xx.soubi[2].Air + xx.soubi[3].Air + xx.soubi[4].Air;
            Antisub = xx.Antisub + xx.soubi[1].Antisub + xx.soubi[2].Antisub + xx.soubi[3].Antisub + xx.soubi[4].Antisub;
            Search = xx.Search + xx.soubi[1].Search + xx.soubi[2].Search + xx.soubi[3].Search + xx.soubi[4].Search;
        }
    }
}
