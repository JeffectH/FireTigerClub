using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class MainMenuHUD : MonoBehaviour
{
    private Button _levelSelectionMenuButton;
    private SceneLoadMediator _loadMediator;

    [Inject]
    private void Construct(SceneLoadMediator sceneLoad)
    {
        _loadMediator = sceneLoad;
    }

    private void Awake()
        => _levelSelectionMenuButton = GetComponent<Button>();


    private void OnEnable()
        => _levelSelectionMenuButton.onClick.AddListener(OnLevelSelectionMenuClick);

    private void OnDisable()
        => _levelSelectionMenuButton.onClick.RemoveListener(OnLevelSelectionMenuClick);

    private void OnLevelSelectionMenuClick()
    {
        _loadMediator.GoToLeveleSelectionMenu();
    }
}