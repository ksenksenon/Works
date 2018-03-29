using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;


namespace FilesList
{
    class Program
    {

        static void Method2(string directoryPath)
        {
            ListOfFiles list = new ListOfFiles(directoryPath);
            list.GetFiles();
            var sorted = list.FilesList
                .OrderByDescending((fd) => fd.Size)
                .Take(5)
                .OrderByDescending((fd) => fd.Name);
        }

        static void Method1(string directoryPath)
        {
            ListOfFiles list = new ListOfFiles(directoryPath);
            list.GetFiles();

            list.FilesList.Sort(delegate(FileData file1, FileData file2)
            {
                if (file1.Size < file2.Size)
                    return 1;
                if (file1.Size > file2.Size)
                    return -1;
                return 0;
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

            sortList.FilesList.Sort(delegate(FileData file1, FileData file2)
            {
                return file2.Name.CompareTo(file1.Name);
            });
            Console.WriteLine("\nСортировка по имени:");
            sortList.PrintFiles();

            //Serialize(sortList);
            //Deserialize();
            //SerializeAsCsv(sortList);
            //DeserializeAsCsv();

            //Excel COM
            //dynamic excelApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));

            //var workBook = excelApp.Workbooks.Add();
            //var workSheet = workBook.Worksheets[1];

            //workSheet.Cells[1, "A"] = "Name";
            //workSheet.Cells[1, "B"] = "Size";

            //for (int i = 0; i < sortList.FilesList.Count; i++)
            //{
            //    workSheet.Cells[i + 2, 1] = sortList.FilesList[i].Name;
            //    workSheet.Cells[i + 2, 2] = sortList.FilesList[i].Size;
            //}

            //excelApp.Visible = true;
            //excelApp.UserControl = true;
        }

        static void Serialize(ListOfFiles list)
        {
            using (var fs = new FileStream("SerializedDate.Data", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, list);
            }
        }

        static ListOfFiles Deserialize()
        {
            ListOfFiles resList = new ListOfFiles();
            using (var fs = new FileStream("SerializedDate.Data", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                resList = (ListOfFiles)bf.Deserialize(fs);
            }
            return resList;
        }

        static void SerializeAsCsv(ListOfFiles list)
        {
            using (FileStream fs = new FileStream("Data.csv", FileMode.Create))
            {
                var bom = Encoding.UTF8.GetPreamble();
                for (int i = 0; i < bom.Length; i++)
                {
                    fs.WriteByte(bom[i]);
                }
            }

            using (var sw = new StreamWriter("Data.csv", true, Encoding.GetEncoding("UTF-8")))
            {
                string name;
                foreach(FileData file in list.FilesList)
                {
                    name = file.Name;
                    if (name.IndexOfAny(new char[] {'\"', ';'}) >= 0)
                    {
                        name = name.Replace("\"","\"\"");
                        name = string.Format("\"{0}\"", name);
                    }
                    sw.WriteLine(string.Format("{0};{1}", name, file.Size));
                }
            }
        }

        public static ListOfFiles DeserializeAsCsv()
        {
            ListOfFiles resList = new ListOfFiles();
            string line;
            string[] mas;
            using (var sw = new StreamReader("Data.csv"))
            {
               while ((line = sw.ReadLine())!= null)
                {
                    mas  = line.Split(';');
                    FileData f = new FileData(mas[0], int.Parse(mas[1]));
                    resList.FilesList.Add(f);
                }
            }
            return resList;
        }


        static void Main(string[] args)
        {
            string directoryPath = @"C:\Windows\System32";
            Method1(directoryPath);

            //Reflection
            Type myType = typeof(Program);
            MethodInfo[] methods = myType.GetMethods(BindingFlags.Static |  BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            Console.WriteLine("\nMethods: ");

            foreach (var m in methods)
            {
                Console.WriteLine(m.Name);
            }

            Console.WriteLine("Enter name of method: ");
            string name;
            name = Console.ReadLine();

            ConstructorInfo mConstructor = myType.GetConstructor(Type.EmptyTypes);
            object magicClassObject = mConstructor.Invoke(new object[] {});
            MethodInfo mi = typeof(Program).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            mi.Invoke(magicClassObject, new object[] { directoryPath });

            Console.ReadKey();
        }

    }
}