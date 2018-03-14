using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Threads
{
    public class FileSorter
    {
        public string FilePath { get; set; }
        public List<string> StringList { get; set; }

        public FileSorter(string path, List<string> str)
        {
            FilePath = path;
            StringList = str;
        }

        public void Load()
        {
            string line;
            using (var sw = new StreamReader(FilePath))
            {
                while ((line = sw.ReadLine()) != null)
                {
                    StringList.Add(line);
                }
            }
        }

        public void Sort()
        {
            var sorted = StringList
                .OrderBy((line) => line);
            sorted.ToArray();
        }

    }
}
