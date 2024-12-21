namespace AmhPorterTest.Models
{
    public record CustomWordList(List<CustomWord> customWords, Rules rules);

    public record ResultCollection(List<CustomWord> inputWords, CustomWordList customWords);
    
}
