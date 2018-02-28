using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesList
{
    public class FileData
    {
        public string Name { get; set; }
        public long Size { get; set; }

        public FileData(string name, long size)
        {
            Name = name;
            Size = size;
        }
    }
}
