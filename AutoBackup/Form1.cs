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

namespace AutoBackup
{
    public partial class Form1 : Form
    {
        public string sourcePath = string.Empty;
        public string targetPath = string.Empty;
        public string actualTime = string.Empty;
        public string checkTime = string.Empty;

        public Form1()
        {
               InitializeComponent();
         }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox1.Text = fbd.SelectedPath;
                    sourcePath = fbd.SelectedPath;

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox2.Text = fbd.SelectedPath;
                    targetPath = fbd.SelectedPath;

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            actualTime = DateTime.Now.ToString("HH:mm");
            checkTime = comboBox1.SelectedItem.ToString() + ":" + comboBox2.SelectedItem.ToString();
            if ((checkTime == actualTime) && ((textBox1.Text != "") || (textBox2.Text != "")))
            {
                label6.Visible = true;
                label7.Visible = true;
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = Environment.GetEnvironmentVariable("WINDIR") + @"\System32\xcopy.exe";
                proc.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\" /E /I /Y", sourcePath, targetPath);
                proc.Start();
                label7.Text = actualTime;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == ""))
            { 
            MessageBox.Show("Please select source and target paths.");
            
            }

            if ((textBox1.Text != "") && (textBox2.Text != ""))
            {
                timer1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;
        }
    }
}

