using System;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPageLevel : MonoBehaviour
{
    [SerializeField] private Button _next;
    [SerializeField] private Button _back;
    [SerializeField] private GameObject[] _pages;

    private int _currentPage = 1;

    private void OnEnable()
    {
        _next.onClick.AddListener(NextPage);
        _back.onClick.AddListener(BackPage);
    }

    private void OnDisable()
    {
        _next.onClick.RemoveListener(NextPage);
        _back.onClick.RemoveListener(BackPage);
    }

    private void NextPage()
    {
        if (_currentPage >= _pages.Length)
            return;

        _pages[_currentPage - 1].SetActive(false);
        _currentPage++;
        _pages[_currentPage - 1].SetActive(true);
    }

    private void BackPage()
    {
        if (_currentPage <= 1)
            return;

        _pages[_currentPage - 1].SetActive(false);
        _currentPage--;
        _pages[_currentPage - 1].SetActive(true);
    }
}