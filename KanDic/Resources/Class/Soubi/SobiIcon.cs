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
            Sobi1 = a;
            Sobi2 = b;
            Sobi3 = c;
            Sobi4 = d;
        }
    }
}
