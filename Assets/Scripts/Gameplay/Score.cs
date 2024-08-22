public static class Score
{
    private static int _currentValue = 0;
    public static int CurrentScore => _currentValue;

    public static void AddValueToScore(int addValue = 50)
        => _currentValue += addValue;

    public static void SaveBestScore()
    {
        if (_currentValue > SaveManager.LoadBestScore())
        {
            SaveManager.SaveBestScore(_currentValue);
        }

        ZeroingOutScore();
    }

    public static void ZeroingOutScore()
        => _currentValue = 0;
}