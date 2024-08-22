using UnityEngine;
using Zenject;

public class ItemSpawnerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ItemFactory>().AsSingle();
    }
}
