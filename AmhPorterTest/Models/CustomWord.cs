namespace AmhPorterTest.Models
{
    public class CustomWord
    {
        public string? word { get; set; }
        public Rules? wordRules { get; set; }
        public bool? changeCheck { get; set; }

        char[] checkChars = ['ሰ', 'ወ', 'ኘ', 'ደ'];
        int[] checkCharKeys = [6, 19, 15, 24];

        public CustomWord() { }

        public CustomWord(string word)
        {
            this.word = word;
            this.wordRules = new Rules();
        }

        public CustomWord(string word, Rules rules)
        {
            this.word = word;
            this.wordRules = rules;
        }

        public void IdentifyRules()
        {
            if (this.word == null) return;
            if (this.wordRules != null) this.wordRules = new Rules();

            HandlePrefixes();

            // Rule 101: P('የ')*{7Var}S('ች') -> Rem(P('የ'), S('ች')) and Rep(w.len-1, {6Var})
            if (this.word[0] == 'የ'
                && this.word[this.word.Length - 1] == 'ች')
            {
                this.wordRules.Remove_FirstIndex = true;
                this.wordRules.Substitute_2LastIndex_6Letter_Remove_LastLetter = true;
            }

            // Rule 102: P('የ')*{7Var}S('ዎ')S('ች') -> Rem(P('የ'), S('ዎ')S('ች')) and Rep(w.len-1, {6Var})
            else if (this.word[0] == 'የ'
                && this.word[this.word.Length - 2] == 'ዎ'
                && this.word[this.word.Length - 1] == 'ች')
            {
                this.wordRules.Remove_FirstIndex = true;
                this.wordRules.Substitute_2LastIndex_6Letter_Remove_LastLetter = true;
            }



            // Rule 104: * (ሰ) || (ወ) || (ኘ) || (ደ) *S('ዎ')S('ች') -> Rem(S('ዎ', 'ች')) and Rep(w.len-1, {4Var})

            else if (CustomWordContains(this.word) && this.word[this.word.Length - 2] == 'ዎ'
                && this.word[this.word.Length - 1] == 'ች')
            {
                this.wordRules.Substitute_2LastIndex_4Letter_Remove_LastLetter = true;
            }

            // Rule 105: * (ሰ) || (ወ) || (ኘ) || (ደ) *S() -> Rem(S('ች')) and Rep(w.len-1, {4Var})

            else if (CustomWordContains(this.word)
                && this.word[this.word.Length - 1] == 'ች')
            {
                this.wordRules.Substitute_2LastIndex_4Letter_Remove_LastLetter = true;
            }


            // Rule 106: *S() -> Rem(S(, 'ች'))

            else if (this.word[this.word.Length - 2] == 'ዎ'
                && this.word[this.word.Length - 1] == 'ች')
            {
                this.wordRules.Substitute_2LastIndex_4Letter_Remove_LastLetter = true;
            }

            // Rule 107: *{7Var}S('ች') -> Rem(S('ች')) and Rep(w.len-1, {6Var})
            else if (this.word[this.word.Length - 1] == 'ች')
            {
                this.wordRules.Substitute_2LastIndex_6Letter_Remove_LastLetter = true;
            }
            
        }

        private void HandlePrefixes()
        {

			// Rule 101: P('በየ')* -> Rem(P('በየ')) - Not for words with Prefix + a single letter
			if (this.word[0] == 'በ' && this.word[1] == 'የ')
			{
				if (this.word.Length == 2) return;
				this.wordRules.Remove_FirstTwoIndex = true;
			}

			// Rule 102: P('በ')* -> Rem(P('በ')) - Not for words with Prefix + a single letter
			if (this.word[0] == 'በ')
			{
				if (this.word.Length == 2) return;
				this.wordRules.Remove_FirstIndex = true;
			}

			// Rule 103: P('የ')* -> Rem(P('የ')) - Not for words with Prefix + a single letter
			if (this.word[0] == 'የ')
			{
				if (this.word.Length == 2) return;
				this.wordRules.Remove_FirstIndex = true;
			}
		}

        private bool CustomWordContains(string str)
        {
            MatrixHandler tempMatrix = new MatrixHandler();
            var tempDictionary = tempMatrix.GetDictionary();

            if (str == null || str.Length == 0) return false;

            for( int i = 0; i < str.Length; i++ )
            { 
                if (checkCharKeys.Contains(tempMatrix.FindKey(str[i]).Item1)) return true;
            }
            
            return false;
        }


    }
}
