using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PlayerConfig", fileName = "PlayerConfig")]
public class PlayerStatsConfig : ScriptableObject
{
    [field: SerializeField, Range(1, 10)] public int SpeedMove { get; private set; }
    [field: SerializeField, Range(1, 10)] public float SpeedDamageRate { get; private set; }
    [field: SerializeField, Range(0, 10)] public float SpeedAttack { get; private set; }

    [SerializeField] private Fire _prefabFire;
    public Fire PrefabFire => _prefabFire;
}