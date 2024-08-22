using UnityEngine;
using Zenject;

public class LevelSelectionPanel : MonoBehaviour
{
    [SerializeField] private LevelSelectionButton[] _levelSelectionButtons;
    [SerializeField] private LevelConfig _levelConfig;
    
    private SceneLoadMediator _loadMediator;

    [Inject]
    private void Construct(SceneLoadMediator sceneLoad)
    {
        _loadMediator = sceneLoad;
    }

    private void OnEnable()
    {
        foreach (var levelSelectionButton in _levelSelectionButtons)
            levelSelectionButton.Click += OnLevelSelected;
    }

    private void OnDisable()
    {
        foreach (var levelSelectionButton in _levelSelectionButtons)
            levelSelectionButton.Click -= OnLevelSelected;
    }

    private void OnLevelSelected(int level)
    {
        _loadMediator.GoToGamePlayLevel(new LevelLoadingData(level, _levelConfig.GetCurrentItemSpeed(level), 
            _levelConfig.GetMissetItemCount(level), _levelConfig.GetNumberMinScorePoints(level)));
    }
}