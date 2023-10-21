using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndDestroy : MonoBehaviour
{
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
            newColor.a = Mathf.Lerp(1.0f, 0.0f, t);
            panelImage.color = newColor;
            yield return null;
        }

        Destroy(gameObject);
    }
}
