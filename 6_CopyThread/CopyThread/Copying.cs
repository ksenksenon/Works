using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CopyThread
{
    public class Copying
    {
        public string InPath { get; set; }
        public string OutPath { get; set; }

        public Copying(string inPath, string outPath)
        {
            InPath = inPath;
            OutPath = outPath;
        }

        public void FileCopying()
        {
           
            File.Copy(InPath,OutPath);
        }

    }
}
