using System;
using UnityEngine;
using Firebase.Database;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class DBSaveLoad : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _userPrefabStats = new List<GameObject>(); // Префаб для отображения пользователя

    [SerializeField] private InputField _nameInputField; // InputField для ввода никнейма
    [SerializeField] private Button _verificateName;
    private DatabaseReference _dbRef;

    public void Awake()
    {
        _dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        StartCoroutine(LoadData());

        if (SaveManager.LoadFirstEnterStats())
            SaveData(SaveManager.LoadNickname(), SaveManager.LoadBestScore());
    }

    private void OnEnable()
    {
        _verificateName.onClick.AddListener(EnterNickName);
    }

    private void OnDisable()
    {
        _verificateName.onClick.RemoveListener(EnterNickName);
    }

    private void EnterNickName()
    {
        SaveManager.SaveNickname(_nameInputField.text);
        SaveData(SaveManager.LoadNickname(), SaveManager.LoadBestScore());
        StartCoroutine(LoadData());
    }

    public void SaveData(string nameUser, int bestDistance)
    {
        _dbRef.Child("Users").Child(nameUser).Child("bestScore").SetValueAsync(bestDistance);
    }

    private IEnumerator LoadData()
    {
        // Запрашиваем все данные из узла "Users"
        var status = _dbRef.Child("Users").GetValueAsync();
        yield return new WaitUntil(predicate: () => status.IsCompleted);

        if (status.Exception != null)
        {
            Debug.LogError(status.Exception);
        }
        else
        {
            DataSnapshot snapshot = status.Result;
            List<User> users = new List<User>();

            // Проходим по каждому пользователю в базе данных
            foreach (DataSnapshot userSnapshot in snapshot.Children)
            {
                string name = userSnapshot.Key;
                int bestResult = 0;

                if (userSnapshot.Child("bestScore").Value != null)
                {
                    int.TryParse(userSnapshot.Child("bestScore").Value.ToString(), out bestResult);
                }

                users.Add(new User { Name = name, BestResult = bestResult });
            }

            // Сортируем пользователей по лучшему рекорду от лучшего к меньшему
            users.Sort((a, b) => b.BestResult.CompareTo(a.BestResult));

            // Обновляем UI
            UpdateUI(users);
        }
    }

    private void UpdateUI(List<User> users)
    {
        for (int i = 0; i < _userPrefabStats.Count; i++)
        {
            if (users.Count == i)
                break;

            _userPrefabStats[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text =
                users[i].Name + " " + users[i].BestResult;
        }
    }
}

public class User
{
    public string Name { get; set; }
    public int BestResult { get; set; }
}