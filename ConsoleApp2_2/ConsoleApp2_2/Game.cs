using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using System.IO;

namespace ConsoleApp2_2
{
    enum OptionsNewGame
    {
        [Description("")]
        none,
        [Description("/yes")]
        yes,
        [Description("/no")]
        no
    }

    enum Comands
    {
        [Description("")]
        none,
        [Description("/finish")]
        finish,
        [Description("/words")]
        words,
        [Description("/score")]
        score,
        [Description("/all-score")]
        allscore
    }
    class Game
    {
        public void GetGame()
        {
            bool isActiveWordRight = true;
            bool isGameActive = true;
            int numberOfActiveGamer = 0;
            int quantityOfGamers = 0;

            File file = new File();
            file.CreateFile();

            GamersRootJson r = new GamersRootJson();
            r.DeserializationGamersFile();

            GetQuantityOfGamers(out quantityOfGamers);
            Console.Clear();
            Gamer gamer = new Gamer();
            GetNames(quantityOfGamers, numberOfActiveGamer, gamer);
            Console.Clear();

            Start();
            Console.WriteLine($"Игрок {gamer.gamersSet.ElementAt(numberOfActiveGamer)}, введите слово");
            string word;
            ActiveWord activeWord = new ActiveWord();
            FirstWord firstWord = new FirstWord();
            firstWord.Word = GetFirstWord();
            firstWord.AddWord();
            do
            {
                numberOfActiveGamer = GetNumberOfGamer(numberOfActiveGamer, quantityOfGamers);
                if (numberOfActiveGamer + 1 > quantityOfGamers)
                {
                    numberOfActiveGamer = 0;
                }
                Console.WriteLine($"игрок {gamer.gamersSet.ElementAt(numberOfActiveGamer)}, введите слово");
                int maxQuantityAttempts = 3;
                int numberAttempt = 0;
                do
                {
                    isActiveWordRight = true;
                    numberAttempt++;
                    word = GetActiveWord();
                    if (CheckCommand(word, activeWord, firstWord, ref quantityOfGamers, ref numberOfActiveGamer, gamer, r, ref isActiveWordRight) == false)
                    {
                        isActiveWordRight = false;
                        numberOfActiveGamer--;
                        break;
                    }
                    else if(isActiveWordRight == false)
                    {
                        
                            break;
                        
                    }
                    activeWord.Word = word;
                    isActiveWordRight = activeWord.IsWordRight(firstWord, activeWord);
                    if (isActiveWordRight == false)
                    {
                        if (maxQuantityAttempts - numberAttempt == 0)
                        {
                            Console.WriteLine($"игрок {gamer.gamersSet.ElementAt(numberOfActiveGamer)}, вы проиграли");
                            ProcessingOfTheLosingGamer(numberOfActiveGamer, ref quantityOfGamers, gamer, r);
                            numberOfActiveGamer--;
                        }
                        else
                        {
                            Console.WriteLine($"  у вас осталось попыток: {maxQuantityAttempts - numberAttempt}");
                        }
                    }
                }
                while (isActiveWordRight != true & numberAttempt < maxQuantityAttempts);

                activeWord.AddWord();
                if (quantityOfGamers == 1)
                {
                    numberOfActiveGamer = 0;
                    Console.WriteLine($"Победу одержал игрок {gamer.gamersSet.ElementAt(numberOfActiveGamer)}");
                    isGameActive = false;
                    if (r.IsGamerExistInFile(numberOfActiveGamer, gamer, r) == false)
                    {
                        r.GamersDictionary.Add(gamer.gamersSet.ElementAt(numberOfActiveGamer), 1);
                    }
                    else
                    {
                        if (r.GamersDictionary.ContainsKey(gamer.gamersSet.ElementAt(numberOfActiveGamer)))
                        {
                            r.GamersDictionary[gamer.gamersSet.ElementAt(numberOfActiveGamer)]++;
                        }
                    }
                    file.DeletionFile();
                    file.CreateFile();
                    r.SaveGameResults();
                    //file.DeletionFile();
                }
            }
            while (isGameActive == true);
            Newgame(numberOfActiveGamer, gamer);
        }

        public void GetQuantityOfGamers(out int quantityOfGamers)
        {
            bool result = false;
            quantityOfGamers = 0;
            int maxQuantity = 5;
            int minQuantity = 2;
            while (result == false)
            {
                double q = 0;
                Console.WriteLine("введите количество игроков от двух до пяти включительно");
                if (double.TryParse(Console.ReadLine(), out q))
                {
                    quantityOfGamers = (int)q;
                    if (quantityOfGamers > maxQuantity || quantityOfGamers < minQuantity)
                    {
                        Console.WriteLine(" неверное количество игроков, введите еще раз");
                        GetQuantityOfGamers(out quantityOfGamers);
                        break;
                    }
                    result = true;
                }
                else
                {
                    Console.WriteLine(" введите правильно, это не число");
                    GetQuantityOfGamers(out quantityOfGamers);
                    break;
                }
            }
        }

        public int GetNumberOfGamer(int numberOfActiveGamer, int quantityOfGamers)
        {
            //numberOfActiveGamer = numberOfActiveGamer == quantityOfGamers ? (1) : (numberOfActiveGamer++);
            if (numberOfActiveGamer == quantityOfGamers)
            {
                numberOfActiveGamer = 1;
            }
            else
            {
                numberOfActiveGamer++;
            }
            return numberOfActiveGamer;
        }

        public void GetNames(int quantityOfGamers, int numberOfActiveGamer, Gamer gamer)
        {
            int maxLength = 15;
            int minLength = 1;
            for (int i = 0; i < quantityOfGamers; i++)
            {
                numberOfActiveGamer = GetNumberOfGamer(numberOfActiveGamer, quantityOfGamers);
                Console.WriteLine($"введите имя игрока №{numberOfActiveGamer}");
                string name = Console.ReadLine();
                bool isFound = gamer.gamersSet.Contains(name);
                if (isFound == false)
                {
                    if (name.Length > maxLength || name.Length < minLength)
                    {
                        Console.WriteLine("  имя должно быть от 1 до 15 символов, введите еще раз");
                        numberOfActiveGamer--;
                        --i;
                    }
                    else
                    {
                        gamer.gamersSet.Add(name);
                    }
                }
                else
                {
                    Console.WriteLine("  имена не должны повторяться, введите еще раз");
                    numberOfActiveGamer--;
                    --i;
                }
            }
            numberOfActiveGamer = 0;
        }

        public string GetFirstWord()
        {
            int maxLength = 30;
            int minLength = 8;
            bool result = false;
            string word;
            do
            {
                word = Console.ReadLine();
                if (word.Length < minLength || word.Length > maxLength)
                {
                    Console.WriteLine("  слово введено неверно! попробуйте еще раз");
                }
                else
                {
                    result = true;
                }
            }
            while (result == false);
            return word;
        }

        public string GetActiveWord()
        {
            string word2 = Console.ReadLine();
            return word2;
        }

        public void ProcessingOfTheLosingGamer(int numberOfActiveGamer, ref int quantityOfGamers, Gamer gamer, GamersRootJson r)
        {
            gamer.Name = gamer.gamersSet.ElementAt(numberOfActiveGamer);
            if (r.IsGamerExistInFile(numberOfActiveGamer, gamer, r) == false)
            {
                r.GamersDictionary.Add(gamer.gamersSet.ElementAt(numberOfActiveGamer), 0);
            }
            string a = gamer.gamersSet.ElementAt(numberOfActiveGamer);
            gamer.gamersSet.RemoveWhere(s => s == a);
            quantityOfGamers--;
        }

        public bool CheckCommand(string word, ActiveWord activeWord, FirstWord firstWord, ref int quantityOfGamers, ref int numberOfActiveGamer, Gamer gamer, GamersRootJson r, ref bool isActiveWordRight)
        {
            bool check = true;
            Comands result = Comands.none;
            foreach (Comands option in Enum.GetValues(typeof(Comands)))
            {
                string desc = option.GetType().GetMember(option.ToString())[0].GetCustomAttribute<DescriptionAttribute>().Description;
                if (desc == word)
                {
                    result = option;
                    break;
                    check = false;
                }
            }
            if (result == Comands.finish)
            {
                Console.WriteLine($"игрок {gamer.gamersSet.ElementAt(numberOfActiveGamer)}, вы проиграли");
                ProcessingOfTheLosingGamer(numberOfActiveGamer, ref quantityOfGamers, gamer, r);
                numberOfActiveGamer--;
                isActiveWordRight = false;
                check = true;
            }
            else if (result == Comands.words)
            {
                firstWord.OutputWords();
                activeWord.OutputWords();
                check = false;
            }
            else if (result == Comands.score)
            {
                r.OutputActiveGamers(gamer);
                check = false;
            }
            else if (result == Comands.allscore)
            {
                r.Output();
                check = false;
            }
            else
            {
                if (word.Contains('/'))
                {
                    Console.WriteLine("  команда введена некорректно, попробуйте еще раз");
                    check = false;
                }
            }
            return check;
        }

        public void Start()
        {
            Console.WriteLine("игра началась");
            Console.WriteLine("вызовы доп. команд: /finish-невозможность ввести слово, /words-все слова,");
            Console.WriteLine("\t\t/score-счет текущий, /all-score-счет всех игроков");
            Console.WriteLine("");
        }

        public void Newgame(int numberOfActiveGamer, Gamer gamer)
        {
            Console.WriteLine("");
            Console.WriteLine("  Хотите сыграть еще раз? (/yes, /no)");
            string repeat = Console.ReadLine();
            OptionsNewGame result = OptionsNewGame.none;
            foreach (OptionsNewGame option in Enum.GetValues(typeof(OptionsNewGame)))
            {
                string desc = option.GetType().GetMember(option.ToString())[0].GetCustomAttribute<DescriptionAttribute>().Description;
                if (desc == repeat)
                {
                    result = option;
                    break;
                }
            }
            if (result == OptionsNewGame.yes)
            {
                Console.Clear();
                Game game = new Game();
                game.GetGame();
            }
            else if (result == OptionsNewGame.no)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("  введите правильно");
                Newgame(numberOfActiveGamer, gamer);
            }
        }
    }
}
