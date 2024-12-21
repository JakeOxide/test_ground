using AmhPorterTest.Models;
using System.Text.RegularExpressions;

namespace AmhPorterTest.Utils
{
	public class PatternDetectionEngine
	{

        List<CustomWord> input;

        public PatternDetectionEngine(List<CustomWord> input)
        {
            this.input = input;
        }


        public List<CustomWord> DetectPatterns()
        {

			DetectPrefixPatterns();
            DetectSuffixPatterns();

            return input;
        }



		//  Prefix Pattern Detection

		private void DetectPrefixPatterns()
        {
            /*


            የ

            በ የ

            በ

            እ ን ደ የ

            እ ን ደ

            እ ነ

            እ የ

            እ ያ


             */

            for (int i = 0; i < input.Count; i++)
            {

				// Rule 101: P('በየ')* -> Rem(P('በ', 'የ'))* - Not for words with Prefix + a two letters
				// Rule 101_Independent: (P('በየ')*) *{X[2var]} -> Rem(P('በ', 'የ'))* Sub(*{X[6var]}) - Not for words with Prefix + a two letters
				if (input[i].word[0] == 'በ' && input[i].word[1] == 'የ')
				{
					if (input[i].word.Length <=3) return;
					input[i].wordRules.Rule101 = true;
					
				}

				// Rule 102: P('በ')* -> Rem(P('በ')) - Not for words with Prefix + a single letter
				else if (input[i].word[0] == 'በ')
				{
					if (input[i].word.Length <= 2) return;
					input[i].wordRules.Rule102 = true;
				}

				// Rule 103: P('እንደየ')* -> Rem(P('እ', 'ን', 'ደ', 'የ'))
				else if (input[i].word[0] == 'እ' && input[i].word[1] == 'ን' && input[i].word[2] == 'ደ' && input[i].word[3] == 'የ')
				{
					input[i].wordRules.Rule103 = true;
				}

				// Rule 104: P('እንደ')* -> Rem(P('እ', 'ን', 'ደ'))
				else if (input[i].word[0] == 'እ' && input[i].word[1] == 'ን' && input[i].word[2] == 'ደ')
				{
					input[i].wordRules.Rule104 = true;
				}

				// Rule 105: P('እነ')* -> Rem(P('እ', 'ነ')) - Not for words with Prefix + a single letter
				else if (input[i].word[0] == 'እ' && input[i].word[1] == 'ነ')
				{
					if (input[i].word.Length == 3) return;
					input[i].wordRules.Rule105 = true;
				}

				// Rule 106: P('እየ')* -> Rem(P('እ', 'የ'))
				else if (input[i].word[0] == 'እ' && input[i].word[1] == 'የ')
				{
					input[i].wordRules.Rule106 = true;
				}

				// Rule 107: P('እያ')* -> Rem(P('እ', 'ያ'))
				else if (input[i].word[0] == 'እ' && input[i].word[1] == 'ያ')
				{
					input[i].wordRules.Rule107 = true;
				}

				// Rule 108: P('የ')* -> Rem(P('የ')) - Not for words with Prefix + a single letter
				else if (input[i].word[0] == 'የ')
				{
					if (input[i].word.Length <= 3) return;
					input[i].wordRules.Rule108 = true;
				}

                // Rule 109: P('ስለ')* -> Rem(P('ስለ')) - Not for words with Prefix + a single letter
                else if (input[i].word[0] == 'ስ' && input[i].word[1] == 'ለ')
                {
                    if (input[i].word.Length <= 3) return;
                    input[i].wordRules.Rule109 = true;
                }

            }

        }


        //  Suffix Pattern Detection

        private void DetectSuffixPatterns()
		{

            /*
             
				ሬ
				ህ
				ሽ
				ሩ
				ሯ
				ራችን
				ራችሁ
				ራቸው
				ሮች
				ሮቼ
				ሮችህ
				ሮችሽ
				ሮቹ
				ሮቿ
				ሮቻችን
				ሮቻችሁ
				ሮቻቸው

             */


            for (int i = 0; i < input.Count; i++)
            {
                MatrixHandler matrixHandler = new MatrixHandler();

                // Rule 302: *S({ዬ[3var]}) || *S({ህ[3var]}) || *S({ሽ[3var]}) -> Rem(*S({ዬ[3var]}) || *S({ህ[3var]}) || *S({ሽ[3var]}) )
                if (input[i].word[input[i].word.Length - 1] == 'ዬ' || 
                    input[i].word[input[i].word.Length - 1] == 'ሽ' || 
                    input[i].word[input[i].word.Length - 1] == 'ህ')
                {
                    input[i].wordRules.Rule302 = true;
                }

                // Rule 301: *S({X[3var]}) -> Rem(*S({X[3var]}))* - Not for words with Prefix + a two letters
                else if (input[i].word[input[i].word.Length - 1] == matrixHandler.GetSpecificVariation(input[i].word[input[i].word.Length - 1], 3))
                {
                    if (input[i].word.Length == 2) return;
                    input[i].wordRules.Rule301 = true;

                }

                // Rule 303: *S({X[2var]}) -> Sub(S({X[6var]}))
                else if (input[i].word[input[i].word.Length - 1] == matrixHandler.GetSpecificVariation(input[i].word[input[i].word.Length - 1], 0))
                {
                    input[i].wordRules.Rule303 = true;
                }

                // Rule 304: *S({X[8var]}) -> Sub(S({X[6var]}))
                else if (input[i].word[input[i].word.Length - 1] == matrixHandler.GetSpecificVariation(input[i].word[input[i].word.Length - 1], 6))
                {
                    input[i].wordRules.Rule304 = true;
                }

                // ያችንን
                // Rule 305: *S(ያችንን) -> Rem(*S(ችንን))  
                else if (input[i].word.Length > 5 && 
                    input[i].word[input[i].word.Length - 4] == 'ያ' &&
                        input[i].word[input[i].word.Length - 3] == 'ች' &&
                        input[i].word[input[i].word.Length - 2] == 'ን' &&
                        input[i].word[input[i].word.Length - 1] == 'ን')
                {
                    if (input[i].word.Length == 3) return;
                    input[i].wordRules.Rule305 = true;
                }

                // Rule 307: {X[7var]}*S(ችችንን) -> Rem(*S(ችችንን)) Sub(X[6var]})
                else if (input[i].word.Length > 5 &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 4], 'ች') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 3], 'ች') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 2], 'ን') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 1], 'ን'))
                {
                    input[i].wordRules.Rule307 = true;
                }


                // Rule 306: {X[8var]}*S(ችንን) -> Rem(*S(ያችንን))
                else if (input[i].word.Length > 5 &&
                    input[i].word[input[i].word.Length - 4] == matrixHandler.GetSpecificVariation(input[i].word[input[i].word.Length - 4], 2))
                {
                    input[i].wordRules.Rule306 = true;
                }


                // Rule 308: ቻችንን* -> Rem(P('የ')) - Not for words with Prefix + a single letter
                else if (input[i].word.Length > 5 &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 4], 'ቻ') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 3], 'ች') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 2], 'ን') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 1], 'ን')
                    )
                {
                    if (input[i].word.Length == 2) return;
                    input[i].wordRules.Rule308 = true;
                    input[i].wordRules.Rule309 = true;
                }

                // Rule 310: ቻችን* -> Rem(P('የ')) - Not for words with Prefix + a single letter
                else if (input[i].word.Length > 4 &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 3], 'ቻ') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 2], 'ች') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 1], 'ን') 
                    )
                {
                    if (input[i].word.Length == 2) return;
                    input[i].wordRules.Rule310 = true;
                    input[i].wordRules.Rule311 = true;
                }

                // Rule 312: ችን* -> Rem(P('የ')) - Not for words with Prefix + a single letter
                else if (
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 3], 'ች') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 2], 'ን') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 1], 'ን')
                    )
                {
                    if (input[i].word.Length == 2) return;
                    input[i].wordRules.Rule312 = true;
                    input[i].wordRules.Rule313 = true;
                }

                // Rule 314: ችን* -> Rem(P('የ')) - Not for words with Prefix + a single letter
                else if (input[i].word.Length > 3 &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 2], 'ች') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 1], 'ን') 
                    )
                {
                    if (input[i].word.Length == 2) return;
                    input[i].wordRules.Rule314 = true;
                    input[i].wordRules.Rule315 = true;
                }

                // Rule 316: ች* -> Rem(P('የ')) - Not for words with Prefix + a single letter
                else if (
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 2], 'ወ') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 1], 'ች') ||
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 2], 'ሰ') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 1], 'ች') ||
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 2], 'ኝ') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 1], 'ች') ||
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 2], 'ቲ') &&
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 1], 'ች') 
                    )
                {
                    if (input[i].word.Length == 2) return;
                    input[i].wordRules.Rule320 = true;
                }

                // Rule 316: ች* -> Rem(P('የ')) - Not for words with Prefix + a single letter
                else if (
                    
                    matrixHandler.CheckFamily(input[i].word[input[i].word.Length - 1], 'ች')
                    )
                {
                    if (input[i].word.Length == 2) return;
                    input[i].wordRules.Rule316 = true;
                    input[i].wordRules.Rule317 = true;

                }

            }








        }



        //  Special Patterns Detection

        private void DetectSpecialPatterns()
        {
            MatrixHandler matrixHandler = new MatrixHandler();
            for (int i = 0; i < input.Count; i++)
            {
                var currentWord = input[i];

                //Rule 401: {X[1var]}{Y[1var]}{Z[4var]}{Z[1var]}{A[6var]} -> {X[1var]}{Y[4var]}{Z[1var]}{0}{A[1var]}

                if (input[i].word[0] == matrixHandler.GetSpecificVariation(input[i].word[0], 0)) ;


                //Rule 402: {X[6var]}{Y[1var]}{Z[4var]}{X[6var]}{Y[1var]}{Z[6var]} -> {X[6var]}{Y[1var]}{0}{0}{0}{Z[6var]}

                

            }

		}






	}
}
