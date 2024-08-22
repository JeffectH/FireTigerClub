using System;
using UnityEngine;
using UnityEngine.UI;

public class MovementHandler : MonoBehaviour, IInput
{
    public event Action RightMove;
    public event Action LeftMove;

    [SerializeField] private Button _rightBtn;
    [SerializeField] private Button _leftBtn;

    private void Start()
    {
        _rightBtn.onClick.AddListener(OnRightClickArrow);
        _leftBtn.onClick.AddListener(OnLeftClickArrow);
    }

    private void OnRightClickArrow()
    {
        RightMove?.Invoke();
    }

    private void OnLeftClickArrow()
    {
        LeftMove?.Invoke();
    }
}