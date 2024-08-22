using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private List<Transform> _spawnPoints;
    private ItemFactory _itemFactory;

    private Coroutine _spawn;

    [Inject]
    private void Construct(ItemFactory itemFactory)
        => _itemFactory = itemFactory;
    
    public void StartWork()
    {
        StopWork();

        _spawn = StartCoroutine(Spawn());
    }

    public void StopWork()
    {
        if (_spawn != null)
            StopCoroutine(_spawn);
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
             Item item =_itemFactory.Get((ItemType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(ItemType)).Length));
            item.MoveTo(_spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)].position);
          
            yield return new WaitForSeconds(_spawnCooldown);
        }
    }
}
