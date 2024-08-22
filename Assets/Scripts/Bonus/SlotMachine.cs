using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SlotMachine : MonoBehaviour
{
    public event Action FirstPlay;
    public event Action EndSpin;

    public Slot[] slots;
    public float timeInterval = 0.025f;

    private int _stoppedSlots = 3;
    private bool _isSpin = false;

    private int _randomBonus;

    public int RandomBonus => _randomBonus;

    public int TotalScore { get; private set; }

    public void StartBonusFirstGame()
    {
        gameObject.SetActive(true);
        GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        Spin();
    }

    public void StartBonus()
    {
        transform.DOScale(new Vector3(7.5f, 7.5f, 7.5f), 0.5f).SetEase(Ease.InOutQuad);
    }

    private void HideBonus()
    {
        transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InOutQuad);
    }

    public void Spin()
    {
        if (_isSpin == false)
        {
            _isSpin = true;
            foreach (Slot slot in slots)
            {
                slot.StartCoroutine("Spin");
            }
        }
    }

    public void WaitResults()
    {
        _stoppedSlots -= 1;
        if (_stoppedSlots <= 0)
        {
            _stoppedSlots = 3;

            if (SaveManager.LoadFirstRun())
                CheckResults();
            else
                GenerateRandomBonus();
        }
    }

    private void GenerateRandomBonus()
    {
        _randomBonus = Random.Range(1, 6);
        SaveManager.SaveWelcomeBonus(_randomBonus * 100);
        FirstPlay?.Invoke();
    }

    public void CheckResults()
    {
        _isSpin = false;

        Dictionary<Combinations.SlotValue, int> iconValues = new Dictionary<Combinations.SlotValue, int>
        {
            { Combinations.SlotValue.Gold, 300 },
            { Combinations.SlotValue.Hat, 50 },
            { Combinations.SlotValue.Fan, 75 },
            { Combinations.SlotValue.Flashlights, 30 },
            { Combinations.SlotValue.Firecrackers, 20 }
        };

        int totalScore = 0;

        // Получение значений слотов
        Combinations.SlotValue[] stoppedSlots = new Combinations.SlotValue[3];
        for (int i = 0; i < slots.Length; i++)
        {
            stoppedSlots[i] = (Combinations.SlotValue)slots[i].gameObject.GetComponent<Slot>().StoppedSlot;
            totalScore += iconValues[stoppedSlots[i]]; // Подсчет суммы за каждую иконку
        }

        TotalScore = totalScore;

        Score.AddValueToScore(totalScore);

        EndSpin?.Invoke(); //спин завершился

        Debug.Log("Total score " + totalScore);

        //Invoke("HideBonus", 3);
    }
}

[System.Serializable]
public class Combinations
{
    public enum SlotValue
    {
        Gold,
        Hat,
        Fan,
        Flashlights,
        Firecrackers
    }

    public SlotValue FirstValue;
    public SlotValue SecondValue;
    public SlotValue ThirdValue;
    public int prize;
}