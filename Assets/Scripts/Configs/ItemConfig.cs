using UnityEngine;

[CreateAssetMenu (menuName = "Configs/ItemConfig", fileName = "ItemConfig")]
public class ItemConfig: ScriptableObject
{
    [SerializeField] private Item _prefab;
    [SerializeField, Range(1, 1)] private int _health;

    public Item Prefab => _prefab;
    public int Health => _health;
}