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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KanDic.Resources;
using System.Xml;
using System.Reflection;

namespace KanDic.Viewer
{
    /// <summary>
    /// Quest.xaml 的交互逻辑
    /// </summary>
    public partial class Quest : UserControl
    {
        public List<QuestInfo> list = new List<QuestInfo>();
        public List<QuestInfo> listshow = new List<QuestInfo>();
        public List<QuestInfo> listfilter = new List<QuestInfo>();

        public Quest()
        {
            Load_Quest();
            InitializeComponent();
            Reset_List(1);
        }

        #region 读取xml并生成List<QuestInfo>
        private void Load_Quest()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("KanDic.Resources.Data.Quest.xml");
            XmlDocument QuestList = new XmlDocument();
            QuestList.Load(sStream);
            foreach (XmlNode temp in QuestList.DocumentElement.ChildNodes)
            {
                Set_Quest(temp);
            }
        }

        private void Set_Quest(XmlNode x)
        {
            QuestInfo temp = new QuestInfo();
            foreach (XmlNode yy in x.ChildNodes)
            {
                string name1 = yy.Name;
                typeof(QuestInfo).GetProperty(name1).SetValue(temp, yy.InnerText, null);
            }
            list.Add(temp);
        }
        #endregion

        #region 重置listview内容
        private void Reset_List(int x)
        {
            string typename;
            switch (x)
            {
                case 1:
                    typename = "编成类";
                    break;
                case 2:
                    typename = "出击类";
                    break;
                case 3:
                    typename = "演习类";
                    break;
                case 4:
                    typename = "远征类";
                    break;
                case 5:
                    typename = "补给/入渠类";
                    break;
                case 6:
                    typename = "工厂类";
                    break;
                case 7:
                    typename = "改装类";
                    break;
                case 8:
                    typename = "其他类";
                    break;
                default:
                    typename = "";
                    break;
            }
            listshow.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Type == typename) listshow.Add(list[i]);
            }
            ShowList.ItemsSource = null;
            ShowList.ItemsSource = listshow;
        }
        #endregion

        #region 筛选listview内容
        private void Filt_List(string x)
        {
            listfilter.Clear();
            switch (x)
            {
                case "周常任务":
                    for (int i = 0; i < listshow.Count; i++)
                        if (Convert.ToBoolean(listshow[i].IsWeekly)) listfilter.Add(listshow[i]);
                    break;
                case "日常任务":
                    for (int i = 0; i < listshow.Count; i++)
                        if (Convert.ToBoolean(listshow[i].IsDaily)) listfilter.Add(listshow[i]);
                    break;
                case "单次任务":
                    for (int i = 0; i < listshow.Count; i++)
                        if (!(Convert.ToBoolean(listshow[i].IsWeekly) && Convert.ToBoolean(listshow[i].IsDaily))) listfilter.Add(listshow[i]);
                    break;
                default:
                    ShowList.ItemsSource = null;
                    ShowList.ItemsSource = listshow;
                    return;
            }
            ShowList.ItemsSource = null;
            ShowList.ItemsSource = listfilter;
        }
        #endregion

        //任务类型选择
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton temp = sender as RadioButton;
            Reset_List(Convert.ToInt32(temp.Tag));
            foreach (UIElement element in FilterPanel.Children)
            {
                if (element is RadioButton)
                {
                    RadioButton radbut = element as RadioButton;
                    if ((bool)radbut.IsChecked) Filter_Click(radbut,new RoutedEventArgs());
                }
            }
        }

        //重置按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement element in FilterPanel.Children)
            {
                if(element is RadioButton) ((RadioButton)element).IsChecked = false;
            }
            Filt_List("");
        }

        //过滤选项
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            RadioButton element = sender as RadioButton;
            Filt_List((string)element.Content);
        }
    }
}
