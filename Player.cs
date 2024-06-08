/// <summary>
/// Class <c> Player </c> stores info such as players lives and games won and lost.
/// </summary>
class Player
{
    int livesRemaining;
    int gamesWon;
    int gamesLost;
    double winLossRatio;

    public Player()
    {
        livesRemaining = 5;
        gamesWon = 0;
        gamesLost = 0;
        winLossRatio = 0.0;
    }

    public int LivesRemaining
    {
        get { return livesRemaining; }
        set { livesRemaining = value; }
    }

    public int GamesWon 
    {
        get { return gamesWon; }
        set { gamesWon = value; }
    }

    public int GamesLost
    {
        get { return gamesLost; }
        set { gamesLost = value; }
    }

    /// <summary>
    /// This method calculates the players win to loss ratio.
    /// </summary>
    /// <returns> A double containing the players win loss ratio </returns>
    public double GetWinLossRatio()
    {
        winLossRatio = (double)gamesWon / (double)gamesLost;
        return winLossRatio;
    }
}
