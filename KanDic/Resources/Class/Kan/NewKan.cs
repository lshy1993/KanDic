using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanDic.Resources
{
    public class NewKan
    {
        public string Number { get; set; }
        public string AlbumNum { get; set; }
        public string Name { get;set;}
        public string Hiragana { get;set;}
        public string RomaName { get;set;}
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
        public int Fuel { get; set; }
        public int Ammo { get; set; }

        public NewKan() { }

        public NewKan(Kan xx)
        {
            Number = xx.BasicInfo.Number;
            AlbumNum = xx.BasicInfo.AlbumNum;
            Name = xx.BasicInfo.Name;
            Hiragana = xx.BasicInfo.Hiragana;
            RomaName = xx.BasicInfo.RomaName;
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
            Fuel = xx.SupplyInfo.Fuel;
            Ammo = xx.SupplyInfo.Ammo;
        }
    }
}
