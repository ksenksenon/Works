using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace FilesList
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = @"C:\Windows\System32";
            ListOfFiles list = new ListOfFiles(directoryPath);
            list.GetFiles();

            list.FilesList.Sort(delegate (FileData file1, FileData file2)
            {
                if (file1.Size == file2.Size) return 0;
                else if (file1.Size < file2.Size) return 1;
                else if (file1.Size > file2.Size ) return -1;
                else return file1.Size.CompareTo(file2.Size);
            });
            list.PrintFiles();

            ListOfFiles sortList = new ListOfFiles();
            int count = 5;
            for (int i = 0; i < count; i++)
            {
                sortList.FilesList.Add(list.FilesList[i]);
            }
            Console.WriteLine("\n5 файлов с наибольшим размером:");
            sortList.PrintFiles();

            sortList.FilesList.Sort(delegate (FileData file1, FileData file2)
            {
                if (file1.Name == null && file2.Name == null) return 0;
                else if (file1.Name == null) return -1;
                else if (file2.Name == null) return 1;
                else return file1.Name.CompareTo(file2.Name);
            });
            Console.WriteLine("\nСортировка по имени:");
            sortList.PrintFiles();

            Console.ReadKey();
        }
    }
}
