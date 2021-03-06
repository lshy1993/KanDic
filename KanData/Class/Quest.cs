﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanData
{
    public class Quest
    {
        public string Type { set; get; }
        public string ID { set; get; }
        public string Father { set; get; }
        public string Name { set; get; }
        public string GetFuel { set; get; }
        public string GetAmmo { set; get; }
        public string GetSteel { set; get; }
        public string GetAluminium { set; get; }
        public string GetOther { set; get; }
        public string Content { set; get; }
        public string IsDaily { set; get; }
        public string IsWeekly { set; get; }
        public string Extra { set; get; }

        public string IsDone { set; get; }

        public Quest() { }
    }
}
