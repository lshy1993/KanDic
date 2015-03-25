using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanDic.Resources
{
    public class MoniKan
    {
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string IconName { get; set; }

        public string Type { get; set; }

        public int LV { get; set; }

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

        public int MinPower { get; set; }
        public int MinDefence { get; set; }
        public int MinTorpedo { get; set; }
        public int MinAir { get; set; }

        public Soubi[] soubi { get; set; }

        public int[] Carrys { get; set; }

        public int MaxPower { get; set; }
        public int MaxDefence { get; set; }
        public int MaxTorpedo { get; set; }
        public int MaxAir { get; set; }

        public MoniKan() { }

        public MoniKan(Kan xx)
        {
            LV = 1;

            Type = xx.BasicInfo.Type;

            Name = xx.BasicInfo.Name;
            ImageName = xx.BasicInfo.FileName;

            HP = xx.BattleInfo.HP;
            Power = xx.BattleInfo.Power;
            Defence = xx.BattleInfo.Defence;
            Torpedo = xx.BattleInfo.Torpedo;
            Air = xx.BattleInfo.Air;
            Carry = xx.BattleInfo.Carry;
            Antisub = xx.BattleInfo.Antisub;
            Speed = xx.BattleInfo.Speed;
            Search = xx.BattleInfo.Search;
            Range = xx.BattleInfo.Range;
            Lucky = xx.BattleInfo.Lucky;
            Dodge = xx.BattleInfo.Dodge;

            MinPower = Power;
            MinDefence = Defence;
            MinAir = Air;
            MinTorpedo = Torpedo;

            MaxPower = xx.MaxInfo.Power;
            MaxAir = xx.MaxInfo.Air;
            MaxDefence = xx.MaxInfo.Defence;
            MaxTorpedo = xx.MaxInfo.Torpedo;

            soubi = new Soubi[5];
            soubi[1] = new Soubi();
            soubi[2] = new Soubi();
            soubi[3] = new Soubi();
            soubi[4] = new Soubi();

            Carrys = new int[5];
            Carrys[1] = Convert.ToInt32(xx.EquipInfo.Carry1);
            Carrys[2] = Convert.ToInt32(xx.EquipInfo.Carry2);
            Carrys[3] = Convert.ToInt32(xx.EquipInfo.Carry3);
            Carrys[4] = Convert.ToInt32(xx.EquipInfo.Carry4);
        }
    }
}
