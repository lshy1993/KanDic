using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace KanData
{
    public class DataInit
    {
        public List<Soubi> soubilist = new List<Soubi>();
        public List<Kan> kanlist = new List<Kan>();
        public List<Enemy> enemylist = new List<Enemy>();
        public List<Map> maplist = new List<Map>();
        public List<Quest> questlist = new List<Quest>();
        public List<Expedition> explist = new List<Expedition>();
        public List<Revamp> revamplist = new List<Revamp>();
        public UpdateInfo updateinfo = new UpdateInfo();

        public Assembly _assembly = Assembly.GetExecutingAssembly();
        public Stream sStream;

        public DataInit()
        {
            Load_Soubi();
            Load_Enemy();
            Load_Kan();
            Load_Exp();
            Load_Map();
            Load_Quest();
            Load_Revamp();
            /*StreamWriter sw = new StreamWriter("D:\\test2.txt",false,Encoding.UTF8);
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(kanlist);
            sw.Write(content);
            sw.Flush();
            sw.Close();*/
        }

        public Kan GetKan(int x)
        {
            for (int i = 0; i < 250; i++)
            {
                if (kanlist[i].Number == x) return kanlist[i];
            }
            return null;
        }

        //读取ship.json
        private void Load_Kan()
        {
            sStream = _assembly.GetManifestResourceStream("KanData.XmlData.ship.json");
            StreamReader sr = new StreamReader(sStream);
            var html = sr.ReadToEnd();
            kanlist = JsonConvert.DeserializeObject<List<Kan>>(html);
        }

        //读取expedition.json
        private void Load_Exp()
        {
            sStream = _assembly.GetManifestResourceStream("KanData.XmlData.expedition.json");
            StreamReader sr = new StreamReader(sStream);
            var html = sr.ReadToEnd();
            explist = JsonConvert.DeserializeObject<List<Expedition>>(html);
        }

        //读取map.json
        private void Load_Map()
        {
            sStream = _assembly.GetManifestResourceStream("KanData.XmlData.map.json");
            StreamReader sr = new StreamReader(sStream);
            var html = sr.ReadToEnd();
            maplist = JsonConvert.DeserializeObject<List<Map>>(html);
        }

        //读取quest.json
        private void Load_Quest()
        {
            sStream = _assembly.GetManifestResourceStream("KanData.XmlData.quest.json");
            StreamReader sr = new StreamReader(sStream);
            var html = sr.ReadToEnd();
            questlist = JsonConvert.DeserializeObject<List<Quest>>(html);
        }

        //读取revamp.json
        private void Load_Revamp()
        {
            sStream = _assembly.GetManifestResourceStream("KanData.XmlData.revamp.json");
            StreamReader sr = new StreamReader(sStream);
            var html = sr.ReadToEnd();
            revamplist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Revamp>>(html);
        }

        //读取soubi.json
        private void Load_Soubi()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanData.XmlData.soubi.json");
            StreamReader sr = new StreamReader(sStream);
            var html = sr.ReadToEnd();
            soubilist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Soubi>>(html);
        }

        #region 读取xml并生成Enemy类
        private void Load_Enemy()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanData.XmlData.Enemy.xml");
            XmlDocument EnemyList = new XmlDocument();
            EnemyList.Load(sStream);
            XmlElement EnemyInfo = EnemyList.DocumentElement;
            foreach (XmlNode temp in EnemyInfo.ChildNodes)
            {
                Set_Enemy(temp);
            }
        }

        private void Set_Enemy(XmlNode x)
        {
            Enemy shenhai = new Enemy();
            foreach (XmlNode yy in x.ChildNodes)
            {
                string name1 = yy.Name;
                typeof(Enemy).GetProperty(name1).SetValue(shenhai, yy.InnerText, null);
            }
            enemylist.Add(shenhai);
        }
        #endregion

        /* 已废除
        读取xml并生成Kan类
        private void Load_Kan()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanData.XmlData.Ships.xml");
            XmlDocument ShipList = new XmlDocument();
            ShipList.Load(sStream);
            XmlElement ShipInfo = ShipList.DocumentElement;
            foreach (XmlNode temp in ShipInfo.ChildNodes)
            {
                Set_Kan(temp);
            }
        }

        private void Set_Kan(XmlNode x)
        {
            string name1;
            Kan ship = new Kan();
            foreach (XmlNode yy in x.ChildNodes)
            {
                name1 = yy.Name;
                var prop = typeof(Kan).GetProperty(name1);
                if (prop.PropertyType.Equals(typeof(int)))
                {
                    if (!string.IsNullOrEmpty(yy.InnerText))
                    {
                        prop.SetValue(ship, Convert.ToInt32(yy.InnerText), null);
                    }
                }
                else
                {
                    prop.SetValue(ship, yy.InnerText, null);
                }
            }
            //ship.ImageNormal = "/Cache/ships/" + ship.FileName + ".swf/Image 5.jpg";
            //ship.ImageSmall = "/Cache/ships/" + ship.FileName + ".swf/Image 1.jpg";
            ship.IsFinal = ship.LinkNumber == 0;
            if (ship.Name != null)
            {
                kanlist.Add(ship);
            }
        }
        #endregion

        读取xml生成Expedition
        private void Load_Exp()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanData.XmlData.Expedition.xml");
            XmlDocument ExpList = new XmlDocument();
            ExpList.Load(sStream);
            XmlElement ExpInfo = ExpList.DocumentElement;
            foreach (XmlNode temp in ExpInfo.ChildNodes)
            {
                Set_Exp(temp);
            }
        }

        private void Set_Exp(XmlNode x)
        {
            string name1;
            Expedition exp = new Expedition();
            foreach (XmlNode yy in x.ChildNodes)
            {
                name1 = yy.Name;
                var prop = typeof(Expedition).GetProperty(name1);
                if (prop.PropertyType.Equals(typeof(int)))
                {
                    prop.SetValue(exp, Convert.ToInt32(yy.InnerText), null);
                }
                else
                {
                    prop.SetValue(exp, yy.InnerText, null);
                }
            }
            exp.Hard = "/KanDic;component/Cache/icon/expand/" + exp.Hard + ".PNG";
            exp.ItemName1 = "/KanDic;component/Cache/icon/expand/" + exp.ItemName1 + ".PNG";
            exp.ItemName2 = "/KanDic;component/Cache/icon/expand/" + exp.ItemName2 + ".PNG";
            explist.Add(exp);
        }
        
        读取xml生成Map
        private void Load_Map()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanData.XmlData.Map.xml");
            XmlDocument MapList = new XmlDocument();
            MapList.Load(sStream);
            XmlElement MapInfo = MapList.DocumentElement;
            foreach (XmlNode temp in MapInfo.ChildNodes)
            {
                Set_Map(temp);
            }
        }

        private void Set_Map(XmlNode x)
        {
            Map map = new Map();
            foreach (XmlNode yy in x.ChildNodes)
            {
                string name1 = yy.Name;
                var prop = typeof(Map).GetProperty(name1);
                if (prop.PropertyType.Equals(typeof(int)))
                {
                    prop.SetValue(map, Convert.ToInt32(yy.InnerText), null);
                }
                else
                {
                    prop.SetValue(map, yy.InnerText, null);
                }
            }
            maplist.Add(map);
        }
        
        #region 读取xml并生成Quest类
        private void Load_Quest()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanData.XmlData.Quest.xml");
            XmlDocument EnemyList = new XmlDocument();
            EnemyList.Load(sStream);
            XmlElement EnemyInfo = EnemyList.DocumentElement;
            foreach (XmlNode temp in EnemyInfo.ChildNodes)
            {
                Set_Quest(temp);
            }
        }

        private void Set_Quest(XmlNode x)
        {
            Quest renwu = new Quest();
            foreach (XmlNode yy in x.ChildNodes)
            {
                string name1 = yy.Name;
                typeof(Quest).GetProperty(name1).SetValue(renwu, yy.InnerText, null);
            }
            questlist.Add(renwu);
        }
        #endregion
        
        #region 读取xml并生成Soubi类
        private void Load_Soubi()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanData.XmlData.Soubi.xml");
            XmlDocument ShipList = new XmlDocument();
            ShipList.Load(sStream);
            XmlElement ShipInfo = ShipList.DocumentElement;
            foreach (XmlNode temp in ShipInfo.ChildNodes)
            {
                Set_Soubi(temp);
            }
        }

        private void Set_Soubi(XmlNode x)
        {
            string name1;
            Soubi equip = new Soubi();
            foreach (XmlNode yy in x.ChildNodes)
            {
                name1 = yy.Name;
                var prop = typeof(Soubi).GetProperty(name1);
                if (prop.PropertyType.Equals(typeof(int)))
                {
                    typeof(Soubi).GetProperty(name1).SetValue(equip, Convert.ToInt32(yy.InnerText), null);
                }
                else
                {
                    typeof(Soubi).GetProperty(name1).SetValue(equip, yy.InnerText, null);
                }
            }
            equip.Image = "/Cache/equipment/" + string.Format("{0:D3}", equip.Number) + ".png";
            equip.Icon = "/Cache/icon/soubi/" + equip.Icon + ".PNG";
            soubilist.Add(equip);
        }
        #endregion
        */
    }
}
