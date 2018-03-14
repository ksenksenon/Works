using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;


namespace Threads
{
    class Program
    {
        static void GetFiles(string directoryPath, List<string> names)
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            foreach (FileInfo file in directory.GetFiles())
            {
                names.Add(file.FullName);
            }
        }

        static void LoadAndSort(List<string> names)
        {
             foreach (var name in names)
            {
                List<string> strings = new List<string>();
                FileSorter newFile = new FileSorter(name, strings);

                DateTime startLoad = DateTime.Now;
                newFile.Load();
                DateTime stopLoad = DateTime.Now;

                DateTime startSort = DateTime.Now;
                newFile.Sort();
                DateTime stopSort = DateTime.Now;

                Console.WriteLine(string.Format("File: {0}  Load: {1}   Sort: {2}", name, (stopLoad - startLoad).TotalMilliseconds,(stopSort - startSort).TotalMilliseconds));
             }
        }

        static void ThreadingLoadAndSort(object parameter)
        {
            LoadAndSort(parameter as List<string>);
        }

        static void FileLoadAndSort(FileSorter file)
        {
            DateTime startLoad = DateTime.Now;
            file.Load();
            DateTime stopLoad = DateTime.Now;

            DateTime startSort = DateTime.Now;
            file.Sort();
            DateTime stopSort = DateTime.Now;
            Console.WriteLine(string.Format("File: {0}  Load: {1}   Sort: {2}", file.FilePath, (stopLoad - startLoad).TotalMilliseconds, (stopSort - startSort).TotalMilliseconds));
        }

        static void ThreadingFileLoadAndSort(object parameter)
        {
            FileLoadAndSort(parameter as FileSorter);
        }

        static void OnlyLoad(List<string> names)
        {
            foreach (var name in names)
            {
                List<string> strings = new List<string>();
                FileSorter newFile = new FileSorter(name, strings);
                DateTime startLoad = DateTime.Now;
                newFile.Load();
                DateTime stopLoad = DateTime.Now;
                Console.WriteLine(string.Format("File: {0}  Load: {1}", name, (stopLoad - startLoad).TotalMilliseconds));
            }
        }

        static void ThreadingLoad(object parameter)
        {
            OnlyLoad(parameter as List<string>);
        }

        static void OnlySort(FileSorter file)
        {
            DateTime startSort = DateTime.Now;
            file.Sort();
            DateTime stopSort = DateTime.Now;
            Console.WriteLine(string.Format("File: {0}  Sort: {1}", file.FilePath, (stopSort - startSort).TotalMilliseconds));
        }

        static void ThreadingSort(object parameter)
        {
            OnlySort(parameter as FileSorter);
        }

        static void Main(string[] args)
        {
            string path = @"C:\Users\k.kataeva\threads";
            List<string> names = new List<string>();
            GetFiles(path, names);

            List<Thread> threadsList = new List<Thread>();

            Console.WriteLine("-------------------Method 1-------------------");
            DateTime start = DateTime.Now;

            var operation1 = new ParameterizedThreadStart(ThreadingLoadAndSort);
            Thread theThread = new Thread(operation1);
            theThread.Start(names);
            theThread.Join();

            DateTime stop = DateTime.Now;
            
            Console.WriteLine(string.Format("All files: {0}", (stop - start).TotalMilliseconds));

            Console.WriteLine("-------------------Method 2-------------------");
            DateTime start1 = DateTime.Now;

            var operation2 = new ParameterizedThreadStart(ThreadingFileLoadAndSort);

            foreach (var name in names)
            {
                List<string> strings = new List<string>();
                FileSorter newFile = new FileSorter(name, strings);
                Thread newThread = new Thread(operation2);
                newThread.Start(newFile);
                threadsList.Add(newThread);
            }
            foreach (var t in threadsList)
            {
                t.Join();
            }
            DateTime stop1 = DateTime.Now;
            Console.WriteLine(string.Format("All files: {0}", (stop1 - start1).TotalMilliseconds));

            Console.WriteLine("-------------------Method 3-------------------");
            DateTime start2 = DateTime.Now;

            var operation3 = new ParameterizedThreadStart(ThreadingLoad);
            Thread theThread3 = new Thread(operation3);
            theThread3.Start(names);
            theThread3.Join();

            var operation4 = new ParameterizedThreadStart(ThreadingSort);
            threadsList.Clear();
            foreach (var name in names)
            {
                List<string> strings = new List<string>();
                FileSorter newFile = new FileSorter(name, strings);
                newFile.Load();
                Thread newThread = new Thread(operation4);
                newThread.Start(newFile);
                threadsList.Add(newThread);
            }
            foreach (var t in threadsList)
            {
                t.Join();
            }
            DateTime stop2 = DateTime.Now;
            Console.WriteLine(string.Format("All files: {0}", (stop2 - start2).TotalMilliseconds));
            Console.ReadKey();
        }
    }
}
