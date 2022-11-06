using System.Data;
using MySql.Data.MySqlClient;

namespace word_picker
{
    internal class Program
    {
        static void Main()
        {
            while (true)
            {
                var foundWords = SearchWords();
                Console.WriteLine();
                for (int i = 0; i < foundWords.Count; i++)
                {
                    Console.WriteLine(foundWords[i]);
                }
                Console.WriteLine("\n\n\n-----------------------------------------\n\n\n");
                Console.ReadKey();
            }
        }

        static private List<string> SearchWords()
        {
            Word word = GetInformation();
            List<string> words = CreateWordArray(word.length);

            for (int i = 0; i < words.Count; i++)
            {
                foreach (var apcentLetter in word.apsentLetters)
                {
                    if (words[i].Contains(apcentLetter))
                    {
                        words.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }

            for (int i = 0; i < words.Count; i++)
            {
                foreach (var presentLetter in word.presentLetters)
                {
                    if (!words[i].Contains(presentLetter))
                    {
                        words.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }

            if (word.lettersOrder.Count != 0)
            {
                for (int i = 0; i < words.Count; i++)
                {
                    for (int j = 0; j < word.lettersOrder.Count; j++)
                    {
                        if (words[i][j] != word.lettersOrder[j])
                        {
                            words.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
            return words;
        }

        static private List<string> CreateWordArray(int length)
        {
            DB DataBase = new DB();
            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT word FROM nouns WHERE char_length(word) = @length", DataBase.GetConnection());
            command.Parameters.Add("@length", MySqlDbType.Int32, 12).Value = length;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(table);

            DataBase.OpenConnection();
            DataBase.CloseConnection();

            DataRow[] rows = table.Select();
            List<string> words = new List<string>(rows.Length - 1);

            for (int i = 0; i < rows.Length; i++)
            {
                words.Add(rows[i].ItemArray[0].ToString());
            }
            return words;
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
                    else
                    {
                        RequestLettersOrder(wordLength, description);
                    }
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
            word.lettersOrder = RequestLettersOrder(word.length, "\nНиже нужно записать буквы, которые стоят в слове в определённом месте(желтые)\n");
            return word;
        }
    }
}