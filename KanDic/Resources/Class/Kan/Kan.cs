using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanDic.Resources;

namespace KanDic.Resources
{
    public class Kan
    {
        //基本信息系列
        public int Number { get; set; }//编号
        public string AlbumNum { get; set; }//图鉴编号
        public int Rare { get; set; }//稀有度
        public string Type { get; set; }//种类
        public string Series { get; set; }//系列
        public string SeriesNum { get; set; }//几号舰
        public string Name { get; set; }//名称
        public string Hiragana { get; set; }//平假名
        public string RomaName { get; set; }//罗马音
        public string FileName { get; set; }//对应swf文件
        public string ImageNormal { get; set; }//标准图
        public string ImageSmall { get; set; }//缩略图
        public string CV { get; set; }//声优
        public string Illustrator { get; set; }//画师

        //属性信息系列
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

        public int MaxHP { get; set; }
        public int MaxPower { get; set; }
        public int MaxDefence { get; set; }
        public int MaxTorpedo { get; set; }
        public int MaxAir { get; set; }
        public int MaxCarry { get; set; }
        public int MaxAntisub { get; set; }
        public string MaxSpeed { get; set; }
        public int MaxSearch { get; set; }
        public string MaxRange { get; set; }
        public int MaxLucky { get; set; }
        public int MaxDodge { get; set; }

        //建造信息系列
        public string OnlyHuge { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Formula { get; set; }
        public string DropPoint { get; set; }

        //初始装备信息系列
        public int Carry1 { get; set; }
        public int Carry2 { get; set; }
        public int Carry3 { get; set; }
        public int Carry4 { get; set; }
        public string Equip1 { get; set; }
        public string Equip2 { get; set; }
        public string Equip3 { get; set; }
        public string Equip4 { get; set; }

        //改造信息系列
        public int UpdateLevel { get; set; }
        public int CostAmmo { get; set; }
        public int CostSteel { get; set; }
        public int LinkNumber { get; set; }
        public bool IsFinal { get; set; }

        //分解信息系列
        public int ResolveFuel { set; get; }
        public int ResolveSteel { set; get; }
        public int ResolveAmmo { set; get; }
        public int ResolveAluminium { set; get; }

        //补给信息系列
        public int Fuel { get; set; }
        public int Ammo { get; set; }
        public int MaxFuel { get; set; }
        public int MaxAmmo { get; set; }

        //喂养信息系列
        public int PlusPower { get; set; }
        public int PlusTorpedo { get; set; }
        public int PlusAir { get; set; }
        public int PlusDefence { get; set; }
    }
}
