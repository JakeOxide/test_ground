using AmhPorterTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AmhPorterTest
{
    class MatrixHandler
    {
        private Dictionary<char, List<char>> privateDictionary;

        public MatrixHandler()
        {
            this.privateDictionary = GenerateDictionary();
        }

        public Dictionary<char, List<char>> GetDictionary()
        {
            return this.privateDictionary;
        }

        private Dictionary<char, List<char>> GenerateDictionary()
        {
            Dictionary<char, List<char>> keyValuePairs = new Dictionary<char, List<char>>();

            //string[] lines = File.ReadAllLines("TextFile1.txt").Select(x => x.Replace('\t', ' ')).ToArray();

            // The input string containing the lines
            string input = @"ሀ	ሁ	ሂ	ሃ	ሄ	ህ	ሆ	-
ለ	ሉ	ሊ	ላ	ሌ	ል	ሎ	ሏ
ሐ	ሑ	ሒ	ሓ	ሔ	ሕ	ሖ	ሗ
መ	ሙ	ሚ	ማ	ሜ	ም	ሞ	ሟ
ሠ	ሡ	ሢ	ሣ	ሤ	ሥ	ሦ	ሧ
ረ	ሩ	ሪ	ራ	ሬ	ር	ሮ	ሯ
ሰ	ሱ	ሲ	ሳ	ሴ	ስ	ሶ	ሷ
ሸ	ሹ	ሺ	ሻ	ሼ	ሽ	ሾ	ሿ
ቀ	ቁ	ቂ	ቃ	ቄ	ቅ	ቆ	ቋ
በ	ቡ	ቢ	ባ	ቤ	ብ	ቦ	ቧ
ቨ	ቩ	ቪ	ቫ	ቬ	ቭ	ቮ	ቯ
ተ	ቱ	ቲ	ታ	ቴ	ት	ቶ	ቷ
ቸ	ቹ	ቺ	ቻ	ቼ	ች	ቾ	ቿ
ኀ	ኁ	ኂ	ኃ	ኄ	ኅ	ኆ	ኋ
ነ	ኑ	ኒ	ና	ኔ	ን	ኖ	ኗ
ኘ	ኙ	ኚ	ኛ	ኜ	ኝ	ኞ	ኟ
አ	ኡ	ኢ	ኣ	ኤ	እ	ኦ	-
ከ	ኩ	ኪ	ካ	ኬ	ክ	ኮ	ኳ
ኸ	ኹ	ኺ	ኻ	ኼ	ኽ	ኾ	ዃ
ወ	ዉ	ዊ	ዋ	ዌ	ው	ዎ	-
ዐ	ዑ	ዒ	ዓ	ዔ	ዕ	ዖ	-
ዘ	ዙ	ዚ	ዛ	ዜ	ዝ	ዞ	ዟ
ዠ	ዡ	ዢ	ዣ	ዤ	ዥ	ዦ	ዧ
የ	ዩ	ዪ	ያ	ዬ	ይ	ዮ	-
ደ	ዱ	ዲ	ዳ	ዴ	ድ	ዶ	ዷ
ጀ	ጁ	ጂ	ጃ	ጄ	ጅ	ጆ	ጇ
ገ	ጉ	ጊ	ጋ	ጌ	ግ	ጎ	ጓ
ጠ	ጡ	ጢ	ጣ	ጤ	ጥ	ጦ	ጧ
ጨ	ጩ	ጪ	ጫ	ጬ	ጭ	ጮ	ጯ
ጰ	ጱ	ጲ	ጳ	ጴ	ጵ	ጶ	ጷ
ጸ	ጹ	ጺ	ጻ	ጼ	ጽ	ጾ	ጿ
ፀ	ፁ	ፂ	ፃ	ፄ	ፅ	ፆ	-
ፈ	ፉ	ፊ	ፋ	ፌ	ፍ	ፎ	ፏ
ፐ	ፑ	ፒ	ፓ	ፔ	ፕ	ፖ	ፗ";

            // Split the input into lines
            string[] lines = input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Replace('\t', ' ')).Select(x => x.Replace('\r', ' ')).ToArray();



            List<char> values = new List<char>();

            for(int i = 0; i < lines.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                for(int j = 0; j < lines[i].Length; j++)
                {
                    if (j == 0 || lines[i][j] == ' ') continue;
                    sb.Append(lines[i][j].ToString()).ToString();
                }
                values = sb.ToString().ToList();
                keyValuePairs.Add(lines[i].ElementAt(0), values);
            }
            return keyValuePairs;

        }

        public (int, char) FindKey(char letter)
        {
            if (this.privateDictionary.ContainsKey(letter)) return (this.privateDictionary.Keys.ToList().IndexOf(letter), letter);

            for(int i = 0; i < this.privateDictionary.Count; i++)
            {
                if (this.privateDictionary.ElementAt(i).Value.Contains(letter)) 
                    return (i, this.privateDictionary.Keys.ElementAt(i));
            }
            return (0, '0');
        }


        public List<char> GetVariation(char letter, int ident)
        {
            char key = FindKey(letter).Item2;
            return this.privateDictionary.GetValueOrDefault(key);
        }

        public char GetSpecificVariation(char letter, int ident)
        {
            char key = FindKey(letter).Item2;
            return this.privateDictionary.GetValueOrDefault(key).ElementAt(ident);
        }

        public bool CheckFamily(char letter, char letter2)
        {
            char key = FindKey(letter).Item2;
            return this.privateDictionary.GetValueOrDefault(key).Contains(letter2);

        }


        public List<CustomWord> GetCleanInput(string str)
        {
            List<CustomWord> words = new List<CustomWord>();
            List<string>? input = str.Split(' ').ToList();
            if (input == null)
                throw new Exception("Null Input Detected");

            List<string> newList = new List<string>();

            foreach (string temp in input)
                newList.Add(temp.Trim());

            foreach (string tempstr in newList)
            {
                words.Add(new CustomWord(tempstr));
            }
            return words;
        }

    }
}
