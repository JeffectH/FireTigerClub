using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayHUD : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _retruBtnWin;
    [SerializeField] private Button _retruBtnLose;

    private SceneLoadMediator _loadMediator;
    private LevelLoadingData _levelLoadingData;

    [Inject]
    private void Construct(SceneLoadMediator sceneLoad, LevelLoadingData levelLoadingData)
    {
        _loadMediator = sceneLoad;
        _levelLoadingData = levelLoadingData;
        Debug.Log($"Level {_levelLoadingData.Level}");
        Debug.Log($"Speed {_levelLoadingData.SpeedItem}");
    }

    private void OnEnable()
    {
        _mainMenuButton.onClick.AddListener(OnMainMenuClick);
        _retruBtnWin.onClick.AddListener(RebootLevel);
        _retruBtnLose.onClick.AddListener(RebootLevel);
    }

    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveListener(OnMainMenuClick);
        _retruBtnWin.onClick.RemoveListener(RebootLevel);
        _retruBtnLose.onClick.RemoveListener(RebootLevel);
    }

    private void OnMainMenuClick()
    {
        _loadMediator.GoToMainMenu();
    }

    private void RebootLevel()
    {
        _loadMediator.GoToGamePlayLevel(_levelLoadingData);
    }
}