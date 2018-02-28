using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Regular
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var time = new RegularExpressions(textBox2.Text);
            MessageBox.Show("Time: " + time.convertTime(textBox2.Text));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var phone = new RegularExpressions(textBox1.Text);
            textBox1.BackColor = phone.isPhone(textBox1.Text) ? SystemColors.Window : SystemColors.Info;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var time = new RegularExpressions(textBox2.Text);
            textBox2.BackColor = time.isTime(textBox2.Text) ? SystemColors.Window : SystemColors.Info;
        }
    }
}
