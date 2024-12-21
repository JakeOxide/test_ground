using AmhPorterTest.Models;
using AmhPorterTest.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AmhPorterTest.Controllers
{
    public class StemmerController : Controller
    {
        private MatrixHandler matrix= new MatrixHandler();

        private List<CustomWord> inputWords;

        private List<CustomWord> originalWords;

        private Dictionary<char, List<char>> dictionary;

        private PatternDetectionEngine patternDetectionEngine;


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
            originalWords = matrix.GetCleanInput(Query);
            List<CustomWord> result = ProcessWords();
            CustomWordList cw = new CustomWordList(result, new Rules());
            ResultCollection resultCollection = new ResultCollection(originalWords, cw);
            return View("StemResult", resultCollection);
        }
        public List<CustomWord> ProcessWords()
        {
            patternDetectionEngine = new PatternDetectionEngine(inputWords);
            inputWords = patternDetectionEngine.DetectPatterns();
            ExecuteTransformations();
            return inputWords;
        }

        private void ExecuteTransformations()
        {
            
            for (int i = 0; i < inputWords.Count; i++)
            {

                // SUFFIX


                if (inputWords[i].wordRules.Rule301)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule301(inputWords.ElementAt(i)));
                }

                else if (inputWords[i].wordRules.Rule302)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule302(inputWords.ElementAt(i)));
                }

                else if (inputWords[i].wordRules.Rule303)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule303(inputWords.ElementAt(i)));
                }

                else if (inputWords[i].wordRules.Rule304)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule304(inputWords.ElementAt(i)));
                }

                else if (inputWords[i].wordRules.Rule305)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule305(inputWords.ElementAt(i)));
                }

                else if (inputWords[i].wordRules.Rule306)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule306(inputWords.ElementAt(i)));
                }

                else if (inputWords[i].wordRules.Rule307)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule307(inputWords.ElementAt(i)));
                }


                else if (inputWords[i].wordRules.Rule308 && inputWords[i].wordRules.Rule309)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule308(inputWords.ElementAt(i)));
                }

                else if (inputWords[i].wordRules.Rule310 && inputWords[i].wordRules.Rule311)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule310(inputWords.ElementAt(i)));
                }

                else if (inputWords[i].wordRules.Rule312 && inputWords[i].wordRules.Rule313)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule312(inputWords.ElementAt(i)));
                }

                else if (inputWords[i].wordRules.Rule314 && inputWords[i].wordRules.Rule315)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule314(inputWords.ElementAt(i)));
                }

                else if (inputWords[i].wordRules.Rule316 && inputWords[i].wordRules.Rule317)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule316(inputWords.ElementAt(i)));
                }



                /*




                   else if (inputWords[i].wordRules.Rule108)
                   {
                       SubstituteCustomWord(inputWords, i, Trigger_Rule108(inputWords.ElementAt(i)));
                   }
   */


                if (inputWords[i].wordRules.Rule101)
                {
					SubstituteCustomWord(inputWords, i, Trigger_Rule101(inputWords.ElementAt(i)));
				}

                else if(inputWords[i].wordRules.Rule102)
				{
					SubstituteCustomWord(inputWords, i, Trigger_Rule102(inputWords.ElementAt(i)));
				}

                else if(inputWords[i].wordRules.Rule103)
				{
					SubstituteCustomWord(inputWords, i, Trigger_Rule103(inputWords.ElementAt(i)));
				}

                else if (inputWords[i].wordRules.Rule104)
				{
					SubstituteCustomWord(inputWords, i, Trigger_Rule104(inputWords.ElementAt(i)));
				}

                else if(inputWords[i].wordRules.Rule105)
				{
					SubstituteCustomWord(inputWords, i, Trigger_Rule105(inputWords.ElementAt(i)));
				}

                else if(inputWords[i].wordRules.Rule106)
				{
					SubstituteCustomWord(inputWords, i, Trigger_Rule106(inputWords.ElementAt(i)));
				}

                else if(inputWords[i].wordRules.Rule107)
				{
					SubstituteCustomWord(inputWords, i, Trigger_Rule107(inputWords.ElementAt(i)));
				}

                else if(inputWords[i].wordRules.Rule108)
				{
					SubstituteCustomWord(inputWords, i, Trigger_Rule108(inputWords.ElementAt(i)));
				}

                else if (inputWords[i].wordRules.Rule109)
                {
                    SubstituteCustomWord(inputWords, i, Trigger_Rule109(inputWords.ElementAt(i)));
                }




                /*
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

                */
            }
            
        }




		private CustomWord Trigger_Rule101(CustomWord word)
		{
			dictionary = matrix.GetDictionary();
			List<char> tempWord = word.word.ToList();
			tempWord.RemoveRange(0, 2);
			CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
			return newWord;
		}

		private CustomWord Trigger_Rule102(CustomWord word)
		{
			dictionary = matrix.GetDictionary();
			List<char> tempWord = word.word.ToList();
			tempWord.RemoveAt(0);
			CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
			return newWord;
		}

		private CustomWord Trigger_Rule103(CustomWord word)
		{
			dictionary = matrix.GetDictionary();
			List<char> tempWord = word.word.ToList();
            tempWord.RemoveRange(0, 4);
			CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
			return newWord;
		}

		private CustomWord Trigger_Rule104(CustomWord word)
		{
			dictionary = matrix.GetDictionary();
			List<char> tempWord = word.word.ToList();
            tempWord.RemoveRange(0, 3);
			CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
			return newWord;
		}

		private CustomWord Trigger_Rule105(CustomWord word)
		{
			dictionary = matrix.GetDictionary();
			List<char> tempWord = word.word.ToList();
            tempWord.RemoveRange(0, 2);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
			return newWord;
		}

		private CustomWord Trigger_Rule106(CustomWord word)
		{
			dictionary = matrix.GetDictionary();
			List<char> tempWord = word.word.ToList();
            tempWord.RemoveRange(0, 2);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
			return newWord;
		}

		private CustomWord Trigger_Rule107(CustomWord word)
		{
			dictionary = matrix.GetDictionary();
			char newLetter = 'አ';
			List<char> tempWord = word.word.ToList();
            tempWord.RemoveRange(0, 2);
            tempWord.Reverse();
            tempWord.Add(newLetter);
            tempWord.Reverse();
			CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
			return newWord;
		}

		private CustomWord Trigger_Rule108(CustomWord word)
		{
			dictionary = matrix.GetDictionary();
			List<char> tempWord = word.word.ToList();
			tempWord.RemoveAt(0);
			CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            word.wordRules.Rule108 = false;
			return newWord;
		}

        private CustomWord Trigger_Rule109(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            List<char> tempWord = word.word.ToList();
            tempWord.RemoveRange(0, 2);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            word.wordRules.Rule108 = false;
            return newWord;
        }



        // SUFFIX


        private CustomWord Trigger_Rule301(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            char key = matrix.FindKey(word.word[word.word.Length - 1]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(4);
            List<char> tempWord = word.word.ToList();
            tempWord.RemoveAt(tempWord.Count - 1);
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }

        private CustomWord Trigger_Rule302(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            List<char> tempWord = word.word.ToList();
            tempWord.RemoveAt(tempWord.Count - 1);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }

        private CustomWord Trigger_Rule303(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            char key = matrix.FindKey(word.word[word.word.Length - 1]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(4);
            List<char> tempWord = word.word.ToList();
            tempWord.RemoveAt(tempWord.Count - 1);
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }

        private CustomWord Trigger_Rule304(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            char key = matrix.FindKey(word.word[word.word.Length - 1]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(4);
            List<char> tempWord = word.word.ToList();
            tempWord.RemoveAt(tempWord.Count - 1);
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }

        private CustomWord Trigger_Rule305(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            List<char> tempWord = word.word.ToList();
            tempWord.Reverse();
            tempWord.RemoveRange(0, 3);
            tempWord.Reverse();
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }

        private CustomWord Trigger_Rule306(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            List<char> tempWord = word.word.ToList();
            char key = matrix.FindKey(word.word[word.word.Length - 4]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(4);
            tempWord.Reverse();
            tempWord.RemoveRange(0, 4);
            tempWord.Reverse();
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }

        private CustomWord Trigger_Rule307(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            List<char> tempWord = word.word.ToList();
            char key = matrix.FindKey(word.word[word.word.Length - 5]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(4);
            tempWord.Reverse();
            tempWord.RemoveRange(0, 5);
            tempWord.Reverse();
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }

        private CustomWord Trigger_Rule308(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            List<char> tempWord = word.word.ToList();
            char key = matrix.FindKey(word.word[word.word.Length - 5]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(4);
            tempWord.Reverse();
            tempWord.RemoveRange(0, 5);
            tempWord.Reverse();
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }

        private CustomWord Trigger_Rule310(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            List<char> tempWord = word.word.ToList();
            char key = matrix.FindKey(word.word[word.word.Length - 4]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(4);
            tempWord.Reverse();
            tempWord.RemoveRange(0, 4);
            tempWord.Reverse();
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }
        private CustomWord Trigger_Rule312(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            List<char> tempWord = word.word.ToList();
            char key = matrix.FindKey(word.word[word.word.Length - 4]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(4);
            tempWord.Reverse();
            tempWord.RemoveRange(0, 4);
            tempWord.Reverse();
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }
        private CustomWord Trigger_Rule314(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            List<char> tempWord = word.word.ToList();
            char key = matrix.FindKey(word.word[word.word.Length - 3]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(4);
            tempWord.Reverse();
            tempWord.RemoveRange(0, 3);
            tempWord.Reverse();
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }
        private CustomWord Trigger_Rule316(CustomWord word)
        {
            dictionary = matrix.GetDictionary();
            List<char> tempWord = word.word.ToList();
            char key = matrix.FindKey(word.word[word.word.Length - 2]).Item2;
            char newLetter = dictionary.GetValueOrDefault(key).ElementAt(4);
            tempWord.Reverse();
            tempWord.RemoveRange(0, 2);
            tempWord.Reverse();
            tempWord.Add(newLetter);
            CustomWord newWord = new CustomWord(new string(tempWord.ToArray()), word.wordRules);
            return newWord;
        }


        /*      
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
		*/


        private void SubstituteCustomWord(List<CustomWord> words, int index, CustomWord word)
        {
            words.RemoveAt(index);
            words.Insert(index, word);
        }

























    }
}
