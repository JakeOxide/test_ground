using AmhPorterTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AmhPorterTest.Controllers
{
    public class StemmerController : Controller
    {
        private MatrixHandler matrix= new MatrixHandler();

        private List<CustomWord> inputWords;

        private Dictionary<char, List<char>> dictionary;


        CustomWordList model;
        public IActionResult Index()
        {
            this.model = new CustomWordList(new List<CustomWord>(), new Rules());
            char[] checkChars = ['ሱ', 'ዉ', 'ኙ', 'ደ'];

            foreach(char c in checkChars)
                Console.WriteLine($"Char_IN - ${c} - Key Index - ${matrix.FindKey(c).Item1}");

            /*
                ሱ
                ዉ
                ኙ
                ዱ             
            */

            return View();
        }


        [HttpPost]
        public IActionResult Stem(string Query)
        { 
			inputWords = matrix.GetCleanInput(Query);
            List<CustomWord> result = ProcessWords();
            CustomWordList cw = new CustomWordList(result, new Rules());
            return View("StemResult", cw);
        }
        public List<CustomWord> ProcessWords()
        {
            ExecuteTransformations();
            return inputWords;
        }

        private void ExecuteTransformations()
        {
            for (int i = 0; i < inputWords.Count; i++)
            {
                var currentWord = inputWords[i];
                currentWord.IdentifyRules();
                if (currentWord.wordRules.Remove_FirstIndex && currentWord.wordRules.Substitute_2LastIndex_6Letter_Remove_LastLetter)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Remove_FirstIndex(inputWords.ElementAt(i)));
                    SubstituteCustomWord(inputWords, i, Trigger_Substitute_2LastIndex_6Letter_Remove_LastLetter(inputWords.ElementAt(i)));
                }
                else if (currentWord.wordRules.Remove_FirstIndex && currentWord.wordRules.Substitute_2LastIndex_4Letter_Remove_LastLetter)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Remove_FirstIndex(inputWords.ElementAt(i)));
                    SubstituteCustomWord(inputWords, i, Trigger_Substitute_2LastIndex_2Letter_Remove_LastLetter(inputWords.ElementAt(i)));
                }
                else if (currentWord.wordRules.Remove_FirstIndex)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Remove_FirstIndex(inputWords.ElementAt(i)));
                }
                else if (currentWord.wordRules.Substitute_2LastIndex_4Letter_Remove_LastLetter)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Substitute_2LastIndex_4Letter_Remove_LastLetter(inputWords.ElementAt(i)));
                }
                else if (currentWord.wordRules.Substitute_2LastIndex_6Letter_Remove_LastLetter)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Substitute_2LastIndex_6Letter_Remove_LastLetter(inputWords.ElementAt(i)));
                }
            }
        }

        private CustomWord Trigger_Remove_FirstIndex(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            List<char> tempWord = word.word.ToList();
            tempWord.RemoveAt(0);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }

        private CustomWord Trigger_Substitute_2LastIndex_6Letter_Remove_LastLetter(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            char key = matrix.FindKey(word.word[word.word.Length - 2]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(4);
            List<char> tempWord = word.word.ToList();
            tempWord.RemoveRange(tempWord.Count - 2, 2);
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }

        private CustomWord Trigger_Substitute_2LastIndex_2Letter_Remove_LastLetter(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            char key = matrix.FindKey(word.word[word.word.Length - 2]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(2);
            List<char> tempWord = word.word.ToList();
            tempWord.RemoveRange(tempWord.Count - 2, 2);
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }

        private CustomWord Trigger_Substitute_2LastIndex_4Letter_Remove_LastLetter(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            char key = matrix.FindKey(word.word[word.word.Length - 2]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(2);
            List<char> tempWord = word.word.ToList();
            tempWord.RemoveRange(tempWord.Count - 2, 2);
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }

        private void SubstituteCustomWord(List<CustomWord> words, int index, CustomWord word)
        {
            words.RemoveAt(index);
            words.Insert(index, word);
        }

























    }
}
