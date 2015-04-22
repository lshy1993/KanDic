using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanData
{
    public class UpdateInfo
    {
        public string AppName { set; get; }
        public string AppVersion { set; get; }
        public string AppDescription { set; get; }
        public string AppUpdateTime { set; get; }
        public string DataVersion { set; get; }
        public string DataDescription { set; get; }
        public string DataUpdateTime { set; get; }

        public UpdateInfo()
        {

        }
    }
}
