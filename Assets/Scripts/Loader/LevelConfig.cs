using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    [SerializeField] private LevelStatsConfig[] _levelStatsConfigs;

    public float GetCurrentItemSpeed(int level)
    {
        foreach (LevelStatsConfig config in _levelStatsConfigs)
        {
            if (Mathf.Approximately(config.NumberLevel, level))
            {
                return config.Speed;
            }
        }

        return 1;
    }
    
    public int GetMissetItemCount(int level)
    {
        foreach (LevelStatsConfig config in _levelStatsConfigs)
        {
            if (Mathf.Approximately(config.NumberLevel, level))
            {
                return config.MissedItemLevel;
            }
        }

        return 1;
    }
    
    public int GetNumberMinScorePoints(int level)
    {
        foreach (LevelStatsConfig config in _levelStatsConfigs)
        {
            if (Mathf.Approximately(config.NumberLevel, level))
            {
                return config.MinScoreForWin;
            }
        }

        return 1;
    }
    
}