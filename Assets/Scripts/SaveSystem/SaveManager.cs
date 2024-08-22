public static class SaveManager
{
    private static SaveData currentData;

    static SaveManager()
    {
        currentData = JsonHelper.LoadData();
    }

    #region URL

    public static void SaveURL(string url)
    {
        currentData.URL = url;
        JsonHelper.SaveData(currentData);
    }

    public static string LoadURL()
        => currentData.URL;

    public static bool HasURL()
        => string.IsNullOrEmpty(currentData.URL) == false;

    #endregion

    #region FirstRun

    public static void SaveFirstRun(bool state)
    {
        currentData.FirstRun = state;
        JsonHelper.SaveData(currentData);
    }

    public static bool LoadFirstRun()
        => currentData.FirstRun;

    #endregion

    #region GamePlay

    public static void SaveGamePlay(bool state)
    {
        currentData.GamePlay = state;
        JsonHelper.SaveData(currentData);
    }

    public static bool LoadGamePlay()
        => currentData.GamePlay;

    public static bool HasGamePlay()
        => currentData.GamePlay == false;

    #endregion

    #region BestScore

    public static void SaveBestScore(int score)
    {
        currentData.BestScore = score;
        JsonHelper.SaveData(currentData);
    }

    public static int LoadBestScore()
        => currentData.BestScore;

    #endregion

    #region WelcomeBonus

    public static void SaveWelcomeBonus(int count)
    {
        currentData.WelcomeBonus = count;
        JsonHelper.SaveData(currentData);
    }

    public static int LoadWelcomeBonus()
        => currentData.WelcomeBonus;

    #endregion

    #region CurrentStreak

    public static void SaveCurrentStreak(int count)
    {
        currentData.CurrentStreak = count;
        JsonHelper.SaveData(currentData);
    }

    public static int LoadCurrentStreak()
        => currentData.CurrentStreak;

    #endregion

    #region LastStateGame

    public static void SaveLastStateGame(bool state)
    {
        currentData.LastStateGame = state;
        JsonHelper.SaveData(currentData);
    }

    public static bool LoadLastStateGame()
        => currentData.LastStateGame;

    public static bool HasLastStateGame()
        => currentData.LastStateGame == false;

    #endregion

    #region SoundMute

    public static void SaveSoundMuteState(bool state)
    {
        currentData.SoundMute = state;
        JsonHelper.SaveData(currentData);
    }

    public static bool LoadSoundMuteState()
        => currentData.SoundMute;

    #endregion

    #region MusicMute

    public static void SaveMusicMuteState(bool state)
    {
        currentData.MusicMute = state;
        JsonHelper.SaveData(currentData);
    }

    public static bool LoadMusicMuteState()
        => currentData.MusicMute;

    #endregion
    
    #region FirstEnterStats

    public static void SaveFirstEnterStats(bool state)
    {
        currentData.FirstEnterStats = state;
        JsonHelper.SaveData(currentData);
    }

    public static bool LoadFirstEnterStats()
        => currentData.FirstEnterStats;

    #endregion
    
    #region Nickname

    public static void SaveNickname(string name)
    {
        currentData.Nickname = name;
        JsonHelper.SaveData(currentData);
    }

    public static string LoadNickname()
        => currentData.Nickname;

    #endregion

}