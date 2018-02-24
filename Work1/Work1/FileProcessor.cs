using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Work1
{
    /// <summary>
    /// Процесс
    /// </summary>
    public abstract class FileProcessor
    {
        /// <summary>
        /// Возвращает путь к папке
        /// </summary>
        public string DirectoryPath { get; set; }

        /// <summary>
        /// Возвращает список файлов в папке
        /// </summary>
        public IList<FileInfo> FilesList { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="path">Путь к папке</param>
        public FileProcessor(string path)
        {
            DirectoryPath = path;
            FilesList = new List<FileInfo>();
        }

        /// <summary>
        /// Считывает файлы из директории
        /// </summary>
        public void GetFiles()
        {
            DirectoryInfo directory = new DirectoryInfo(DirectoryPath);
            foreach (FileInfo file in directory.GetFiles())
            {
                FilesList.Add(file);
            }
        }

        /// <summary>
        /// Очищает список файлов в директории
        /// </summary>
        public void ListClear()
        {
            FilesList.Clear();
        }

        /// <summary>
        /// Выполняет процесс
        /// </summary>
        /// <param name="file">Входной файл</param>
        /// <param name="directory">Директория для размещения выходного файла</param>
        public abstract void Process(FileInfo file, FileProcessor directory);
    }
}
