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
    /// Компрессор
    /// </summary>
    public class Compressor : FileProcessor
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="path">Путь для сжатия</param>
        public Compressor(string path)
            : base(path)
        {
        }

        /// <summary>
        /// Выполняет сжатие файла
        /// </summary>
        /// <param name="file">Входной файл</param>
        /// <param name="directory">Директория для размещения сжатого файла</param>
        public override void Process(FileInfo file, FileProcessor directory)
        {
            using (FileStream inFile = file.OpenRead())
            {
                var outFilePath = Path.Combine(directory.DirectoryPath, file.Name);
                using (var outFile = File.Create(outFilePath + ".gz"))
                {
                    using (var compress = new GZipStream(outFile, CompressionMode.Compress))
                    {
                        inFile.CopyTo(compress);
                    }
                }
            }
        }
    }
}
