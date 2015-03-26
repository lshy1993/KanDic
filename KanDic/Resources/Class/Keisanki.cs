using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanDic.Resources
{
    public class Keisanki
    {
        MoniKan[] example { get; set; }
        int counter, lvall, search;
        double sdold, sdnew, sdsimple;
        public Keisanki(MoniKan[] xx)
        {
            example = xx;
            data_init();
        }

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

        public int GetLevelAll()
        {
            return lvall;
        }

        public double GetLevelAverage()
        {
            if (counter == 0)
                return 0;
            else
                return lvall / counter;
        }

        public double GetOldSearch()
        {
            return sdold + Math.Sqrt(search);
        }
        public double GetNewSearch()
        {
            return sdnew + Math.Sqrt(search);
        }
        public double GetSimpleSearch()
        {
            return sdsimple + Math.Sqrt(search);
        }
    }

}
