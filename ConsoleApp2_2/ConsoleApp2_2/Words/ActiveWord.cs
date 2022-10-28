using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2_2
{
    class ActiveWord:Words
    {
        static Dictionary<char, int> CharCount(string str) => str.ToLower().GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        public bool IsWordRight(FirstWord baseWord, ActiveWord activeWord)
        {
            bool isFound = words.Contains(Word);
            var basic = CharCount(baseWord.Word);
            var active = CharCount(activeWord.Word);
            var result = active.All(x => basic.ContainsKey(x.Key) && basic[x.Key] >= x.Value);
            if (activeWord.Word.Length < 2 || result == false)
            {
                Console.WriteLine("  слово введено неверно");
                result = false;
            }
            
            else if (isFound==true)
            {
                Console.WriteLine("  это слово было введено ранее, придумайте другое");
                result = false;
            }
            return result;
        }
    }
}
