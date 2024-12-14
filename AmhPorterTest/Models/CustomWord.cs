namespace AmhPorterTest.Models
{
    public class CustomWord
    {
        public string? word { get; set; }
        public Rules? wordRules { get; set; }

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
    }
}
