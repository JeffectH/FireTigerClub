using System;
using Zenject;

public class GameManager
{
    public event Action<bool> EndGame;
    public event Action<bool> WinGame;
    public event Action MissedItem;

    private LevelLoadingData _levelLoadingData;
    private int _currentMissedItems = 0;

    private int _numberScoreForWin = 100;

    public int CurrentMissedItems => _currentMissedItems;

    [Inject]
    public GameManager(LevelLoadingData levelLoadingData)
    {
        _levelLoadingData = levelLoadingData;
        _numberScoreForWin = _levelLoadingData.MinScorePointForWin;
    }

    public void CountingMissedItems()
    {
        _currentMissedItems++;
        
        MissedItem?.Invoke();
        
        if (_currentMissedItems >= _levelLoadingData.MissedItemCount)
        {
            if (Score.CurrentScore >= _numberScoreForWin)
            {
                HandleWinScenario();
            }
            else
            {
                HandleLossScenario();
            }
        }
    }

    private void HandleWinScenario()
    {
        if (SaveManager.LoadLastStateGame())
        {
            SaveManager.SaveCurrentStreak(SaveManager.LoadCurrentStreak() + 1);

            if (SaveManager.LoadCurrentStreak() == 3)
            {
                WinGame?.Invoke(true);
                SaveManager.SaveCurrentStreak(0);
            }
            else
            {
                WinGame?.Invoke(false);
            }
        }
        else
        {
            WinGame?.Invoke(false);
            SaveManager.SaveCurrentStreak(1);
        }

        SaveManager.SaveLastStateGame(true);
    }

    private void HandleLossScenario()
    {
        Score.ZeroingOutScore();

        if (SaveManager.LoadLastStateGame() == false)
        {
            SaveManager.SaveCurrentStreak(SaveManager.LoadCurrentStreak() + 1);

            if (SaveManager.LoadCurrentStreak() == 3)
            {
                EndGame?.Invoke(true);
                SaveManager.SaveCurrentStreak(0);
            }
            else
            {
                EndGame?.Invoke(false);
            }
        }
        else
        {
            EndGame?.Invoke(false);
            SaveManager.SaveCurrentStreak(1);
        }

        SaveManager.SaveLastStateGame(false);
    }
}