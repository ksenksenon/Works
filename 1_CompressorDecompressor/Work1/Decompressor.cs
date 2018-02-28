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
    /// Декомпрессор
    /// </summary>
    public class Decompressor : FileProcessor
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="path">Путь для разжатия</param>
        public Decompressor(string path)
            : base(path)
        {
        }

        /// <summary>
        /// Выполняет разжатие файла
        /// </summary>
        /// <param name="file">Входной файл</param>
        /// <param name="directory">Директория для размещения разжатого файла</param>
        public override void Process(FileInfo file, FileProcessor directory)
        {
            using (var inFile = file.OpenRead())
            {
                string currentFile = file.Name;
                string originalName = currentFile.Remove(currentFile.Length - file.Extension.Length);
                var outFilePath = Path.Combine(directory.DirectoryPath, originalName);
                using (var outFile = File.Create(outFilePath))
                {
                    using (var decompress = new GZipStream(inFile, CompressionMode.Decompress))
                    {
                        decompress.CopyTo(outFile);
                    }
                }
            }
        }
    }
}
