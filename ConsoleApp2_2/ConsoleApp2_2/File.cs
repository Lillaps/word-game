using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.IO;

namespace ConsoleApp2_2
{
    class File
    {
        string fileOfGamers = @"allgamers.json";

        public void CreateFile()
        {
            FileInfo fi1 = new FileInfo(fileOfGamers);
            if (!(fi1.Exists))
            {
                using (StreamWriter sw = fi1.CreateText()) ;
            }
        }

        public void DeletionFile()
        {
            FileInfo fileInf = new FileInfo(fileOfGamers);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
        }
    }
}
