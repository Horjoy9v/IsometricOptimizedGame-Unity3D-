using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public Text fpsText;
    private float deltaTime;
    public float updateInterval = 0.5f; // Інтервал оновлення в секундах
    private float timeSinceLastUpdate = 0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        timeSinceLastUpdate += Time.unscaledDeltaTime;
        if (timeSinceLastUpdate > updateInterval)
        {
            fpsText.text = $"{Mathf.CeilToInt(fps)}";
            timeSinceLastUpdate = 0f;
        }
    }
}
