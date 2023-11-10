using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    private Text fpsText;
    private float deltaTime;
    public float updateInterval = 0.5f;
    private float timeSinceLastUpdate = 0f;

    private void Start()
    {
        fpsText = GameObject.FindWithTag("FPS").GetComponent<Text>();
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
