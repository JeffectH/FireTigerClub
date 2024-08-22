using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadWebView : MonoBehaviour
{
    public event Action FirstPlayGameWithoutWebView;
    public event Action PlayGameWithoutWebView;

    [SerializeField] private RemoteConfig _remoteConfig;
    [SerializeField] private Web _webView;
    [SerializeField] private NotificationMessage _notificationMessage;

    [Header("LoadinLine")] [SerializeField]
    private Image _loadingLine;

    public void Start()
    {
        Debug.Log("LoadFirsRun " + SaveManager.LoadFirstRun());

        //if (SaveManager.LoadFirstRun() == false)

        if (PlayerPrefs.HasKey("URL") == false)
        {
            _remoteConfig.Initialize();
        }

        StartCoroutine(LoadingScreen());

        //if (SaveManager.LoadFirstRun())
        if (PlayerPrefs.HasKey("URL"))
            OpenWebAndGame();
    }

    private void OnEnable()
    {
        _remoteConfig.DataLoading += OpenWebAndGame;
        _remoteConfig.NotConnect += OpenWebAndGame;
    }

    private void OnDisable()
    {
        _remoteConfig.DataLoading -= OpenWebAndGame;
        _remoteConfig.NotConnect -= OpenWebAndGame;
    }

    private IEnumerator LoadingScreen()
    {
        while (_loadingLine.fillAmount < 0.9f)
        {
            _loadingLine.fillAmount += 0.05f;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void OpenWebAndGame()
    {
        //  if (SaveManager.LoadFirstRun() == false && _remoteConfig.CurrentURL != null)
        if (PlayerPrefs.HasKey("URL") == false)
        {
            if (_remoteConfig.CurrentURL != "")
            {
                Debug.Log("FirstWebView");
                _webView.ShowUrlFullScreeNoBar(_remoteConfig.CurrentURL);
                //SaveManager.SaveURL(_remoteConfig.CurrentURL);
                PlayerPrefs.SetString("URL", _remoteConfig.CurrentURL);
                _notificationMessage.Initialize();
            }
            else
            {
                Debug.Log("FirstPlayGame");
                PlayerPrefs.SetString("URL", "");
                //SaveManager.SaveURL("");
                FirstPlayGameWithoutWebView?.Invoke();
            }
        }

        else
        {
            //if (SaveManager.LoadURL() != "")
            if (PlayerPrefs.GetString("URL") != "")
            {
                Debug.Log("WebViewPlay");
                _webView.ShowUrlFullScreeNoBar(PlayerPrefs.GetString("URL"));
                _notificationMessage.Initialize();
            }
            else
            {
                Debug.Log("PlayGame");
                PlayGameWithoutWebView?.Invoke();
            }
        }


        SaveManager.SaveGamePlay(true);

        //Debug.Log("URL " + SaveManager.LoadURL());
        Debug.Log("URL " + PlayerPrefs.GetString("URL"));
    }

    private void OnApplicationQuit()
    {
        SaveManager.SaveGamePlay(false);
        SaveManager.SaveFirstRun(true);
    }
}