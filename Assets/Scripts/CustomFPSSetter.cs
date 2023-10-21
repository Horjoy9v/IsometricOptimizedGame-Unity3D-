using UnityEngine;

public class CustomFPSSetter : MonoBehaviour
{
    public int targetFPS = 5000;

    void Start()
    {
        QualitySettings.vSyncCount = 0; // Відключення VSync
        Application.targetFrameRate = targetFPS;
    }
}
