using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndDestroy : MonoBehaviour
{
    public bool reverse;
    public float fadeDuration = 2.0f;

    private Image panelImage;
    private float startTime;

    void Start()
    {
        panelImage = GetComponent<Image>();
        startTime = Time.time;
        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        while (Time.time - startTime < fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;
            Color newColor = panelImage.color;

            if (reverse)
                newColor.a = Mathf.Lerp(0.0f, 1.0f, t); // Інвертуємо інтерполяцію для чорної панелі
            else
                newColor.a = Mathf.Lerp(1.0f, 0.0f, t);

            panelImage.color = newColor;
            yield return null;
        }

        Destroy(gameObject);
    }
}
