using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanDic.Resources
{
    public class Keisanki
    {
        public MoniKan[] example { get; set; }
        int counter, lvall, search;
        double sdold, sdnew, sdsimple;
        double[,] capformation = new double[5, 4] { { 1, 0.45, 1, 1 }, { 0.8, 0.6, 0.8, 1 }, { 0.7, 0.9, 0.7, 1 }, { 0.6, 0.75, 0.6, 1 }, { 0.6, 1, 0.6, 1 } };
        double[] capstatus = new double[4] { 1, 0.8, 1.2, 0.6 };
        List<string> airforce = new List<string>() { "正規空母", "軽空母", "水上機母艦", "航空戦艦", "航空巡洋艦", "装甲空母" };
        List<string> hastorpedo = new List<string>() { "重雷装巡洋艦", "水上機母艦", "潜水艦", "潜水空母" };
        List<string> antisub = new List<string>() { "駆逐艦", "軽巡洋艦", "重雷装巡洋艦" };
        List<string> mainarti = new List<string>() { "小口径主砲", "中口径主砲", "大口径主砲" };
        List<string> artillery = new List<string>() { "小口径主砲", "中口径主砲", "大口径主砲", "副砲", "対艦強化弾", "主炮类" };
        
        public Keisanki(MoniKan[] xx)
        {
            example = xx;
            data_init();
        }

        #region 初始化统计数据
        /// <summary>
        /// 遍历所有船只，每只船只的装备，进行统计计算
        /// </summary>
        private void data_init()
        {
            counter = 0;
            lvall = 0;
            search = 0;
            sdold = 0;
            sdnew = 0;
            sdsimple = 0;
            for (int i = 1; i <= 6; i++)
            {
                if (example[i] != null)
                {
                    search += example[i].Search;
                    for (int j = 1; j <= 4; j++)
                    {
                        switch (example[i].soubi[j].Type)
                        {
                            case "艦上爆撃機":
                                sdnew += example[i].soubi[j].Search * 1.04;
                                sdsimple += example[i].soubi[j].Search * 0.6;
                                break;
                            case "艦上攻撃機":
                                sdnew += example[i].soubi[j].Search * 1.37;
                                sdsimple += example[i].soubi[j].Search * 0.8;
                                break;
                            case "艦上偵察機":
                                sdnew += example[i].soubi[j].Search * 1.66;
                                sdsimple += example[i].soubi[j].Search * 1.0;
                                sdold += example[i].soubi[j].Search * 2;
                                break;
                            case "水上偵察機":
                                sdnew += example[i].soubi[j].Search * 2.00;
                                sdsimple += example[i].soubi[j].Search * 1.2;
                                sdold += example[i].soubi[j].Search * 2;
                                break;
                            case "水上爆撃機":
                                sdnew += example[i].soubi[j].Search * 1.78;
                                sdsimple += example[i].soubi[j].Search * 1.0;
                                break;
                            case "小型電探":
                                sdnew += example[i].soubi[j].Search * 1.00;
                                sdsimple += example[i].soubi[j].Search * 0.6;
                                sdold += example[i].soubi[j].Search;
                                break;
                            case "大型電探":
                                sdnew += example[i].soubi[j].Search * 0.99;
                                sdsimple += example[i].soubi[j].Search * 0.6;
                                sdold += example[i].soubi[j].Search;
                                break;
                            case "探照灯":
                                sdnew += example[i].soubi[j].Search * 0.91;
                                sdsimple += example[i].soubi[j].Search * 0.5;
                                break;
                        }
                    }
                    lvall += example[i].LV;
                    counter++;
                }
            }
        }
        #endregion

        //获取monikan类
        public MoniKan GetShip(int i)
        {
            return example[i];
        }

        #region 统计类方法
        /// <summary>
        /// 简单统计类方法
        /// </summary>
        //计算等级加和
        public int GetLevelAll()
        {
            return lvall;
        }
        //计算等级平均值
        public double GetLevelAverage()
        {
            if (counter == 0)
                return 0;
            else
                return lvall / counter;
        }
        //获取火力总数
        public int GetPower(int i)
        {
            int sum = 0;
            sum += example[i].Power;
            for (int j = 1; j <= 4; j++)
            {
                sum += example[i].soubi[j].Power;
            }
            return sum;
        }
        //获取雷装总数
        public int GetTorpedo(int i)
        {
            int sum = 0;
            sum += example[i].Torpedo;
            for (int j = 1; j <= 4; j++)
            {
                sum += example[i].soubi[j].Torpedo;
            }
            return sum;
        }
        //获取爆装总数
        public int GetBomb(int i)
        {
            int sum = 0;
            for (int j = 1; j <= 4; j++)
            {
                sum += example[i].soubi[j].Bomb;
            }
            return sum;
        }
        //获取对潜总数
        public int GetAntisub(int i)
        {
            int sum = 0;
            for (int j = 1; j <= 4; j++)
            {
                sum += example[i].soubi[j].Antisub;
            }
            return sum;
        }
        //获取命中补正
        public int GetHit(int i)
        {
            int sum = 0;
            for (int j = 1; j <= 4; j++)
            {
                sum += example[i].soubi[j].Hitrate;
            }
            return sum;
        }
        //获取弹着种类
        public string GetCIType(int i)
        {
            int zhu = 0;
            int fu = 0;
            int che = 0;
            int tan = 0;
            for (int j = 1; j <= 4; j++)
            {
                switch (example[i].soubi[j].Type)
                {
                    case "小口径主砲":
                        zhu++;
                        break;
                    case "中口径主砲":
                        zhu++;
                        break;
                    case "大口径主砲":
                        zhu++;
                        break;
                    case "副砲":
                        fu++;
                        break;
                    case "対艦強化弾":
                        che++;
                        break;
                    case "小型電探":
                        tan++;
                        break;
                    case "大型電探":
                        tan++;
                        break;
                }
            }
            if (zhu == 2 && che == 1) return "主砲 + 主砲";
            else if (che == 1) return "主砲 + 徹甲彈";
            else if (tan == 1) return "主砲 + 電探";
            else return "主砲 + 副砲";
        }
        #endregion

        #region 计算类方法
        /// <summary>
        /// 涉及数值复杂计算，不包括单纯统计加和
        /// </summary>
        //三种不同索敌的计算方式
        public double OldSearch()
        {
            return sdold + Math.Sqrt(search);
        }
        public double NewSearch(int level)
        {
            return sdnew + Math.Sqrt(search) - level * 0.61;
        }
        public double SimpleSearch(int level)
        {
            return sdsimple + Math.Sqrt(search) - level * 0.4;
        }
        //计算所需经验值
        public int GetNextExp(int i)
        {
            if (example[i].LV <= 50) return 100 * example[i].LV;
            else if (example[i].LV <= 70) return 5000 + 200 * (example[i].LV - 50);
            else if (example[i].LV <= 80) return 10000 + 400 * (example[i].LV - 70);
            else if (example[i].LV <= 90) return 14000 + 500 * (example[i].LV - 80);
            else if (example[i].LV <= 100) return 14000 + 500 * (example[i].LV - 80);
            else return 0;
        }
        //回避
        public double MissRate(int i)
        {
            return example[i].Dodge / (example[i].Dodge + 37.5);
        }
        //命中
        public double HitRate(int i,int level){
            return 1 + Math.Sqrt((level - 1) * 5) / 100 + GetHit(i) / 100;
        }
        //基本攻击力(雷装)
        public int BasicDamageT(int i)
        {
            return GetTorpedo(i) + 5;
        }
        //基本攻击力(火力)(非空母)
        public int BasicDamageP(int i)
        {
            return GetPower(i) + 5;
        }
        //基本攻击力(火力)(空母)
        public int BasicDamageA(int i)
        {
            return (int)((GetPower(i) + GetTorpedo(i)) * 1.5 + GetBomb(i) * 2 + 55);
        }
        //基本攻击力（对潜）
        public int BasicDamageS(int i)
        {
            return (int)(Correction(i) * ((int)example[i].Antisub / 5) + GetAntisub(i) * 2 + AttackAddition(i));
        }
        //协同补正（对潜）
        public double Correction(int i)
        {
            bool temp1 = false;
            bool temp2 = false;
            for (int j = 1; j <= 4; j++)
            {
                if (example[i].soubi[j].Type == "爆雷") temp1 = true;
                if (example[i].soubi[j].Type == "ソナー") temp2 = true;
                if (example[i].soubi[j].Type == "大型ソナー") temp2 = true;
            }
            if (temp1 && temp2) return 1.15;
            else return 1;
        }
        //攻击类别补正（对潜）
        public int AttackAddition(int i)
        {
            bool temp1 = false;
            for (int j = 1; j <= 4; j++)
            {
                if (example[i].soubi[j].Type == "爆雷" || example[i].soubi[j].Type == "ソナー" || example[i].soubi[j].Type == "大型ソナー") temp1 = true;
            }
            if (temp1) return 25;
            else return 10;
        }
        //软上限前补正
        public double CAPB(int formation,int status,int type)
        {
            return capformation[formation, type] * capstatus[status];
        }
        //软上限后补正
        public double CAPA(string citype)
        {
            if (citype == "主砲 + 主砲") return 1.5;
            if (citype == "主砲 + 徹甲彈") return 1.3;
            if (citype == "主砲 + 電探") return 1.2;
            if (citype == "主砲 + 副砲") return 1.1;
            if (citype == "连击") return 1.2;
            return 1;
        }
        //弹药补正
        public double CAPAmmo()
        {
            return 0;
        }
        //总威力(昼战)
        public int DamageAll(int basic, double capb, double capa,double capam)
        {
            double x = basic * capb;
            if (x > 150)
            {
                x = 150 + Math.Sqrt(x - 150);
            }
            x = x * capa * capam;
            return (int)x;
        }
        //总威力(反潜)
        public int DamageAllS(int basic, double capb, double capa, double capam)
        {
            double x = basic * capb;
            if (x > 100)
            {
                x = 100 + Math.Sqrt(x - 100);
            }
            x = x * capa * capam;
            return (int)x;
        }
        //总威力(夜战)
        public int DamageAllY(int basic, double capb, double capa, double capam)
        {
            double x = basic * capb;
            if (x > 300)
            {
                x = 300 + Math.Sqrt(x - 300);
            }
            x = x * capa * capam;
            return (int)x;
        }
        #endregion

        #region 判断类方法
        /// <summary>
        /// 判断是否存在、是否符合要求等的方法
        /// </summary>
        //判断i号位置是否有船
        public bool IfExist(int i)
        {
            return example[i] == null ? false : true;
        }
        //判断是否装有装备
        public bool IfEquip(int i, int j)
        {
            return example[i].soubi[j].Icon != null ? true : false;
        }
        //是否是航母系
        public bool IfAirforce(int i)
        {
            return airforce.Find(x => x == example[i].Type) != null;
        }
        //是否参与雷击战
        public bool IfTorpedo(int i)
        {
            return !IfAirforce(i) && example[i].Torpedo != 0;
        }
        //是否可以对潜
        public bool IfAntiSub(int i)
        {
            return antisub.Find(x => x == example[i].Type) != null && example[i].Antisub > 0;
        }
        //是否能开幕雷击
        public bool IfOpenTorpedo(int i)
        {
            if (hastorpedo.Find(x => x == example[i].Type) == null) return false;
            if ((example[i].Type == "潜水艦" || example[i].Type == "潜水空母") && example[i].LV >= 10) return true;
            for (int j = 1; j <= 4; j++)
            {
                if (example[i].soubi[j].Name == "甲標的 甲") return true;
            }
            return false;
        }
        //是否可以观测射击
        public bool IfCutIn(int i)
        {
            bool tempbool1 = false;
            bool tempbool2 = false;
            int sum = 0;
            for (int j = 1; j <= 4; j++)
            {
                if (example[i].soubi[j].Type == "水上偵察機" || example[i].soubi[j].Type == "水上爆撃機") tempbool1 = true;
                if (mainarti.Find(x => x == example[i].soubi[j].Type) != null) tempbool2 = true;
                if (artillery.Find(x => x == example[i].soubi[j].Type) != null) sum++;
            }
            return tempbool1 && tempbool2 && sum >= 2;
        }
        //是否可以二连
        public bool IfDoubleHit(int i)
        {
            bool tempbool1 = false;
            int sum = 0;
            for (int j = 1; j <= 4; j++)
            {
                if (example[i].soubi[j].Type == "水上偵察機" || example[i].soubi[j].Type == "水上爆撃機") tempbool1 = true;
                if (mainarti.Find(x => x == example[i].soubi[j].Type) != null) sum++;
            }
            return tempbool1 && sum >= 2;
        }
        #endregion

        #region 获取属性方法
        /// <summary>
        /// 舰娘的基本属性，Kan类属性，部分计算
        /// 具体含义详见方法名
        /// </summary>
        /// <param name="i">编队中6个位置的编号</param>
        public string GetName(int i)
        {
            return example[i] != null ? example[i].Name : null;
        }
        public string GetType(int i)
        {
            return example[i] != null ? example[i].Type : null;
        }
        public int GetLevel(int i)
        {
            return example[i] != null ? example[i].LV : 0;
        }
        #endregion

        #region 获取装备属性方法
        /// <summary>
        /// 获取Soubi类的属性，详见方法名
        /// </summary>
        /// <param name="i">编队中6个位置的编号</param>
        /// <param name="j">船的第几个装备</param>
        public string GetIcon(int i, int j)
        {
            return example[i].soubi[j].Icon;
        }
        public string GetSoubiName(int i, int j)
        {
            return example[i].soubi[j].Name;
        }
        public string GetSoubiType(int i, int j)
        {
            return example[i].soubi[j].Type;
        }
        #endregion
    }
}
