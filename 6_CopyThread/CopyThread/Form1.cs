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
using System.Diagnostics;


namespace CopyThread
{
    public partial class Form1 : Form, IProgress<double>
    {
        static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private string _Path1 = config.AppSettings.Settings["path1"].Value;
        private string _Path2 = config.AppSettings.Settings["path2"].Value;

        private SynchronizationContext _Context;

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //контекст синхронизации
            //_Context = SynchronizationContext.Current;
            //Thread theThread = new Thread(FileCopying);
            //theThread.Start();
            //button1.Enabled = false;
            //File.Copy(_Path1, _Path2);

            button1.Enabled = false;
            EventLog DemoLog = new EventLog("Application");
            DemoLog.Source = "DemoApp";
            DemoLog.WriteEntry("Start copying", EventLogEntryType.Information);
            await FileCopyAsync(_Path1, _Path2);
            button1.Enabled = true;
            DemoLog.WriteEntry("Stop copying", EventLogEntryType.Information);
        }

        /// <summary>
        /// Асинхронное копирование файла
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        private async Task FileCopyAsync(string path1, string path2)
        {
            using (var fs1 = new FileStream(path1, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var fs2 = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                var buffer = new byte[65536];
                long bytesCount = fs1.Length;
                long bytesCopied = 0;
                this.Report(0);
                int bytesRead = 0;
                do
                {
                    bytesRead = await fs1.ReadAsync(buffer, 0, buffer.Length);
                    await fs2.WriteAsync(buffer, 0, bytesRead);
                    bytesCopied += bytesRead;
                    this.Report((double)bytesCopied / (double)bytesCount * 100.0);
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

        /// <summary>
        /// Устанавливает прогресс копирования файла
        /// </summary>
        /// <param name="value">значение прогресса в процентах</param>
        public void Report(double value)
        {
            progressBar1.Value = Convert.ToInt32(value);
        }
    }
}
