using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using Newtonsoft.Json.Linq;

namespace covidView
{
    public partial class Form1 : Form
    {
        private const string URL = "https://api.corona-19.kr/korea/beta/";
        private string api = "?serviceKey={YOUR_APIKEY_HERE}";

        string[] regions = { "seoul", "gyeonggi", "busan", "incheon", "jeju" };

        JObject json;

        private void reload()
        {
            
            
            label7.Text = DateTime.Now.ToString("yyyy년 MM월dd일 HH시 mm분 ss초");


            using (WebClient wc = new WebClient())
            {
                string res = new WebClient().DownloadString(URL + api);

                json = JObject.Parse(res);

                string latest_update = json["API"]["updateTime"].ToString(); // 갱신시간
                string total = json["korea"]["totalCnt"].ToString(); // 전체 확진자
                string dayIncrease = json["korea"]["incDec"].ToString(); // 하루 증가


                label1.Text = latest_update;
                label2.Text = String.Format("{0:#,###}", Int32.Parse( total ) ) + "명";
                label3.Text = String.Format("{0:#,###}", Int32.Parse( dayIncrease ) ) + "명";


                string select_total = json[regions[comboBox1.SelectedIndex]]["totalCnt"].ToString(); // 전체 확진자
                string select_dayIncrease = json[regions[comboBox1.SelectedIndex]]["incDec"].ToString(); // 하루 증가

                label11.Text = String.Format("{0:#,###}", Int32.Parse(select_total)) + "명";
                label9.Text = String.Format("{0:#,###}", Int32.Parse(select_dayIncrease)) + "명";

            }
        }

        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;

            reload();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            reload();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if( json != null) {
                string total = json[regions[comboBox1.SelectedIndex]]["totalCnt"].ToString(); // 전체 확진자
                string dayIncrease = json[regions[comboBox1.SelectedIndex]]["incDec"].ToString(); // 하루 증가

                label11.Text = String.Format("{0:#,###}", Int32.Parse(total)) + "명";
                label9.Text = String.Format("{0:#,###}", Int32.Parse(dayIncrease)) + "명";
            }

        }
    }
}
