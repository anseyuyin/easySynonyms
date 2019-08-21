using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace easySynonyms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Process pyP;
        private void button1_Click(object sender, EventArgs e)
        {
            var idx = tabControl1.SelectedIndex;
            string inputC = "";
            switch (idx) {
                case 0:
                    inputC = textBox1.Text;
                    break;
                case 1:
                    string c = checkBox1.Checked ? " vs_seg " : " vs ";
                    inputC = textBox2.Text + c + textBox3.Text;
                    break;
                case 2:
                    inputC = "seg " + textBox4.Text;
                    break;
            }

            //查询
            tryInitPyP();
            pyP.StandardInput.WriteLine(inputC);

        }

        string sArgName = "easySynonyms.py";

        private void tryInitPyP() {
            if (pyP != null) return;
            richTextBox1.AppendText("正在初始化,请稍后... \n");

            Process p = new Process();
            pyP = p;
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sArgName;// 获得python文件的绝对路径（将文件放在c#的debug文件夹中可以这样操作）
            //path = @"C:\Users\user\Desktop\test\" + sArgName;//(因为我没放debug下，所以直接写的绝对路径,替换掉上面的路径了)
            //p.StartInfo.FileName = @"D:\Python\envs\python3\python.exe";//没有配环境变量的话，可以像我这样写python.exe的绝对路径。如果配了，直接写"python.exe"即可
            p.StartInfo.FileName = "python";//没有配环境变量的话，可以像我这样写python.exe的绝对路径。如果配了，直接写"python.exe"即可
            string sArguments = path;

            p.StartInfo.Arguments = sArguments;

            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardOutput = true;

            p.StartInfo.RedirectStandardInput = true;

            p.StartInfo.RedirectStandardError = true;


            //后台启动 py 进程
            p.StartInfo.CreateNoWindow = true;

            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;


            p.Start();
            p.BeginOutputReadLine();
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);

            Console.ReadLine();

        }

        private bool canPrint = false;

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e) {
            var d = e.Data;
            Action at = new Action(delegate () {
                if (d != null && d.Length < 3) return;

                if (d != null &&  d.Substring(0, 3) == "@+@")
                {
                    canPrint = true;
                    return;
                }
                else if(d != null && d.Substring(0, 3) == "@-@") {
                    canPrint = false;
                    richTextBox1.AppendText("----------------------------------------------------------\n");
                    return;
                }

                if (canPrint) {
                    richTextBox1.AppendText(d + "\n");
                }
            });

            Invoke(at);
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox3.SelectAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void textBox4_MouseDown(object sender, MouseEventArgs e)
        {
            textBox4.SelectAll();
        }
    }
}
