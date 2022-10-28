using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Text.Json;
//using System.Text.Json.Serialization;
using System.IO;

namespace ConsoleApp2_2
{
    class GamersRootJson
    {
        public Dictionary<string, int> GamersDictionary { get; set; }
        string fileOfGamers = @"allgamers.json";

        public void DeserializationGamersFile()
        {
            GamersDictionary = new Dictionary<string, int>();
            using (StreamReader sr = new StreamReader(fileOfGamers, System.Text.Encoding.Default))
            {
                string s;
                while (sr.EndOfStream != true)
                {
                    s = sr.ReadLine();
                    GamersDictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, int>>(s);
                }
            }
        }

        public bool IsGamerExistInFile(int numberOfActiveGamer, Gamer gamer, GamersRootJson r)
        {
            bool result = false;
            string n = gamer.gamersSet.ElementAt(numberOfActiveGamer);
            int cnt = GamersDictionary.Count;
            if (cnt == 0)
            {
                result = false;
            }
            else
            {
                foreach (string key in GamersDictionary.Keys)
                {
                    if (GamersDictionary.ContainsKey(n))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }


        public void Output()
        {
            if (GamersDictionary.Count == 0)
            {
                Console.WriteLine("  игр еще не было");
            }
            else
            {
                foreach (var pair in GamersDictionary)
                {
                    Console.WriteLine("  имя:{0}, счет:{1}", pair.Key, pair.Value);
                }
            }
        }

        public void OutputActiveGamers(Gamer gamer)
        {
            int num = 0;
            foreach (var v in gamer.gamersSet)
            {
                if (GamersDictionary.ContainsKey(v))
                {
                    Console.WriteLine("  имя:{0}, счет:{1}", v, GamersDictionary[v]);
                }
                else
                {
                    Console.WriteLine($"  имя:{v}, счет:0");
                }
            }
        }


        public void SaveGameResults()
        {

            using (StreamWriter sw = new StreamWriter(fileOfGamers, true, System.Text.Encoding.Default))
            {
                string json = JsonSerializer.Serialize(GamersDictionary);
                sw.WriteLine(json);
            }
            GamersDictionary.Clear();
        }
    }
}
