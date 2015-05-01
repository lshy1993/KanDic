using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Net;
using System.IO;
using System.Configuration;

namespace KanDic
{
    /// <summary>
    /// Almanac.xaml 的交互逻辑
    /// </summary>
    public partial class Almanac : MetroWindow
    {

        public class Huang
        {
            public string Name { set; get; }
            public string Good { set; get; }
            public string Bad { set; get; }

            public Huang() { }
        }

        public List<Huang> result = new List<Huang>();
        public Random random;
        public int dayseed;

        public Almanac()
        {
            InitializeComponent();
            //联网获取最新黄历内容
            GetJson();
            Date.Text = System.DateTime.Now.ToString("yyyy年MM月dd日 dddd");

            //获取用户种子
            string str = ConfigurationManager.AppSettings["randomseed"];
            //天数种子
            dayseed = DateTime.Now.Year * 1000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
            //产生相对应random
            random = new System.Random(getseed(str) * dayseed);
            //宜忌个数
            int goodnum = random.Next(1713, dayseed) % 3 + 2;
            int badnum = random.Next(1317, dayseed) % 3 + 2;
            //随机挑取
            for (int i = 0; i < goodnum; i++)
            {
                int index = random.Next(1173, dayseed) % result.Count;
                Console.WriteLine("{0}:{1}", i, index);
                TextBlock tbl = new TextBlock();
                tbl.FontSize = 20;
                tbl.Text = result[index].Name;
                Good.Children.Add(tbl);
                TextBlock tbs = new TextBlock();
                tbs.Text = result[index].Good;
                tbs.Margin = new Thickness(0, 0, 0, 5);
                Good.Children.Add(tbs);
                result.RemoveAt(index);
            }
            for (int i = 0; i < badnum; i++)
            {
                int index = random.Next(1137, dayseed) % result.Count;
                Console.WriteLine("xx{0}:{1}", i, index);
                TextBlock tbl = new TextBlock();
                tbl.FontSize = 20;
                tbl.Text = result[index].Name;
                Bad.Children.Add(tbl);
                TextBlock tbs = new TextBlock();
                tbs.Text = result[index].Bad;
                tbs.Margin = new Thickness(0, 0, 0, 5);
                Bad.Children.Add(tbs);
                result.RemoveAt(index);
            }
            BBRecipe();
            CVRecipe();
            Star.Text = "罗盘娘玄学概率：" + SetStar(random.Next(10) % 10 + 1);
        }

        private int getseed(string x)
        {
            int seed = 0;
            for (int i = 0; i < x.Length; i++)
            {
                seed += (int)x[i];
            }
            return seed;
        }

        private void GetJson()
        {
            try
            {
                HttpWebRequest htr = (HttpWebRequest)WebRequest.Create("http://1.pngbase.sinaapp.com/huang.json");
                htr.Method = "GET";
                HttpWebResponse hwr = (HttpWebResponse)htr.GetResponse();
                Stream responseStream = hwr.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                var html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                htr.Abort();
                hwr.Close();
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Huang>>(html);
            }
            catch
            {
                MessageBox.Show("无法获取最新黄历信息QAQ");
                /*
                System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanData.XmlData.huang.json");
                StreamReader sr = new StreamReader(sStream);
                var html = sr.ReadToEnd();
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Huang>>(html);
                */
            }
        }

        private void CVRecipe()
        {
            int r1,r2,r3,r4,zc;
            int num = 0;
            switch(num){
                case 0:
                    r1 = 4000;
                    r2 = 2000;
                    r3 = 5000;
                    r4 = 6500;
                    break;
                case 1:		
                    r1 = 3500;
                    r2 = 3500;
                    r3 = 6000;
                    r4 = 6000;
                    break;
                case 2:
                    r1 = 4000;
                    r2 = 2000;
                    r3 = 5000;
                    r4 = 5200;
                    break;
                default:
                    r1 = 4000;
                    r2 = 2000;
                    r3 = 5000;
                    r4 = 6500;
                    break;
            }
            r1 = r1 + random.Next(72) % 49 * 10;
            r2 = r2 + random.Next(86) % 49 * 10;
            r3 = r3 + random.Next(24) % 49 * 10;
            r4 = r4 + random.Next(741) % 49 * 10;
            zc = random.Next(48) % 10 + 1;
            if(zc <=4 && zc> 0)
            {
                CVFormula.Text = string.Format("空母：{0}/{1}/{2}/{3}/1", r1, r2, r3, r4);
            }
            else if(zc<=8 && zc>4)
            {
                CVFormula.Text = string.Format("空母：{0}/{1}/{2}/{3}/20", r1, r2, r3, r4);
            }
            else
            {
                CVFormula.Text = string.Format("空母：{0}/{1}/{2}/{3}/100", r1, r2, r3, r4);
            }
        }

        private void BBRecipe()
        {
            int r1,r2,r3,r4,zc;
            int num = 0;
            switch(num){
                case 0:
                    r1 = 4000;
                    r2 = 6000;
                    r3 = 6000;
                    r4 = 3000;
                    break;
                case 1:
                    r1 = 4000;
                    r2 = 6000;
                    r3 = 6000;
                    r4 = 2000;
                    break;
                case 2:
                    r1 = 6000;
                    r2 = 5000;
                    r3 = 6000;
                    r4 = 2000;
                    break;
                default:
                    r1 = 4000;
                    r2 = 6000;
                    r3 = 6000;
                    r4 = 3000;
                    break;
            }
            r1 = r1 + random.Next(27) % 49 * 10;
            r2 = r2 + random.Next(68) % 49 * 10;
            r3 = r3 + random.Next(42) % 49 * 10;
            r4 = r4 + random.Next(147) % 49 * 10;
            zc = random.Next(84) % 10 + 1;
            if(zc <=4 && zc> 0)
            {
                BBFormula.Text = string.Format("战舰：{0}/{1}/{2}/{3}/1", r1, r2, r3, r4);
            }
            else if(zc<=8 && zc>4)
            {
                BBFormula.Text = string.Format("战舰：{0}/{1}/{2}/{3}/20", r1, r2, r3, r4);
            }
            else
            {
                BBFormula.Text = string.Format("战舰：{0}/{1}/{2}/{3}/100", r1, r2, r3, r4);
            }
        }

        private string SetStar(int num)
        {
            string result = "";
            for (int i = 0; i < num; i++)
            {
                result += "★";
            }
            return result;
        }

    }
}
