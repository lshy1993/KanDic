using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanUpdate
{
    class Changelog
    {
        public Guid id { get; set; }
        public string Time { get; set; }
        public string Version { get; set; }
        public string Update { get; set; }
    }
}
