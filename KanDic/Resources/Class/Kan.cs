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
        public BasicInfo BasicInfo { set; get; }
        public BuildInfo BuildInfo { set; get; }
        public BattleInfo BattleInfo { set; get; }
        public EquipInfo EquipInfo { set; get; }
        public UpdateInfo UpdateInfo { set; get; }
        public SourceInfo SourceInfo { set; get; }
        public SupplyInfo SupplyInfo { set; get; }
        public MaxInfo MaxInfo { set; get; }
        public ResolveInfo ResolveInfo { set; get; }

        public Kan()
        {
            BasicInfo = new BasicInfo();
            BuildInfo = new BuildInfo();
            BattleInfo = new BattleInfo();
            EquipInfo = new EquipInfo();
            UpdateInfo = new UpdateInfo();
            SourceInfo = new SourceInfo();
            SupplyInfo = new SupplyInfo();
            MaxInfo = new MaxInfo();
            ResolveInfo = new ResolveInfo();
        }

        public Kan(BasicInfo a,BattleInfo b,BuildInfo c,EquipInfo d,MaxInfo e,SourceInfo f,SupplyInfo g,UpdateInfo h,ResolveInfo i)
        {
            BasicInfo = a;
            BattleInfo = b;
            BuildInfo = c;
            EquipInfo = d;
            MaxInfo = e;
            SourceInfo = f;
            SupplyInfo = g;
            UpdateInfo = h;
            ResolveInfo = i;
        }

    }
}
