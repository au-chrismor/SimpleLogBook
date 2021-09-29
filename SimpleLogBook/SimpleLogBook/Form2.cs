using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleLogBook
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.Text = "New Item";
            this.textBox1.Text = DateTime.Now.ToString();
            this.comboBox1.Items.Insert(0, "AM");
            this.comboBox1.SelectedIndex = 0;
            this.comboBox1.Items.Insert(1, "USB");
            this.comboBox1.Items.Insert(2, "LSB");
            this.comboBox1.Items.Insert(3, "CW");
            this.comboBox1.Items.Insert(4, "FM");
            this.comboBox1.Items.Insert(5, "TV");
            this.comboBox1.Items.Insert(6, "RTTY");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Utils utils = new Utils();
            String myCallSign = utils.GetMyCallSign();
            LogEntry le = new LogEntry();
            le.EntryDate = DateTime.Parse(this.textBox1.Text);
            le.CallSignOut = myCallSign;
            le.CallSignIn = this.textBox2.Text;
            le.Frequency = Convert.ToDouble(textBox3.Text);
            le.Mode = comboBox1.Text;
            le.Power = Convert.ToInt32(textBox4.Text);
            le.SignalIn = this.textBox5.Text;
            le.SignalOut = this.textBox6.Text;
            le.RemoteName = this.textBox7.Text;
            le.RemoteLocation = this.textBox8.Text;
            le.Comments = this.textBox9.Text;
            le.Save();
            this.Close();
        }
    }
}
