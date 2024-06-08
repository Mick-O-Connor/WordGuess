class Game
{
    Player player;
    Word word;
    char[] wordToGuess;
    char[] hiddenWord;
    List<char> usedLetters;
    bool gameOver;

    char userGuess;

    public Game()
    {
        player = new Player();
        word = new Word();
        wordToGuess = word.GetRandomWord().ToUpper().ToCharArray();
        hiddenWord = new char[wordToGuess.Length];
        usedLetters = new List<char>();
        userGuess = ' ';
        SetHiddenWord();
        gameOver = false;
    }

    /// <summary>
    /// This method contains the main game loop and acts as the entry point of the program.
    /// </summary>
    public void Run()
    {
        DisplayIntro();
        
        while(!gameOver)
        {
            DisplayHiddenWord();
            GetUserInput();
            CompareInputAgainstWord();
            gameOver = CheckForWinLoss();
        }

        BreakLine();
        PlayAgain();
    }

    /// <summary>
    /// This method sets up the hidden word character array
    /// with the correct number of * characters
    /// </summary>
    void SetHiddenWord()
    {
        for(int i = 0; i < wordToGuess.Length; i++)
        {
            hiddenWord[i] = '*';
        }
    }

    /// <summary>
    /// Displays th introduction message to the player.
    /// </summary>
    void DisplayIntro()
    {
        Console.WriteLine("Welcome to WordGuess! Try to guess the hidden word.");
        Console.WriteLine("Good Luck!");
        BreakLine();
    }

    /// <summary>
    /// Prints the hidden word to the screen.
    /// </summary>
    void DisplayHiddenWord()
    {
        Console.Write("The hidden word: ");
        for(int i = 0; i < hiddenWord.Length; i++)
        {
            Console.Write(hiddenWord[i]);
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    /// <summary>
    /// Gets a letter from the user, recursively calls itself until a valid input is received.
    /// It adds the user's input to a list that contains all guessed letters.
    /// </summary>
    void GetUserInput()
    {
        Console.Write("Please guess a letter(a-z)");

        bool result = char.TryParse(Console.ReadLine(), out userGuess);
        if(result && Char.IsAsciiLetter(userGuess))
        {
            if(usedLetters.Contains(userGuess))
            {
                Console.WriteLine("You already guessed the letter: " + userGuess);
                GetUserInput();
            }
            usedLetters.Add(userGuess);
            userGuess = char.ToUpper(userGuess);
            Console.WriteLine("You guessed the letter: " + userGuess);
        }
        else
        {
            Console.WriteLine("Invalid input!");
            GetUserInput();
        }
    }

    /// <summary>
    /// This method checks to see if the user's input is present in the hidden word.
    /// If present it calls the UpdateHiddenWord function,
    /// otherwise it reduces the players lives by 1.
    /// </summary>
    void CompareInputAgainstWord()
    {
        if(wordToGuess.Contains(userGuess))
        {
            UpdateHiddenWord();
        }
        else
        {
            Console.WriteLine("The hidden word does not contain the letter: " + userGuess);
            player.LivesRemaining--;
            Console.WriteLine($"You lost a life, you have {player.LivesRemaining} lives remaining.");
        }
    }

    /// <summary>
    /// Updates the hidden word with the user guessed letter in each position it appears.
    /// </summary>
    void UpdateHiddenWord()
    {
        for(int i = 0; i < wordToGuess.Length; i ++)
        {
            if(wordToGuess[i] == userGuess)
            {
                hiddenWord[i] = userGuess;
            }
        }
    }

    /// <summary>
    /// Checks for player win or loss,
    /// otherwise game continues.
    /// </summary>
    /// <returns></returns>
    bool CheckForWinLoss()
    {
        if(hiddenWord.SequenceEqual(wordToGuess))
        {
            DisplayWinMessage();
            return true;
        }
        else if(player.LivesRemaining < 0)
        {
            DisplayLossMessage();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Displays the game won message.
    /// </summary>
    void DisplayWinMessage()
    {
        Console.WriteLine("Well done you guessed the correct word: ");
        player.GamesWon++;
        DisplayWinLossRatio();
    }

    /// <summary>
    /// Displays the game loss message.
    /// </summary>
    void DisplayLossMessage()
    {
        Console.WriteLine("You lost, you are out of lives!");
        Console.WriteLine($"The hidden word was {wordToGuess}.");
        player.GamesLost++;
        DisplayWinLossRatio();
    }

    /// <summary>
    /// Displays the number of games that the player has won and lost.
    /// Also displays the players win loss ratio to 2 decimal places.
    /// </summary>
    void DisplayWinLossRatio()
    {
        double wlr = player.GetWinLossRatio();
        Console.WriteLine($"You have won {player.GamesWon} games and lost {player.GamesLost} games.");
        Console.WriteLine($"You have a win loss ratio of: {wlr : 0.00##}.");
    }


    /// <summary>
    /// Prompts the user to play again or quit.
    /// </summary>
    void PlayAgain()
    {
        Console.Write("Please enter 1 to play again or 2 to quit: ");
        bool result = int.TryParse(Console.ReadLine(), out int input);

        if(result)
        {
            if(input == 1)
            {
                RestartGame();
            }
            else if(input == 2)
            {
                Console.WriteLine("Closing game...");
            }
            else
            {
                Console.WriteLine("Enter a 1 or a 2 only please!");
                PlayAgain();
            }
        }
        else
        {
            Console.WriteLine("Invalid input!");
            PlayAgain();
        }
    }

    /// <summary>
    /// Resets the arrays and clears usedletters list.
    /// Gets a new random word and calls SetHiddenWord.
    /// Calls Game.Run(), the main game loop to play again.
    /// Does not reset players wins and losses.
    /// </summary>
    void RestartGame()
    {
        Array.Clear(wordToGuess);
        Array.Clear(hiddenWord);
        
        player.LivesRemaining = 5;
        wordToGuess = word.GetRandomWord().ToUpper().ToCharArray();
        Array.Resize(ref hiddenWord, wordToGuess.Length);
        usedLetters.Clear();
        userGuess = ' ';
        SetHiddenWord();
        gameOver = false;
        Run();
    }

    void BreakLine()
    {
        Console.WriteLine("----------------------------------------");
    }
}