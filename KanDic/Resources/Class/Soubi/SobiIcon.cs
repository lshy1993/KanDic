using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanDic.Resources
{
    public class SobiIcon
    {
        public string Sobi1 { set; get; }
        public string Sobi2 { set; get; }
        public string Sobi3 { set; get; }
        public string Sobi4 { set; get; }

        public SobiIcon() { }

        public SobiIcon(string a,string b,string c,string d)
        {
            Sobi1 = Get_File(a);
            Sobi2 = Get_File(b);
            Sobi3 = Get_File(c);
            Sobi4 = Get_File(d);
        }

        public string Get_File(string x)
        {
            switch (x)
            {
                case "小口径主炮":
                    return "/Cache/icon/soubi/68.PNG";
                case "中口径主炮":
                    return "/Cache/icon/soubi/70.PNG";
                case "大口径主炮":
                    return "/Cache/icon/soubi/72.PNG";
                case "副炮":
                    return "/Cache/icon/soubi/74.PNG";
                case "鱼雷":
                    return "/Cache/icon/soubi/76.PNG";
                case "舰上战斗机":
                    return "/Cache/icon/soubi/78.PNG";
                case "舰上轰炸机":
                    return "/Cache/icon/soubi/80.PNG";
                case "舰上强击机":
                    return "/Cache/icon/soubi/82.PNG";
                case "舰上侦察机":
                    return "/Cache/icon/soubi/84.PNG";
                case "水上轰炸机":
                    return "/Cache/icon/soubi/86.PNG";
                case "水上侦察机":
                    return "/Cache/icon/soubi/86.PNG";
                case "电探":
                    return "/Cache/icon/soubi/88.PNG";
                case "三式弹":
                    return "/Cache/icon/soubi/90.PNG";
                case "彻甲弹":
                    return "/Cache/icon/soubi/92.PNG";
                case "应急修理要员":
                    return "/Cache/icon/soubi/94.PNG";
                case "对空机枪":
                    return "/Cache/icon/soubi/96.PNG";
                case "主炮类":
                    return "/Cache/icon/soubi/98.PNG";
                case "水雷":
                    return "/Cache/icon/soubi/100.PNG";
                case "水听":
                    return "/Cache/icon/soubi/102.PNG";
                case "机关部强化类":
                    return "/Cache/icon/soubi/104.PNG";
                case "大发动艇":
                    return "/Cache/icon/soubi/106.PNG";
                case "カ号观测机":
                    return "/Cache/icon/soubi/108.PNG";
                case "三式指挥联络机（对潜）":
                    return "/Cache/icon/soubi/110.PNG";
                case "追加装甲":
                    return "/Cache/icon/soubi/112.PNG";
                case "探照灯":
                    return "/Cache/icon/soubi/114.PNG";
                case "运输桶":
                    return "/Cache/icon/soubi/116.PNG";
                case "舰艇修理设施":
                    return "/Cache/icon/soubi/118.PNG";
                case "照明弹":
                    return "/Cache/icon/soubi/120.PNG";
                case "舰队司令部设施":
                    return "/Cache/icon/soubi/122.PNG";
                case "舰载机熟练员":
                    return "/Cache/icon/soubi/124.PNG";
                case "高射装置类":
                    return "/Cache/icon/soubi/126.PNG";
                case "WG42":
                    return "/Cache/icon/soubi/128.PNG";
                case "熟练见张员":
                    return "/Cache/icon/soubi/130.PNG";
                default:
                    return "";
            }
        }
    }
}
