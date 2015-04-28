using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanData
{
    public class Map
    {
        public string Key { set; get; }
        public string Name { set; get; }
        public string Type { set; get; }
        public string Code { set; get; }

        public int xPos { set; get; }
        public int yPos { set; get; }

        public List<Pattern> Pattern { set; get; }

        public string DropDD { set; get; }
        public string DropCL { set; get; }
        public string DropCA { set; get; }
        public string DropSS { set; get; }
        public string DropCV { set; get; }
        public string DropBB { set; get; }
        public string DropAV { set; get; }

        public int Exp { set; get; }

        public string NoBattle { set; get; }

        public Map() { }
    }
}
