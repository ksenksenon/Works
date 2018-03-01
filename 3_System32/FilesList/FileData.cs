using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FilesList
{
    [Serializable]
    public class FileData
    {
        public string Name { get; set; }
        public long Size { get; set; }

        public FileData(string name, long size)
        {
            Name = name;
            Size = size;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Name, Size);
        }
    }
}
