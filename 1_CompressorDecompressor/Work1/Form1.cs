using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Work1
{
    public partial class Form1 : Form
    {
        static string path1 = @"C:\Users\k.kataeva\dir1";
        static string path2 = @"C:\Users\k.kataeva\dir2";

        Compressor directory1 = new Compressor(path1);
        Decompressor directory2 = new Decompressor(path2);

        IList<FileInfo> filesList1 = new List<FileInfo>();
        IList<FileInfo> filesList2 = new List<FileInfo>();

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;
        }

        /// <summary>
        /// Обновление listBox
        /// </summary>
        /// <param name="directory">путь к директории</param>
        /// <param name="listbox">поле для вывода списка файлов</param>
        /// <param name="path">поле для вывода пути к директории</param>
        /// <param name="files">список файлов в директории</param>
        private void UpdateList(FileProcessor directory, ListBox listbox, TextBox path, IList<FileInfo> files)
        {
            listbox.Items.Clear();
            path.Text = directory.DirectoryPath;
            directory.GetFiles();
            files = directory.FilesList;
            foreach (FileInfo file in files)
            {
                listbox.Items.Add(file.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e) //список файлов
        {
            if (!Directory.Exists(path2))
            {
                DirectoryInfo directory = Directory.CreateDirectory(path2);
            }
            listBox1.Items.Clear();
            directory1.GetFiles();
            filesList1 = directory1.FilesList;
            textBox1.Text = directory1.DirectoryPath;
            foreach (FileInfo file in filesList1)
            {
                listBox1.Items.Add(file.Name);
            }
            listBox2.Items.Clear();
            directory2.GetFiles();
            filesList2 = directory2.FilesList;
            textBox2.Text = directory2.DirectoryPath;
            foreach (FileInfo file in filesList2)
            {
                listBox2.Items.Add(file.Name);
            }
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e) //сжать
        {
            foreach (FileInfo file in filesList1)
            {
                directory1.Process(file, directory2);
                file.Delete();
            }
            directory1.ListClear();
            UpdateList(directory1, listBox1, textBox1, filesList1);
            UpdateList(directory2, listBox2, textBox2, filesList2);
        }

        private void button3_Click(object sender, EventArgs e) //восстановить
        {
            foreach (FileInfo file in filesList2)
            {
                directory2.Process(file, directory1);
                file.Delete();
            }
            directory2.ListClear();
            UpdateList(directory1, listBox1, textBox1, filesList1);
            UpdateList(directory2, listBox2, textBox2, filesList2);
        }
    }
}
