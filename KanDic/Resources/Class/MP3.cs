using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanDic.Resources
{
    public class MP3
    {
        public int number { get; set; }
        public string name { get; set; }
        public string filename { get; set; }
        public string type { get; set; }

        public MP3()
        {

        }

        public MP3(int x, string y, string z, string a)
        {
            number = x;
            name = y;
            filename = z;
            type = a;
        }

    }
}
