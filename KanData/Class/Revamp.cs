using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanData
{
    public class Revamp
    {
        public string Name { set; get; }
        public string Type { set; get; }
        public int Fuel { set; get; }
        public int Steel { set; get; }
        public int Ammo { set; get; }
        public int Aluminium { set; get; }
        public List<bool> Week { set; get; }
        public int Position { set; get; }
        public string Target { set; get; }
        public string Secretary { set; get; }
        public List<int> Kaifa { set; get; }
        public List<int> Gaixiu { set; get; }
        public List<int> Cost { set; get; }
        public List<string> Extra { set; get; }

        public Revamp() { }
    }
}
