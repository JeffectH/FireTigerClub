using System;
using UnityEngine;

public class BootstrapGame : MonoBehaviour
{
    [SerializeField] private ItemSpawner _itemSpawner;

    private void Awake()
    {
        Time.timeScale = 1;
        
        _itemSpawner.StartWork();
    }
}
