using System.Collections;
using System.IO;
using DG.Tweening;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIGameManager : MonoBehaviour
{
    public static UIGameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _currentScoreInCorner;
    [SerializeField] private TextMeshProUGUI _cuurentMissedItem;

    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Button _settingBtn;
    [Space] [SerializeField] private TextMeshProUGUI _infoTextStateScreen;
    [SerializeField] private TextMeshProUGUI _totalScore;
    [SerializeField] private Button _resumeSettingBtn;
    [SerializeField] private Button _resumePauseBtn;
    [Space] [SerializeField] private GameObject _winLosePauseWindow;
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _bonusWindow;
    [Space] [SerializeField] private SlotMachine _slotMachine;
    [SerializeField] private TextMeshProUGUI _bonusTextScore;
    [SerializeField] private CanvasGroup _spinBtn;

    [SerializeField] private MoveController _moveController;
    [Space] [SerializeField] private Button _muteSound;
    [Space] [SerializeField] private Button _muteMusic;
    private GameManager _gameManager;
    private LevelLoadingData _levelLoadingData;

    private const string SoundSpritePath = "Sprites/Setting";
    private const string SoundOffSprite = "OffSound";
    private const string SoundOnSprite = "OnSound";

    [Inject]
    private void Constrictor(GameManager gameManager, LevelLoadingData levelLoadingData)
    {
        _gameManager = gameManager;
        _levelLoadingData = levelLoadingData;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SaveManager.LoadWelcomeBonus() > 0)
        {
            Score.AddValueToScore(SaveManager.LoadWelcomeBonus());
            SaveManager.SaveWelcomeBonus(0);
            _currentScore.text = Score.CurrentScore.ToString();
        }

        UpdateCurrentMissedItem();
        UpdateSoundButton();
    }

    private void OnEnable()
    {
        _settingBtn.onClick.AddListener(ShowSettingScreen);
        _resumeSettingBtn.onClick.AddListener(ResumeGame);
        _resumePauseBtn.onClick.AddListener(ResumeGame);
        _pauseBtn.onClick.AddListener(ShowPauseScreen);
        _muteMusic.onClick.AddListener(MuteMusic);
        _muteSound.onClick.AddListener(MuteSound);

        _gameManager.WinGame += ShowWinScreen;
        _gameManager.EndGame += ShowLoseScreen;
        _slotMachine.EndSpin += UpdateScoreWithBonus;
        _gameManager.MissedItem += UpdateCurrentMissedItem;
    }

    private void OnDisable()
    {
        _settingBtn.onClick.RemoveListener(ShowSettingScreen);
        _resumeSettingBtn.onClick.RemoveListener(ResumeGame);
        _resumePauseBtn.onClick.RemoveListener(ResumeGame);
        _pauseBtn.onClick.RemoveListener(ShowPauseScreen);
        _muteMusic.onClick.RemoveListener(MuteMusic);
        _muteSound.onClick.RemoveListener(MuteSound);

        _gameManager.WinGame -= ShowWinScreen;
        _gameManager.EndGame -= ShowLoseScreen;
        _slotMachine.EndSpin -= UpdateScoreWithBonus;
        _gameManager.MissedItem -= UpdateCurrentMissedItem;
    }

    public void UpdateScore()
    {
        Score.AddValueToScore();
        _currentScore.text = Score.CurrentScore.ToString();
    }

    private void MuteSound()
    {
        SoundManagerGame.Instance.MuteSound();
        UpdateSoundButton();
    }

    private void MuteMusic()
    {
        SoundManagerGame.Instance.MuteMusic();
        UpdateSoundButton();
    }

    public void UpdateSoundButton()
    {
        if (SoundManagerGame.Instance.IsMuteSound)
        {
            _muteSound.GetComponent<Image>().sprite =
                Resources.Load<Sprite>(Path.Combine(SoundSpritePath, SoundOffSprite));
        }
        else
        {
            _muteSound.GetComponent<Image>().sprite =
                Resources.Load<Sprite>(Path.Combine(SoundSpritePath, SoundOnSprite));
        }

        if (SoundManagerGame.Instance.IsMuteMusic)
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

    private void UpdateCurrentMissedItem()
        => _cuurentMissedItem.text =
            "LOST " + _gameManager.CurrentMissedItems + "/" + _levelLoadingData.MissedItemCount;


    private void ShowWinScreen(bool slotMachinePlay)
    {
        if (slotMachinePlay)
        {
            _bonusWindow.SetActive(true);
            _slotMachine.StartBonus();
        }

        _winLosePauseWindow.SetActive(true);
        _winWindow.SetActive(true);
        _infoTextStateScreen.text = "WIN!";

        _totalScore.text = "Score " + Score.CurrentScore;
        _moveController.StopMove();

        _currentScoreInCorner.text = Score.CurrentScore.ToString();

        if (slotMachinePlay == false)
        {
            Score.SaveBestScore();
            SoundManagerGame.Instance.PlaySoundWinGame();
        }
    }

    private void ShowLoseScreen(bool slotMachinePlay)
    {
        if (slotMachinePlay)
        {
            _bonusWindow.SetActive(true);
            _slotMachine.StartBonus();
        }

        _winLosePauseWindow.SetActive(true);
        _loseWindow.SetActive(true);
        _infoTextStateScreen.text = "LOSE";

        _totalScore.text = "Score " + Score.CurrentScore;
        _moveController.StopMove();

        _currentScoreInCorner.text = Score.CurrentScore.ToString();

        if (slotMachinePlay == false)
        {
            Score.SaveBestScore();
            SoundManagerGame.Instance.PlaySoundSlotLoseGame();
        }
    }

    private void UpdateScoreWithBonus()
    {
        SoundManagerGame.Instance.PlaySoundSlotMachineWin();

        _bonusTextScore.text = "+" + _slotMachine.TotalScore;

        _bonusTextScore.GetComponent<CanvasGroup>().DOFade(1, 1);
        _spinBtn.DOFade(0, 1);
        Invoke(nameof(OffEnableSpinButton), 1);
        _totalScore.text = "Score " + Score.CurrentScore;

        Score.SaveBestScore();
    }

    private void OffEnableSpinButton()
        => _spinBtn.gameObject.SetActive(false);

    public void StartFadeBonusWindow()
        => StartCoroutine(FadeBonusWindow());

    private IEnumerator FadeBonusWindow()
    {
        _bonusWindow.GetComponent<CanvasGroup>().DOFade(0, 1);
        yield return new WaitForSeconds(1);
        _bonusWindow.SetActive(false);
    }

    private void ShowPauseScreen()
    {
        _infoTextStateScreen.text = "PAUSE";
        _totalScore.text = "";
        Time.timeScale = 0;
    }

    private void ShowSettingScreen()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }
}