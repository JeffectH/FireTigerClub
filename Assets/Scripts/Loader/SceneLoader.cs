using System;

public class SceneLoader : ISimpleSceneLoader, ILevelLoader
{
    private ZenjectSceneLoaderWrapper _zenjectSceneLoader;

    public SceneLoader(ZenjectSceneLoaderWrapper zenjectSceneLoader)
    {
        _zenjectSceneLoader = zenjectSceneLoader;
    }

    public void Load(LevelLoadingData levelLoadingData)
    {
        _zenjectSceneLoader.Load((int)SceneID.GameplayLevel,
            container =>
            {
                container.BindInstance(levelLoadingData).AsSingle();
            }
        );
    }

    public void Load(SceneID sceneID)
    {
        if (sceneID == SceneID.GameplayLevel)
            throw new ArgumentException(
                $"{SceneID.GameplayLevel} cannot be started without configuration, use ILevelLoader");

        _zenjectSceneLoader.Load((int)sceneID);
    }
}