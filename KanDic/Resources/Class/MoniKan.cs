using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanData;

namespace KanDic.Resources
{
    public class MoniKan
    {
        public string Name { get; set; }
        public string ImageNormal { get; set; }
        public string ImageSmall { get; set; }

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

        public int Fuel { get; set; }
        public int Ammo { get; set; }
        public int FuelAll { get; set; }
        public int AmmoAll { get; set; }

        public MoniKan() { }

        public MoniKan(Kan xx)
        {
            LV = 1;

            Type = xx.Type;

            Name = xx.Name;
            ImageNormal = xx.ImageNormal;
            ImageSmall = xx.ImageSmall;

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

            MinPower = Power;
            MinDefence = Defence;
            MinAir = Air;
            MinTorpedo = Torpedo;

            MaxPower = xx.MaxPower;
            MaxAir = xx.MaxAir;
            MaxDefence = xx.MaxDefence;
            MaxTorpedo = xx.MaxTorpedo;

            Fuel = xx.Fuel;
            Ammo = xx.Ammo;
            FuelAll = xx.MaxFuel;
            AmmoAll = xx.MaxAmmo;

            soubi = new Soubi[5];
            soubi[1] = new Soubi();
            soubi[2] = new Soubi();
            soubi[3] = new Soubi();
            soubi[4] = new Soubi();

            Carrys = new int[5];
            Carrys[1] = xx.Carry1;
            Carrys[2] = xx.Carry2;
            Carrys[3] = xx.Carry3;
            Carrys[4] = xx.Carry4;
        }
    }
}
