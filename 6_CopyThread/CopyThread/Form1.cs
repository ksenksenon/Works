using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Configuration;



namespace CopyThread
{
    public partial class Form1 : Form
    {
        static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        string _Path1 = config.AppSettings.Settings["path1"].Value;
        string _Path2 = config.AppSettings.Settings["path2"].Value;
        //string _Path1 = ConfigurationSettings.AppSettings["path1"];
        //string _Path2 = ConfigurationSettings.AppSettings["path2"];
        //private string _Path1 = @"C:\Users\k.kataeva\dir1\file.ISO";
        //private string _Path2 = @"C:\Users\k.kataeva\dir2\file.ISO";
        private SynchronizationContext _Context;

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            //_Context = SynchronizationContext.Current;
            //Thread theThread = new Thread(FileCopying);

            //theThread.Start();
            //button1.Enabled = false;

            //File.Copy(_Path1, _Path2);

            button1.Enabled = false;
            await FileCopyAsync(_Path1, _Path2);
            button1.Enabled = true;
        }

        private async Task FileCopyAsync(string path1, string path2)
        {
            using (var fs1 = new FileStream(path1, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var fs2 = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                var buffer = new byte[65536];
                int bytesRead = 0;
                do
                {
                    bytesRead = await fs1.ReadAsync(buffer, 0, buffer.Length);
                    await fs2.WriteAsync(buffer, 0, bytesRead);
                } while (bytesRead == buffer.Length);
            }
        }

        public void FileCopying()
        {
            if (File.Exists(_Path2)) File.Delete(_Path2);
            File.Copy(_Path1, _Path2);
            _Context.Post(Finish, null);
        }

        private void Finish(object state)
        {
            button1.Enabled = true;
        }
    }
}
