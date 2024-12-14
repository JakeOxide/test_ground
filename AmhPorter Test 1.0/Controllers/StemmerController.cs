using AmhPorter_Test_1._0.Models;
using AmhStemmerTest101;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AmhPorter_Test_1._0.Controllers
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
            IdentifyRules();
            ExecuteTransformations();
            return inputWords;
        }

        private void IdentifyRules()
        {
            for (int i = 0; i < inputWords.Count; i++)
            {
                if (inputWords[i].word[0] == 'የ'
                    && inputWords[i].word[inputWords[i].word.Length - 1] == 'ች')
                {
                    inputWords[i].wordRules.Remove_FirstIndex = true;
                    inputWords[i].wordRules.Substitute_2LastIndex_6Letter_Remove_LastLetter = true;
                }
                else if (inputWords[i].word[0] == 'የ'
                    && inputWords[i].word[inputWords[i].word.Length - 2] == 'ዎ'
                    && inputWords[i].word[inputWords[i].word.Length - 1] == 'ች')
                {
                    inputWords[i].wordRules.Remove_FirstIndex = true;
                    inputWords[i].wordRules.Substitute_2LastIndex_2Letter_Remove_LastLetter = true;
                }
                else if (inputWords[i].word[0] == 'የ')
                {
                    inputWords[i].wordRules.Remove_FirstIndex = true;
                }
                else if (inputWords[i].word[inputWords[i].word.Length - 2] == 'ዎ'
                    && inputWords[i].word[inputWords[i].word.Length - 1] == 'ች')
                {
                    inputWords[i].wordRules.Substitute_2LastIndex_2Letter_Remove_LastLetter = true;
                }
                else if (inputWords[i].word[inputWords[i].word.Length - 1] == 'ች')
                {
                    inputWords[i].wordRules.Substitute_2LastIndex_6Letter_Remove_LastLetter = true;
                }
            }
        }

        private void ExecuteTransformations()
        {
            for (int i = 0; i < inputWords.Count; i++)
            {
                if (inputWords[i].wordRules.Remove_FirstIndex && inputWords[i].wordRules.Substitute_2LastIndex_6Letter_Remove_LastLetter)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Remove_FirstIndex(inputWords.ElementAt(i)));
                    SubstituteCustomWord(inputWords, i, Trigger_Substitute_2LastIndex_6Letter_Remove_LastLetter(inputWords.ElementAt(i)));
                }
                else if (inputWords[i].wordRules.Remove_FirstIndex && inputWords[i].wordRules.Substitute_2LastIndex_2Letter_Remove_LastLetter)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Remove_FirstIndex(inputWords.ElementAt(i)));
                    SubstituteCustomWord(inputWords, i, Trigger_Substitute_2LastIndex_2Letter_Remove_LastLetter(inputWords.ElementAt(i)));
                }
                else if (inputWords[i].wordRules.Remove_FirstIndex)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Remove_FirstIndex(inputWords.ElementAt(i)));
                }
                else if (inputWords[i].wordRules.Substitute_2LastIndex_2Letter_Remove_LastLetter)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Substitute_2LastIndex_2Letter_Remove_LastLetter(inputWords.ElementAt(i)));
                }
                else if (inputWords[i].wordRules.Substitute_2LastIndex_6Letter_Remove_LastLetter)
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
            char key = matrix.FindKey(word.word[word.word.Length - 2]);
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
            //char key = matrix.FindKey(word.word[word.word.Length - 2]);
            //char newLetter = dictionary.GetValueOrDefault(key).ElementAt(4);
            List<char> tempWord = word.word.ToList();
            tempWord.RemoveRange(tempWord.Count - 2, 2);
            //tempWord.Add(newLetter);
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
