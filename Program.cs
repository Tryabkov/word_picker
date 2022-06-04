using MySql.Data.MySqlClient;
using System.Data;
<<<<<<< HEAD

namespace word_picker
{
    internal class Program
    {
        static void Main()
        {
            var foundWords = SearchWords();
=======
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace word_picker
{
    [MemoryDiagnoser]
    [RankColumn]

    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run(typeof(Program2).Assembly);
        }
    }
    internal class Program2
    {
        static void Main()
        {
            var apsentLetters = new List<char>();
            var PresentLetters = new List<char>();
            int wordLenth = 0;
            var wordDict = Intro(ref apsentLetters, ref PresentLetters, ref wordLenth);
            List<string> foundWords = SearchWords(apsentLetters, PresentLetters, wordLenth, wordDict);
>>>>>>> fc463bc0cca6866caec15c285e7353e975ed8942
            Console.WriteLine();
            for (int i = 0; i < foundWords.Count; i++)
            {
                Console.WriteLine(foundWords[i]);
            }          
            Console.ReadKey();
<<<<<<< HEAD
        }

        static private List<string> SearchWords()
        {
            Word word = GetInformation();

            List<char> apsentLetters = word.apsentLetters;
            List<char> PresentLetters = word.presentLetters;
            int wordLength = word.length;
            Dictionary<int, char> wordDict = word.lettersOrder;

=======

        }

        [Benchmark]
        static private List<string> SearchWords(List<char> apsentLetters, List<char> PresentLetters, int wordLenth, Dictionary<int, char> wordDict)
        {
>>>>>>> fc463bc0cca6866caec15c285e7353e975ed8942
            List<string> foundWords = new List<string>();
            List<string> wordsAllowedByLenth = new List<string>();
            List<string> wordsAllowedByLenthAndHasLetters = new List<string>();
            List<string> wordsAllowedByLenthAndRightLetters = new List<string>();
            List<string> words = CreateWordArray();
            bool isContain = false;

<<<<<<< HEAD

            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Length == wordLength)
=======
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Length == wordLenth)
>>>>>>> fc463bc0cca6866caec15c285e7353e975ed8942
                {
                    wordsAllowedByLenth.Add(words[i]);
                }
            }

            for (int i = 0; i < wordsAllowedByLenth.Count; i++)
            {
                isContain = true;
                for (int j = 0; j < PresentLetters.Count; j++)
                {
                    if (wordsAllowedByLenth[i].Contains(PresentLetters[j]))
                    {
                        isContain = true;
                    }
                    else
                    {
                        isContain = false;
                        break;
                    }
                }
                if (isContain)
                {
                    wordsAllowedByLenthAndHasLetters.Add(wordsAllowedByLenth[i]);
                }
            }

            for (int i = 0; i < wordsAllowedByLenthAndHasLetters.Count; i++)
            {
                isContain = false;
                for (int j = 0; j < apsentLetters.Count; j++)
                {
                    if (!wordsAllowedByLenthAndHasLetters[i].Contains(apsentLetters[j]))
                    {
                        isContain = false;
                    }
                    else
                    {
                        isContain = true;
                        break;
                    }
                }
                if (!isContain)
                {
                    wordsAllowedByLenthAndRightLetters.Add(wordsAllowedByLenthAndHasLetters[i]);
                }
            }

            if (wordDict.Count != 0)
            {
                for (int i = 0; i < wordsAllowedByLenthAndRightLetters.Count; i++)
                {
                    var valuesArr = wordDict.Values.ToArray();
                    var keysArr = wordDict.Keys.ToArray();
                    for (int j = 0; j < wordDict.Count; j++)
                    {
                        if (wordsAllowedByLenthAndRightLetters[i][keysArr[j]] == valuesArr[j])
                        {
                            isContain = true;
                        }
                        else
                        {
                            isContain = false;
                            break;
                        }
                    }
                    if (isContain)
                    {
                        foundWords.Add(wordsAllowedByLenthAndRightLetters[i]);
                    }
                }
            }
            else
            {
                foundWords = wordsAllowedByLenthAndRightLetters;
            }

            return foundWords;
        }

        static private List<string> CreateWordArray()
        {
<<<<<<< HEAD
            string script = "SELECT * FROM nouns";
            DataTable table = RequestToDB(script);
            DataRow[] rows = table.Select();
            List<string> words = new List<string>(rows.Count());

=======
            List<string> words = new List<string>();

            string script = "SELECT * FROM nouns";
            DataTable table = RequestToDB(script);
            DataRow[] rows = table.Select();
>>>>>>> fc463bc0cca6866caec15c285e7353e975ed8942
            for (int i = 0; i < rows.Length; i++)
            {
                words.Add(rows[i].ItemArray[1].ToString());
            }
<<<<<<< HEAD
=======


>>>>>>> fc463bc0cca6866caec15c285e7353e975ed8942
            return words;
        }

        static private DataTable RequestToDB(string script)
        {
            DB DataBase = new DB();
            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand(script, DataBase.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(table);
            DataBase.OpenConnection();
            DataBase.CloseConnection();
            return table;
        }

<<<<<<< HEAD
        static private Dictionary<int, char> RequestLettersOrder(int wordLength, string description)
        {
            var lettersOrder = new Dictionary<int, char>();
            string String;
            Console.WriteLine(description);

            for (int i = 0; i < wordLength; i++)
            {
                Console.Write(i + 1 + " - ");
                String = Console.ReadLine();
                if (!string.IsNullOrEmpty(String))
                {
                    if (char.IsLetter(String[0]))
                    {
                        lettersOrder.Add(i, String[0]);
                    }
                    else RequestLettersOrder(wordLength, description);
                }
            }
            return lettersOrder;            
        }

        static private string RequestLetters(string description)
        {
            Console.Write(description);
            return Console.ReadLine().ToLower();
        }

        static private int RequestNumber(string descripton)
        {
            Console.Write(descripton);
            int result;
            if (int.TryParse(Console.ReadLine(), out result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("\nВведите корректное значение");
                return RequestNumber(descripton);
            }            
        }

        static private void WriteChars(List<char> array, string sourse)
        {
            foreach (var item in sourse)
            {
                array.Add(item);
            }
        }

        static private Word GetInformation()
        {
            var word = new Word();

            Console.WriteLine("Это подбиратель слов!\n\nВведите буквы(без пробелов и запятых) которые");
            WriteChars(word.apsentLetters, RequestLetters("Отсутствуют в слове(серые): "));
            WriteChars(word.presentLetters, RequestLetters("\nЕсть в слове, но неизвестно где(белые): "));
            word.length = RequestNumber("\nВведите длину слова: ");
            word.lettersOrder = RequestLettersOrder(word.length , "\nНиже нужно записать буквы, которые стоят в слове в определённом месте(желтые)\n");            
            return word;
=======
        static private bool IsChar(string String)
        {
            if (String.Length == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static private string RequestLetters(string text)
        {
            Console.Write(text);
            return Console.ReadLine().ToLower();
        }

        static private int RequestNumber(string text)
        {
            Console.Write(text);
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("\nВведите корректное значение");
                return RequestNumber(text);
            }
            
        }

        static private Dictionary<int, char> Intro(ref List<char> apsentLetters, ref List<char> PresentLetters, ref int wordLenth)
        {
            var wordDict = new Dictionary<int, char>();
            string tempString;
            Console.WriteLine("Это подбиратель слов!\n\nВведите буквы(без пробелов и запятых) которые");
            tempString = RequestLetters("Отсутствуют в слове(серые): ");
            if (tempString != null)
            {
                for (int i = 0; i < tempString.Length; i++)
                {
                    apsentLetters.Add(tempString[i]);
                }
            }

            tempString = RequestLetters("\nЕсть в слове, но неизвестно где(белые): ");
            if (tempString != null)
            {
                for (int i = 0; i < tempString.Length; i++)
                {
                    PresentLetters.Add(tempString[i]);
                }
            }

            wordLenth = RequestNumber("\nВведите длину слова: ");
            Console.WriteLine("\nНиже нужно записать буквы, которые стоят в слове в определённом месте(желтые)\n");
            for (int i = 1; i < wordLenth + 1; i++)
            {
                Console.Write(i + " - ");
                tempString = Console.ReadLine();
                if (tempString != "")
                {
                    var tempChar = tempString[0];
                    wordDict.Add(i - 1, tempChar);
                }
            }
            return wordDict;
>>>>>>> fc463bc0cca6866caec15c285e7353e975ed8942
        }
    }
}