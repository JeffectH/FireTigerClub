public class SceneLoadMediator
{
    private ISimpleSceneLoader _simpleSceneLoader;
    private ILevelLoader _levelLoader;

    public SceneLoadMediator(ISimpleSceneLoader simpleSceneLoader, ILevelLoader levelLoader)
    {
        _simpleSceneLoader = simpleSceneLoader;
        _levelLoader = levelLoader;
    }

    public void GoToGamePlayLevel(LevelLoadingData levelLoadingData)
        => _levelLoader.Load(levelLoadingData);

    public void GoToLeveleSelectionMenu()
        => _simpleSceneLoader.Load(SceneID.LevelSelection);

    public void GoToMainMenu()
        => _simpleSceneLoader.Load(SceneID.MainMenu);
}