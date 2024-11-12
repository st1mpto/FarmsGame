public class ScoreManager
{
    public int Score { get; private set; }

    public ScoreManager()
    {
        Score = 0;
    }

    // Метод для добавления очков
    public void AddScore(string itemType)
    {
        if (itemType == "Useful")
        {
            Score += 10;
        }
        else if (itemType == "Trash")
        {
            Score -= 5;
        }
        else if (itemType == "TrashZone")
        {
            Score += 2;
        }
    }
}
