using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;

namespace KellControler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string module = ConfigurationManager.AppSettings["module"];
        string K1 = ConfigurationManager.AppSettings["K1"];
        string K2 = ConfigurationManager.AppSettings["K2"];
        string K3 = ConfigurationManager.AppSettings["K3"];
        string K4 = ConfigurationManager.AppSettings["K4"];

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Host;
            foreach (Control c in this.Controls)
            {
                if (c.Text.Contains("K1"))
                    c.Text = c.Text.Replace("K1", K1);
                if (c.Text.Contains("K2"))
                    c.Text = c.Text.Replace("K2", K2);
                if (c.Text.Contains("K3"))
                    c.Text = c.Text.Replace("K3", K3);
                if (c.Text.Contains("K4"))
                    c.Text = c.Text.Replace("K4", K4);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            bool flag = DB.SendCmd(Host, module, "setK1");
            btnSend.Enabled = !flag;
            button9.Enabled = flag;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool flag = DB.SendCmd(Host, module, "resetK1");
            button9.Enabled = !flag;
            btnSend.Enabled = flag;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = DB.SendCmd(Host, module, "setK2");
            button1.Enabled = !flag;
            button8.Enabled = flag;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool flag = DB.SendCmd(Host, module, "resetK2");
            button8.Enabled = !flag;
            button1.Enabled = flag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool flag = DB.SendCmd(Host, module, "setK3");
            button2.Enabled = !flag;
            button7.Enabled = flag;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool flag = DB.SendCmd(Host, module, "resetK3");
            button7.Enabled = !flag;
            button2.Enabled = flag;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool flag = DB.SendCmd(Host, module, "setK4");
            button3.Enabled = !flag;
            button5.Enabled = flag;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool flag = DB.SendCmd(Host, module, "resetK4");
            button5.Enabled = !flag;
            button3.Enabled = flag;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool flag = DB.SendCmd(Host, module, "setAll");
            btnSend.Enabled = button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = !flag;
            button5.Enabled = button7.Enabled = button8.Enabled = button9.Enabled = button6.Enabled = flag;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool flag = DB.SendCmd(Host, module, "resetAll");
            button5.Enabled = button7.Enabled = button8.Enabled = button9.Enabled = button6.Enabled = !flag;
            btnSend.Enabled = button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = flag;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Exception ex = SetServerSocket(textBox1.Text.Trim());
            if (ex != null)
            {
                MessageBox.Show("保存失败：" + ex.Message);
            }
            else
            {
                MessageBox.Show("保存成功！");
            }
        }

        public string Host
        {
            get
            {
                string h = "127.0.0.1";
                string host = ConfigurationManager.AppSettings["host"];
                if (!string.IsNullOrEmpty(host))
                    h = host;
                return h;
            }
        }

        public Exception SetServerSocket(string host)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(Application.ExecutablePath + ".config");
                XmlNode xNode;
                XmlElement xElem;
                xNode = xDoc.SelectSingleNode("//appSettings");
                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='host']");
                xElem.SetAttribute("value", host);
                xDoc.Save(Application.ExecutablePath + ".config");
            }
            catch (Exception e)
            {
                return e;
            }
            return null;
        }
    }
}
