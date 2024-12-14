using AmhPorter_Test_1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AmhStemmerTest101
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

            string[] lines = File.ReadAllLines("TextFile1.txt").Select(x => x.Replace('\t', ' ')).ToArray();
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

        public char FindKey(char letter)
        {
            for(int i = 0; i < this.privateDictionary.Count; i++)
            {
                if (this.privateDictionary.ElementAt(i).Value.Contains(letter)) 
                    return this.privateDictionary.Keys.ElementAt(i);
            }
            return '0';
        }


        public List<CustomWord> GetCleanInput(string str)
        {
            List<CustomWord> words = new List<CustomWord>();
            List<string>? input = str.Split(',').ToList();
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
