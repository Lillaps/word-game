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
    class Gamer
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public HashSet<string> gamersSet = new HashSet<string>();
    }
}
