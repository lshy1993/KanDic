using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanData;

namespace KanDic.Resources
{
    public class NewKan
    {
        public int Number { get; set; }
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
        public bool IsFinal { get; set; }

        public NewKan() { }

        public NewKan(Kan xx, bool ifmax)
        {
            Number = xx.Number;
            AlbumNum = xx.AlbumNum;
            Name = xx.Name;
            Hiragana = xx.Hiragana;
            RomaName = xx.RomaName;
            IsFinal = xx.IsFinal;

            if (!ifmax)
            {
                HP = xx.HP;
                Power = xx.Power;
                Defence = xx.Defence;
                Torpedo = xx.Torpedo;
                Air = xx.Air;
                Carry = xx.Carry;
                Antisub = xx.Antisub;
                Speed = xx.Speed;
                Search = xx.Search;
                Range = xx.Range;
                Lucky = xx.Lucky;
                Dodge = xx.Dodge;
                Fuel = xx.Fuel;
                Ammo = xx.Ammo;
            }
            else
            {
                HP = xx.MaxHP;
                Power = xx.MaxPower;
                Defence = xx.MaxDefence;
                Torpedo = xx.MaxTorpedo;
                Air = xx.MaxAir;
                Carry = xx.MaxCarry;
                Antisub = xx.MaxAntisub;
                Speed = xx.MaxSpeed;
                Search = xx.MaxSearch;
                Range = xx.MaxRange;
                Lucky = xx.MaxLucky;
                Dodge = xx.MaxDodge;
                Fuel = xx.MaxFuel;
                Ammo = xx.MaxAmmo;
            }
        }
    }
}
