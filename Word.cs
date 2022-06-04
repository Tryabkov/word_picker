using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace word_picker
{
    internal struct Word
    {
        public Word()
        {
        }

        public int length = 0;
        public List<char> apsentLetters = new List<char>();
        public List<char> presentLetters = new List<char>();
        public Dictionary<int, char> lettersOrder = new Dictionary<int, char>();
    }
}
