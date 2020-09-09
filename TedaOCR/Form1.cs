using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net.Http;
using com.baidu.ai;
using System.IO;
using System.Net;
using System.Web;


namespace TedaOCR
{
    public partial class Form1 : Form
    {
        private string FileName = @"d:\Users\Administrator\Desktop\ocrtest.jpeg";
        private string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic?access_token=";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        // 加载图片按钮
        private void buttonOpenImg_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "所有图像文件（*.bmp;*.jpg;*.jpeg;*.png）|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog1.Title = "打开文件";             //文件过滤；
            openFileDialog1.ShowDialog();                   //打开对话框；
            FileName = openFileDialog1.FileName;            //对fileName进行赋值；

            if (FileName != "")                             //对filename进行判断，不为空的情况下在picturebox中打开图片    
            {
                try
                {
                    FileName = openFileDialog1.FileName;
                    Bitmap pic = new Bitmap(FileName);
                    pictureBox1.Image = pic;
                    //MessageBox.Show(Convert.ToString(FileName),"Open Path");
                }
                catch
                {
                    MessageBox.Show("Load image failure", "Error");
                }
            }
            FileName = @FileName;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        // 获取AT
        private void button2_Click(object sender, EventArgs e)
        {
            //AccessToken AT = new AccessToken();
            //textBox2.AppendText(AT.getAccessToken());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();

            // GeneralBasic
            if (radioButton1.Checked)
            {
                host = "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic?access_token=";
            }
            // GeneralLocation
            else if (radioButton2.Checked)
            {
                host = "https://aip.baidubce.com/rest/2.0/ocr/v1/general?access_token=";
            }
            // AccurateBasic
            else if (radioButton3.Checked)
            {
                host = "https://aip.baidubce.com/rest/2.0/ocr/v1/accurate_basic?access_token=";
            }
            // AccurateLocation
            else if (radioButton4.Checked)
            {
                host = "https://aip.baidubce.com/rest/2.0/ocr/v1/accurate?access_token=";
            }
            // Receipt
            else if (radioButton5.Checked)
            {
                host = "https://aip.baidubce.com/rest/2.0/ocr/v1/receipt?access_token=";
            }
            // HandWriting
            else if (radioButton6.Checked)
            {
                host = "https://aip.baidubce.com/rest/2.0/ocr/v1/handwriting?access_token=";
            }
            //MessageBox.Show(Convert.ToString(host));

            // 请求API
            General general = new General();
            string result = general.general(FileName, host);
            
             //分割数据
            string[] sArray = result.Split(',');
            for (int i = 0; i < sArray.Length; i++)
            {
                textBox2.AppendText(sArray[i]+ "\r\n");
                //textBox2.AppendText();
            }
            //textBox2.AppendText(result);
        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jichao Zhao\r\nwww.tustrobot.com", "Contact");
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("18322595021", "Mobile");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


namespace com.baidu.ai
{
    public class AccessToken

    {
        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
        // 返回token示例
        public static String TOKEN = "24.adda70c11b9786206253ddb70affdc46.2592000.1493524354.282335-1234567";

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static String clientId = "uck9A45flml7ITBQ8VaLUojf";
        // 百度云中开通对应服务应用的 Secret Key
        private static String clientSecret = "p19Nl1Rz1DGGMKepK3lNDWk8TogqankG";

        public String getAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            Console.WriteLine("This is a label of result");
            Console.ReadLine();
            return result;
        }
    }

    public class General
    {
        // OCR 通用识别
        public string general(string FileName, string host)
        {
            string token = "24.2b1386aa82f6cae1bfadaf03e3bf29b2.2592000.1578066207.282335-17165172";        // 20191204晚获取的AT
            host = host + token;
            //string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/accurate_basic?access_token=" + token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;
            request.ContentType = "application/x-www-form-urlencoded";

            // 图片的base64编码
            string base64 = getFileBase64(FileName);

            // 增加数据表头：image=
            String str = "image=" + HttpUtility.UrlEncode(base64);
            // The Encoding.GetBytes() method converts a string into a bytes array.
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            //Console.WriteLine("OCR 通用识别:");
            //Console.WriteLine(result);
            //Console.ReadLine();
            return result;
        }

        public String getFileBase64(String filename)
        {
                FileStream filestream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                byte[] arr = new byte[filestream.Length];
                filestream.Read(arr, 0, (int)filestream.Length);
                string baser64 = Convert.ToBase64String(arr);
                filestream.Close();
                return baser64;
        }
    }
}
