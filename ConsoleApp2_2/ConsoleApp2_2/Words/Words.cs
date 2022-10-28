using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace ConsoleApp2_2
{
    abstract class Words
    {
        public string Word { get; set; }
        public HashSet<string> words = new HashSet<string>();
        public virtual void AddWord()
        {
            words.Add(Word);
        }

        public virtual void OutputWords()
        {
            foreach (string value in words)
                Console.WriteLine(value);
        }
    }
}
