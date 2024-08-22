using UnityEngine.Serialization;

[System.Serializable]
public class SaveData
{
    public string URL;
    
    public int BestScore;

    public bool FirstRun;
    public bool GamePlay;

    public int CurrentStreak;
    public bool LastStateGame;
    public int WelcomeBonus;
    
    public bool SoundMute;
    public bool MusicMute;

    [FormerlySerializedAs("FirstRnterStats")] public bool FirstEnterStats;
    public string Nickname;
}