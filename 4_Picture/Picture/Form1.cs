using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Picture
{
    public partial class Form1 : Form
    {
        static string path = @"C:\dir1\image.png";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Picture picture = new Picture(textBox1.Text, 0.0f);
            pictureBox1.Image = picture.Image;
            picture.SaveToFile(path);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            var trackBar = sender as TrackBar;
            Picture picture = new Picture(textBox1.Text, (float)trackBar.Value);
            pictureBox1.Image = picture.Image;
        }


    }
}
