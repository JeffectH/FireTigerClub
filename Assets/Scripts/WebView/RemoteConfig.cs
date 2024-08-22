using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using System;
using UnityEngine;

public class RemoteConfig : MonoBehaviour
{
    public event Action DataLoading;
    public event Action NotConnect;
    private string _currentURL;
    public string CurrentURL => _currentURL;
    public void Initialize()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
                {
                    var dependencyStatus = task.Result;
                    if (dependencyStatus == Firebase.DependencyStatus.Available)
                    {
                        Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                        Debug.Log("initialize succsesfull");
                        FetchDataAsync();
                    }
                    else
                    {
                        Debug.LogError("not conected");
                        NotConnect?.Invoke();
                    }
                });
    }


    public Task FetchDataAsync()
    {
        Debug.Log("Fetching data...");
        Task fetchTask =
        FirebaseRemoteConfig.DefaultInstance.FetchAsync(
            TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }
    private void FetchComplete(Task fetchTask)
    {
        if (!fetchTask.IsCompleted)
        {
            Debug.LogError("Retrieval hasn't finished.");
            return;
        }

        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        var info = remoteConfig.Info;
        if (info.LastFetchStatus != LastFetchStatus.Success)
        {
            Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
            return;
        }

        // Fetch successful. Parameter values must be activated to use.
        remoteConfig.ActivateAsync()
          .ContinueWithOnMainThread(
            task =>
            {
                Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");

                _currentURL = remoteConfig.GetValue("UserRegisterLink").StringValue;

                DataLoading?.Invoke();
            });
    }
}
