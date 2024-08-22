using UnityEngine;
using Zenject;

public class MoveController : MonoBehaviour
{
    [SerializeField] private ItemSpawner _itemSpawner;
    [SerializeField] private Player _player;

    private ItemFactory _itemFactory;

    [Inject]
    private void Constructor(ItemFactory itemFactory)
    {
        _itemFactory = itemFactory;
    }

    public void StopMove()
    {
        _itemFactory.StomMoveItem();
        _itemSpawner.StopWork();
        _player.StomMoveFire();
    }
}