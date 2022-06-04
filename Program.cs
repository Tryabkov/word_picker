using MySql.Data.MySqlClient;
using System.Data;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;

namespace word_picker
{
    internal class Program
    {
        static void Main()
        {
            var foundWords = SearchWords();
            Console.WriteLine();
            for (int i = 0; i < foundWords.Count; i++)
            {
                Console.WriteLine(foundWords[i]);
            }          
            Console.ReadKey();
        }

        static private List<string> SearchWords()
        {
            Word word = GetInformation();

            List<char> apsentLetters = word.apsentLetters;
            List<char> PresentLetters = word.presentLetters;
            int wordLength = word.length;
            Dictionary<int, char> wordDict = word.lettersOrder;

            List<string> foundWords = new List<string>();
            List<string> wordsAllowedByLenth = new List<string>();
            List<string> wordsAllowedByLenthAndHasLetters = new List<string>();
            List<string> wordsAllowedByLenthAndRightLetters = new List<string>();
            List<string> words = CreateWordArray();
            bool isContain = false;


            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Length == wordLength)
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
            string script = "SELECT * FROM nouns";
            DataTable table = RequestToDB(script);
            DataRow[] rows = table.Select();
            List<string> words = new List<string>(rows.Count());

            for (int i = 0; i < rows.Length; i++)
            {
                words.Add(rows[i].ItemArray[1].ToString());
            }
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
        }
    }
}