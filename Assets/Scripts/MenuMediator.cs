using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuMediator : MonoBehaviour
{
    public event Action OpenPPandTU;

    [SerializeField] private Button _ppBtn;
    [SerializeField] private Button _tuBtn;

    private bool _isOpenPP;
    private bool _isOpenTU;

    [SerializeField]private Web _web;

    private void Awake()
    {

        _ppBtn.onClick.AddListener(OpenPrivacyPolicy);
        _tuBtn.onClick.AddListener(OpenTermOfUse);
    }

    private void OpenPrivacyPolicy()
    {
        _web.ShowUrlFullScreen(_web.GetURLPP());
        _isOpenPP = true;

        OpenPPTU();
    }

    private void OpenTermOfUse()
    {
        _web.ShowUrlFullScreen(_web.GetURLTU());
        _isOpenTU = true;

        OpenPPTU();
    }

    private void OpenPPTU()
    {
        if (_isOpenPP && _isOpenTU)
            OpenPPandTU?.Invoke();
    }


}