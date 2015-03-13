using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanDic.Resources
{
    public class Soubi
    {
        public string Number{ get; set; }
        public string Name{ get; set; }
        public string Content{ get; set; }
        public string FileName{ get; set; }
        public string Type{ get; set; }
        public string Icon { get; set; }

        public Boolean quzhu{ get; set; }
        public Boolean qingxun{ get; set; }
        public Boolean leixun{ get; set; }
        public Boolean zhongxun{ get; set; }
        public Boolean dizhan{ get; set; }
        public Boolean gaozhan{ get; set; }

        public Boolean hangzhan{ get; set; }
        public Boolean hangxun{ get; set; }

        public Boolean qingmu{ get; set; }
        public Boolean zhengmu{ get; set; }
        public Boolean shuimu{ get; set; }
        
        public Boolean qianshui{ get; set; }
        public Boolean qianmu{ get; set; }
        public Boolean qiankong{ get; set; }

        public Boolean yanglu{ get; set; }
        public Boolean gongzuo{ get; set; }

        public string Shu1{ get; set; }
        public string ShuNum1{ get; set; }
        public string Shu2{ get; set; }
        public string ShuNum2{ get; set; }
        public string Shu3{ get; set; }
        public string ShuNum3{ get; set; }
        public string Shu4{ get; set; }
        public string ShuNum4{ get; set; }
        public string Shu5{ get; set; }
        public string ShuNum5{ get; set; }
        public string Shu6{ get; set; }
        public string ShuNum6{ get; set; }

        public string Fuel{ get; set; }
        public string Ammo{ get; set; }
        public string Steel{ get; set; }
        public string Aluminium{ get; set; }

        public string Tuijian { get; set; }
        public string Mishu { get; set; }
        public string Remarks { get; set; }

        public Soubi() { }
    }
}
