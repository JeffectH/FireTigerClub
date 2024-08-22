using UnityEngine;

public class DisableDevelopmentConsole : MonoBehaviour
{
    private void Awake()
    {
        Debug.developerConsoleVisible = false;
    }
}
