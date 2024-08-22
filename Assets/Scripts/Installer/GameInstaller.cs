using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<MoveController>().AsSingle();
    }
}