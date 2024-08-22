using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerStatsConfig _playerStats;

    public override void InstallBindings()
    {
        BindConfig();
        BindInstance();
        
        Container.BindInterfacesTo<MovementHandler>().AsSingle();
    }

    private void BindInstance()
    {
        Container.BindInterfacesTo<Player>().AsSingle();
    }

    private void BindConfig()
    {
        Container.Bind<PlayerStatsConfig>().FromInstance(_playerStats).AsSingle();
    }
}