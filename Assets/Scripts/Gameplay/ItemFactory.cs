using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

public class ItemFactory
{
    private const string ConfigPath = "Items";

    private const string ItemConfig1 = "ItemConfig_1";
    private const string ItemConfig2 = "ItemConfig_2";
    private const string ItemConfig3 = "ItemConfig_3";
    private const string ItemConfig4 = "ItemConfig_4";
    private const string ItemConfig5 = "ItemConfig_5";
    private const string ItemConfig6 = "ItemConfig_6";
    private const string ItemConfig7 = "ItemConfig_7";
    private const string ItemConfig8 = "ItemConfig_8";
    private const string ItemConfig9 = "ItemConfig_9";

    private ItemConfig _itemConfig1,
        _itemConfig2,
        _itemConfig3,
        _itemConfig4,
        _itemConfig5,
        _itemConfig6,
        _itemConfig7,
        _itemConfig8,
        _itemConfig9;

    private DiContainer _diContainer;

    private LevelLoadingData _levelLoadingData;
    private GameManager _gameManager;

    private List<Item> _items = new List<Item>();

    [Inject]
    private void Construct(LevelLoadingData levelLoadingData, GameManager gameManager)
    {
        _levelLoadingData = levelLoadingData;
        _gameManager = gameManager;
    }

    public ItemFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
        Load();
    }

    private void UpdateCountMissedItems(Item item)
    {
        _gameManager.CountingMissedItems();
        item.DeathOnLine -= UpdateCountMissedItems;
    }

    public Item Get(ItemType itemType)
    {
        ItemConfig config = GetConfig(itemType);

        Item instance = _diContainer.InstantiatePrefabForComponent<Item>(config.Prefab);
        instance.Initialize(_levelLoadingData.SpeedItem);
        instance.DeathOnLine += UpdateCountMissedItems;

        _items.Add(instance);

        instance.OnItemDestroyed += HandleItemDestroyed; // Подписка на событие уничтожения
        return instance;
    }

    private void HandleItemDestroyed(Item item)
    {
        _items.Remove(item);
        item.OnItemDestroyed -= HandleItemDestroyed; // Отписка от события
    }

    public void StomMoveItem()
    {
        foreach (var item in _items)
            item.StopMove();
    }

    private ItemConfig GetConfig(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.FlashLight1:
                return _itemConfig1;

            case ItemType.FlashLight2:
                return _itemConfig2;

            case ItemType.FlashLight3:
                return _itemConfig3;

            case ItemType.FlashLight4:
                return _itemConfig4;

            case ItemType.FlashLight5:
                return _itemConfig5;

            case ItemType.FlashLight6:
                return _itemConfig6;

            case ItemType.FlashLight7:
                return _itemConfig7;

            case ItemType.FlashLight8:
                return _itemConfig8;

            case ItemType.FlashLight9:
                return _itemConfig9;

            default:
                throw new ArgumentException(nameof(itemType));
        }
    }

    private void Load()
    {
        _itemConfig1 = Resources.Load<ItemConfig>(Path.Combine(ConfigPath, ItemConfig1));
        _itemConfig2 = Resources.Load<ItemConfig>(Path.Combine(ConfigPath, ItemConfig2));
        _itemConfig3 = Resources.Load<ItemConfig>(Path.Combine(ConfigPath, ItemConfig3));
        _itemConfig4 = Resources.Load<ItemConfig>(Path.Combine(ConfigPath, ItemConfig4));
        _itemConfig5 = Resources.Load<ItemConfig>(Path.Combine(ConfigPath, ItemConfig5));
        _itemConfig6 = Resources.Load<ItemConfig>(Path.Combine(ConfigPath, ItemConfig6));
        _itemConfig7 = Resources.Load<ItemConfig>(Path.Combine(ConfigPath, ItemConfig7));
        _itemConfig8 = Resources.Load<ItemConfig>(Path.Combine(ConfigPath, ItemConfig8));
        _itemConfig9 = Resources.Load<ItemConfig>(Path.Combine(ConfigPath, ItemConfig9));
    }
}