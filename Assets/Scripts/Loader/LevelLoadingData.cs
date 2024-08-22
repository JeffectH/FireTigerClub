using System;

public class LevelLoadingData
{
    private int _level;
    private float _speedItem;
    private int _missedItemCount;
    private int _minScorePointsForWin;

    public LevelLoadingData(int level, float speedItem, int missedItemCount, int minScorePointsForWin)
    {
        Level = level;
        SpeedItem = speedItem;
        MissedItemCount = missedItemCount;
        MinScorePointForWin = minScorePointsForWin;
    }

    public int Level
    {
        get => _level;
        set
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _level = value;
        }
    }
    
    public float SpeedItem
    {
        get => _speedItem;
        set
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _speedItem = value;
        }
    }
    public int MissedItemCount
    {
        get => _missedItemCount;
        set
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _missedItemCount = value;
        }
    }
    
    public int MinScorePointForWin
    {
        get => _minScorePointsForWin;
        set
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _minScorePointsForWin = value;
        }
    }
    
}