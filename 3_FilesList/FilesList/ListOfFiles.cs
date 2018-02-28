using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FilesList
{
    public class ListOfFiles
    {
        public string DirectoryPath { get; set; }
        public List<FileData> FilesList { get; set; }

        public ListOfFiles(string path)
        {
            DirectoryPath = path;
            FilesList = new List<FileData>();
        }

        public ListOfFiles()
        {
            FilesList = new List<FileData>();
        }

        public void GetFiles()
        {
            DirectoryInfo directory = new DirectoryInfo(DirectoryPath);
            foreach (var file in directory.GetFiles())
            {
                FilesList.Add(new FileData(file.Name, file.Length));
            }
        }

        public void PrintFiles()
        {
            foreach (var file in FilesList)
            {
                Console.WriteLine(string.Format("{0}    {1}", file.Name, file.Size));
            }
        }
    }
}
