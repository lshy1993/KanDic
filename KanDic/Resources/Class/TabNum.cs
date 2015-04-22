using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanData;

namespace KanDic.Resources
{
    public class TabNum
    {
        public string Number1 { get; set; }
        public string Number2 { get; set; }
        public string Number3 { get; set; }
        public string Number4 { get; set; }
        public string Number5 { get; set; }
        public string Number6 { get; set; }
        public string Number7 { get; set; }
        public string Number8 { get; set; }
        public string Number9 { get; set; }
        public string Number10 { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string Image6 { get; set; }
        public string Image7 { get; set; }
        public string Image8 { get; set; }
        public string Image9 { get; set; }
        public string Image10 { get; set; }
        public string Tab1 { get; set; }
        public string Tab2 { get; set; }
        public string Tab3 { get; set; }
        public string Tab4 { get; set; }
        public string Tab5 { get; set; }

        public int[] Number = new int[10];

        public TabNum()
        {
            Tab_Init(1);
            Number = new int[10] {1,2,3,4,5,6,7,8,9,10};
            Num_Init();
        }

        public TabNum(int group, int team, Kan[] yyy)
        {
            Tab_Init(group);
            Number = new int[10];
            for (int x = 0; x <= 9; x++)
            {
                Number[x] = (group - 1) * 50 + (team - 1) * 10 + x + 1;
            }
            Num_Init();

            if (yyy[Number[0]] != null) Image1 = yyy[Number[0]].ImageNormal;
            else Image1 = "/Cache/error.png";
            if (yyy[Number[1]] != null) Image2 = yyy[Number[1]].ImageNormal;
            else Image2 = "/Cache/error.png";
            if (yyy[Number[2]] != null) Image3 = yyy[Number[2]].ImageNormal;
            else Image3 = "/Cache/error.png";
            if (yyy[Number[3]] != null) Image4 = yyy[Number[3]].ImageNormal;
            else Image4 = "/Cache/error.png";
            if (yyy[Number[4]] != null) Image5 = yyy[Number[4]].ImageNormal;
            else Image5 = "/Cache/error.png";
            if (yyy[Number[5]] != null) Image6 = yyy[Number[5]].ImageNormal;
            else Image6 = "/Cache/error.png";
            if (yyy[Number[6]] != null) Image7 = yyy[Number[6]].ImageNormal;
            else Image7 = "/Cache/error.png";
            if (yyy[Number[7]] != null) Image8 = yyy[Number[7]].ImageNormal;
            else Image8 = "/Cache/error.png";
            if (yyy[Number[8]] != null) Image9 = yyy[Number[8]].ImageNormal;
            else Image9 = "/Cache/error.png";
            if (yyy[Number[9]] != null) Image10 = yyy[Number[9]].ImageNormal;
            else Image10 = "/Cache/error.png";
        }

        public void Num_Init()
        {   
            Number1 = "No." + Number[0].ToString("D3");
            Number2 = "No." + Number[1].ToString("D3");
            Number3 = "No." + Number[2].ToString("D3");
            Number4 = "No." + Number[3].ToString("D3");
            Number5 = "No." + Number[4].ToString("D3");
            Number6 = "No." + Number[5].ToString("D3");
            Number7 = "No." + Number[6].ToString("D3");
            Number8 = "No." + Number[7].ToString("D3");
            Number9 = "No." + Number[8].ToString("D3");
            Number10 = "No." + Number[9].ToString("D3");
        }

        public void Tab_Init(int x)
        {
            switch (x)
            {
                case 1:
                    Tab1 = "No.001-010";
                    Tab2 = "No.011-020";
                    Tab3 = "No.021-030";
                    Tab4 = "No.031-040";
                    Tab5 = "No.041-050";
                    break;
                case 2:
                    Tab1 = "No.051-060";
                    Tab2 = "No.061-070";
                    Tab3 = "No.071-080";
                    Tab4 = "No.081-090";
                    Tab5 = "No.091-100";
                    break;
                case 3:
                    Tab1 = "No.101-110";
                    Tab2 = "No.111-120";
                    Tab3 = "No.121-130";
                    Tab4 = "No.131-140";
                    Tab5 = "No.141-150";
                    break;
                case 4:
                    Tab1 = "No.151-160";
                    Tab2 = "No.161-170";
                    Tab3 = "No.171-180";
                    Tab4 = "No.181-190";
                    Tab5 = "No.191-200";
                    break;
                case 5:
                    Tab1 = "No.201-210";
                    Tab2 = "No.211-220";
                    Tab3 = "No.221-230";
                    Tab4 = "No.231-240";
                    Tab5 = "No.241-250";
                    break;
                default:
                    Tab1 = "No.001-010";
                    Tab2 = "No.011-020";
                    Tab3 = "No.021-030";
                    Tab4 = "No.031-040";
                    Tab5 = "No.041-050";
                    break;
            }
        }
    }
}
