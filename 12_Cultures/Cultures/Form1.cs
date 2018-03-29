using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace Cultures
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetCultures();
        }

        void GetCultures()
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.UserCustomCulture | CultureTypes.SpecificCultures);
            foreach (var culture in cultures)
            {
                listBox1.Items.Add(new CultureListBoxData(culture)); // 
            }
        }


        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            CultureInfo ci = ((CultureListBoxData)listBox1.SelectedItem).Culture;
            textBox1.Text = dt.ToString("F", ci);

            DriveInfo di = new DriveInfo(@"C:\");
            double free = (double)di.AvailableFreeSpace / 1024.0 / 1024.0;
            textBox2.Text = free.ToString(ci);
        }

    }

    public class CultureListBoxData
    {
        private CultureInfo _Culture;

        public CultureListBoxData(CultureInfo culture)
        {
            _Culture = culture;
        }

        public CultureInfo Culture
        {
            get
            {
                return _Culture;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", _Culture.DisplayName, _Culture.Name);
        }
    }
}
