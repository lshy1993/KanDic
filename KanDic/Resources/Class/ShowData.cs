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
            this.Dodge = xx.Dodge + xx.soubi1.Dodge + xx.soubi2.Dodge + xx.soubi3.Dodge + xx.soubi4.Dodge;
            this.Carry = xx.Carry;
            this.Speed = xx.Speed;
            this.Range = xx.Range;
            this.Power = xx.Power + xx.soubi1.Power + xx.soubi2.Power + xx.soubi3.Power + xx.soubi4.Power;
            this.Torpedo = xx.Torpedo + xx.soubi1.Torpedo + xx.soubi2.Torpedo + xx.soubi3.Torpedo + xx.soubi4.Torpedo;
            this.Air = xx.Air + xx.soubi1.Air + xx.soubi2.Air + xx.soubi3.Air + xx.soubi4.Air;
            this.Antisub = xx.Antisub + xx.soubi1.Antisub + xx.soubi2.Antisub + xx.soubi3.Antisub + xx.soubi4.Antisub;
            this.Search = xx.Search + xx.soubi1.Search + xx.soubi2.Search + xx.soubi3.Search + xx.soubi4.Search;
        }
    }
}
