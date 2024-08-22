using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _PPandTUScreen;
    [SerializeField] private GameObject _slotMachineScreen;
    [SerializeField] private GameObject _welcomeBonusScreen;
    [SerializeField] private GameObject _bonusScreen;
    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private Button _continueButton;
    [Space] [SerializeField] private MenuMediator _menuMediator;
    [Space] [SerializeField] private LoadWebView _loadWebView;
    [Space] [SerializeField] private SlotMachine _slotMachine;
    [Space] [SerializeField] private Image _winScoreSprite;
    [SerializeField] private TextMeshProUGUI _currentBestScore;

    [Header("Button Play")] [SerializeField]
    private Button _playButton;

    private const string ConfigPath = "BonusSprite";

    private const string BonusSprite100 = "100";
    private const string BonusSprite200 = "200";
    private const string BonusSprite300 = "300";
    private const string BonusSprite400 = "400";
    private const string BonusSprite500 = "500";

    private Sprite _bonus100;
    private Sprite _bonus200;
    private Sprite _bonus300;
    private Sprite _bonus400;
    private Sprite _bonus500;

    private Dictionary<int, Sprite> _bonusSprites = new Dictionary<int, Sprite>();

    [SerializeField] private Sprite _activePlayButton;

    [Space] [SerializeField] private Button _muteSound;
    [Space] [SerializeField] private Button _muteMusic;
    [Space] [SerializeField] private GameObject _inputPanel;
    [SerializeField] private Button _stats;

    private const string SoundSpritePath = "Sprites/Setting";
    private const string SoundOffSprite = "OffSound";
    private const string SoundOnSprite = "OnSound";

    private void Start()
    {
        Load();

        _currentBestScore.text = SaveManager.LoadBestScore().ToString();

        if (SaveManager.LoadFirstRun())
            _bonusScreen.SetActive(false);

        UpdateSoundButton();
    }

    private void OnEnable()
    {
        _menuMediator.OpenPPandTU += ActivePlayButton;
        _loadWebView.FirstPlayGameWithoutWebView += HideLoadingScreenFirstRunGame;
        _loadWebView.PlayGameWithoutWebView += HideLoadingScreen;
        _slotMachine.FirstPlay += HideSloteMachine;

        _muteMusic.onClick.AddListener(MuteMusic);
        _muteSound.onClick.AddListener(MuteSound);
        _stats.onClick.AddListener(CheackFirstEnterStats);
    }

    private void OnDisable()
    {
        _menuMediator.OpenPPandTU -= ActivePlayButton;
        _loadWebView.FirstPlayGameWithoutWebView -= HideLoadingScreenFirstRunGame;
        _loadWebView.PlayGameWithoutWebView -= HideLoadingScreen;
        _slotMachine.FirstPlay -= HideSloteMachine;

        _muteMusic.onClick.RemoveListener(MuteMusic);
        _muteSound.onClick.RemoveListener(MuteSound);
        _stats.onClick.RemoveListener(CheackFirstEnterStats);
    }

    private void CheackFirstEnterStats()
    {
        if (SaveManager.LoadFirstEnterStats() == false)
            _inputPanel.SetActive(true);

        SaveManager.SaveFirstEnterStats(true);
    }

    private void MuteSound()
    {
        SoundManagerMenu.Instance.MuteSound();
        UpdateSoundButton();
    }

    private void MuteMusic()
    {
        SoundManagerMenu.Instance.MuteMusic();
        UpdateSoundButton();
    }

    private void UpdateSoundButton()
    {
        if (SoundManagerMenu.Instance.IsMuteSound)
        {
            _muteSound.GetComponent<Image>().sprite =
                Resources.Load<Sprite>(Path.Combine(SoundSpritePath, SoundOffSprite));
        }
        else
        {
            _muteSound.GetComponent<Image>().sprite =
                Resources.Load<Sprite>(Path.Combine(SoundSpritePath, SoundOnSprite));
        }

        if (SoundManagerMenu.Instance.IsMuteMusic)
        {
            _muteMusic.GetComponent<Image>().sprite =
                Resources.Load<Sprite>(Path.Combine(SoundSpritePath, SoundOffSprite));
        }
        else
        {
            _muteMusic.GetComponent<Image>().sprite =
                Resources.Load<Sprite>(Path.Combine(SoundSpritePath, SoundOnSprite));
        }
    }

    private void ActivePlayButton()
    {
        _playButton.interactable = true;
        _playButton.GetComponent<Image>().sprite = _activePlayButton;
    }

    private void HideLoadingScreenFirstRunGame()
    {
        _PPandTUScreen.SetActive(true);
        _loadingScreen.GetComponent<CanvasGroup>().DOFade(0, 0.5f);

        Invoke(nameof(ShowPPandTUScreen), 0.5f);
    }

    private void ShowPPandTUScreen()
    {
        _PPandTUScreen.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        _loadingScreen.SetActive(false);
    }

    private void HideLoadingScreen()
    {
        _loadingScreen.GetComponent<CanvasGroup>().DOFade(0, 0.5f);

        Invoke(nameof(ShowMenuGame), 0.5f);

        Debug.Log("HideLoadinScreen");
    }

    public void ShowMenuGame()
    {
        _menuScreen.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        _loadingScreen.SetActive(false);

        SaveManager.SaveFirstRun(true);
    }

    private void HideSloteMachine()
    {
        _slotMachineScreen.GetComponent<CanvasGroup>().DOFade(0, 0.5f);

        SoundManagerMenu.Instance.PlaySoundSlotMachineWin();

        Invoke(nameof(ShowBonus), 0.5f);
    }

    private void ShowBonus()
    {
        _welcomeBonusScreen.GetComponent<CanvasGroup>().DOFade(1, 0.5f);

        _winScoreSprite.sprite = _bonusSprites[_slotMachine.RandomBonus];
    }

    private void Load()
    {
        _bonus100 = Resources.Load<Sprite>(Path.Combine(ConfigPath, BonusSprite100));
        _bonus200 = Resources.Load<Sprite>(Path.Combine(ConfigPath, BonusSprite200));
        _bonus300 = Resources.Load<Sprite>(Path.Combine(ConfigPath, BonusSprite300));
        _bonus400 = Resources.Load<Sprite>(Path.Combine(ConfigPath, BonusSprite400));
        _bonus500 = Resources.Load<Sprite>(Path.Combine(ConfigPath, BonusSprite500));

        _bonusSprites.Add(1, _bonus100);
        _bonusSprites.Add(2, _bonus200);
        _bonusSprites.Add(3, _bonus300);
        _bonusSprites.Add(4, _bonus400);
        _bonusSprites.Add(5, _bonus500);
    }
}