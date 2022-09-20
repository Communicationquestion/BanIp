using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace BanIp
{
    public partial class Form1 : Form
    {

        private System.Threading.Timer timerClose;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            addip();
        }
        public static bool netConect()
        {
            try
            {
                System.Net.IPHostEntry ipHe = System.Net.Dns.GetHostEntry("www.baidu.com");
                return true;

            }
            catch
            {
                return false;

            }
        }

        public void isconect()
        {
            if (netConect())
            {
                label5.Text = "����������";
            }
            else
            {
                label5.Text = "����δ����";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)

        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            if (radioButton2.Checked)
            {
                string strY = "��ʱ�䡿" + currentTime.ToString();
                string str1 = "������Դ��" + textBox1.Text + " " + radioButton2.Text + "\r\n";
                string str2 = "��Ŀ��IP��" + textBox2.Text + comboBox3.Text + "\r\n";
                string str3 = "��������Դ��" + textBox3.Text + "\r\n";
                string str4 = "���¼�������" + comboBox2.Text + "\r\n";
                textBox4.Text = str1 + str2 + str3 + str4 + strY;
                textBox5.Text += "\r\n" + textBox1.Text;
                //  this.textBox4.Focus();
                this.textBox4.SelectAll();
                this.textBox4.Copy();
                wtip();
            }
            else if (radioButton1.Checked)
            {
                string strY = "��ʱ�䡿" + currentTime.ToString();
                string str1 = "������Դ��" + textBox1.Text + " " + radioButton1.Text + "\r\n";
                string str2 = "��Ŀ��IP��" + textBox2.Text + comboBox3.Text + "\r\n";
                string str3 = "��������Դ��" + textBox3.Text + "\r\n";
                string str4 = "���¼�������" + comboBox2.Text + "\r\n";
                textBox4.Text = str1 + str2 + str3 + str4 + strY;
                textBox5.Text += "\r\n" + textBox1.Text;

                //   this.textBox4.Focus();
                this.textBox4.SelectAll();
                this.textBox4.Copy();
                wtip();

            }
        }
        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {




        }

        private void button2_Click(object sender, EventArgs e)
        {
            timerClose = new System.Threading.Timer(new TimerCallback(timerCall), this, 1000, 1000 * 5);
            findip();
        }
        private void timerCall(object obj)
        {
            isconect();
        }

        private void addip()
        {
            string txt = File.ReadAllText("C:/Users/work/Desktop/banip.txt");
            textBox5.Text = txt;
        }
        private void wtip()
        {
            File.AppendAllText("C:/Users/work/Desktop/banip.txt", "\r\n" + textBox1.Text);
            Movetoend();
        }
        private void findip()
        {
            string[] line = File.ReadAllLines(@"C:/Users/work/Desktop/banip.txt");
            int p = 0;
            for (int i = 0; i < line.Length; i++)
            {
                p = p + line[i].Length + 2;
                if (textBox1.Text == line[i])
                {
                    label6.Text = "�Ѵ���";
                    //���ı����ȡ����
                    this.textBox5.Focus();
                    //���ù���λ�õ��ı�β
                    this.textBox5.Select(p - 1, 0);
                    //�������ؼ���괦
                    this.textBox5.ScrollToCaret();
                    break;
                }
                else
                {
                    label6.Text = "ip���";
                }
            }
        }
        private void Movetoend()
        {
            //���ı����ȡ����
            this.textBox5.Focus();
            //���ù���λ�õ��ı�β
            this.textBox5.Select(this.textBox5.TextLength, 0);
            //�������ؼ���괦
            this.textBox5.ScrollToCaret();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            findip();
            cc();
        }
        public void cc()
        {

            string[] line = File.ReadAllLines(@"C:/Users/work/Desktop/banip.txt");
            string[] lines = textBox4.Lines;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    if (lines[i] == line[j])
                    {
                        textBox4.Text += "\r\n" + "\r\n" + lines[i];
                    }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            socket();
        }
        private void socket()
        {
            byte[] buffer = new byte[2048]; 
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("119.3.140.16"), 6666));
            clientSocket.Send(readtxt());
            int count = clientSocket.Receive(buffer);
            string msg = Encoding.UTF8.GetString(buffer, 0, count);
            textBox4.Text = msg;
            clientSocket.Close();

        }


       
        public byte[] readtxt()
        {

            string text = System.IO.File.ReadAllText(@"C:/Users/work/Desktop/banip.txt");

            byte[] byData = new byte[2048];
            byData = Encoding.Default.GetBytes(text);
            return byData;
        }
    }
    
}