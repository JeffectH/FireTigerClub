using UnityEngine;

[CreateAssetMenu(menuName = "Configs/LevelConfig", fileName = "LevelConfig")]
public class LevelStatsConfig : ScriptableObject
{
    [SerializeField, Range(1, 30)] private int _numberLevel;

    [Space] [SerializeField, Range(1, 100)]
    private float _speedItem;

    [SerializeField, Range(1, 100)] private int _missedItemCount;
    [SerializeField, Range(100, 10000)] private int _minScoreForWin;

    public float NumberLevel => _numberLevel;
    public float Speed => _speedItem;
    public int MissedItemLevel => _missedItemCount;
    
    public int MinScoreForWin => _minScoreForWin;
}