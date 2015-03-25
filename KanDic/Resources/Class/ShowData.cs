using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanDic.Resources
{
    class ShowData:BattleInfo
    {
        public ShowData(MoniKan xx)
        {
            this.HP = xx.HP;
            this.Defence = xx.Defence;
            this.Dodge = xx.Dodge + xx.soubi[1].Dodge + xx.soubi[2].Dodge + xx.soubi[3].Dodge + xx.soubi[4].Dodge;
            this.Carry = xx.Carry;
            this.Speed = xx.Speed;
            this.Range = xx.Range;
            this.Power = xx.Power + xx.soubi[1].Power + xx.soubi[2].Power + xx.soubi[3].Power + xx.soubi[4].Power;
            this.Torpedo = xx.Torpedo + xx.soubi[1].Torpedo + xx.soubi[2].Torpedo + xx.soubi[3].Torpedo + xx.soubi[4].Torpedo;
            this.Air = xx.Air + xx.soubi[1].Air + xx.soubi[2].Air + xx.soubi[3].Air + xx.soubi[4].Air;
            this.Antisub = xx.Antisub + xx.soubi[1].Antisub + xx.soubi[2].Antisub + xx.soubi[3].Antisub + xx.soubi[4].Antisub;
            this.Search = xx.Search + xx.soubi[1].Search + xx.soubi[2].Search + xx.soubi[3].Search + xx.soubi[4].Search;
        }
    }
}
