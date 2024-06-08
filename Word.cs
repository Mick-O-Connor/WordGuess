/// <summary>
/// Class <c> stores an array of words and has a function to return a random word.
/// </summary>
class Word
{
    string[] words;

    public Word()
    {
        words = ["cat", "dog", "pig", "duck", "sheep", "zebra", "horse", "monkey", "giraffe", "elephant"];
    }

    /// <summary>
    /// Method <c>GetRandomWord</c> returns a random word form the words array
    /// </summary>
    /// <returns> A string containing a random word</returns>
    public string GetRandomWord()
    {
        Random random = new Random();
        return words[random.Next(words.Length)];
    }
}