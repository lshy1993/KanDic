using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanDic.Resources
{
    public class QuestInfo
    {
        public string Type { set; get; }
        public string ID { set; get; }
        public string Father { set; get; }
        public string Name { set; get; }
        public string GetFuel { set; get; }
        public string GetAmmo { set; get; }
        public string GetSteel { set; get; }
        public string GetAluminium { set; get; }
        public string GetOhter { set; get; }
        public string Content { set; get; }
        public string IsDaily { set; get; }
        public string IsWeekly { set; get; }
    }
}
