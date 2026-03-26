using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class fadeTitle : MonoBehaviour
{
    public float fadeDuration = 1f;

    Coroutine currentFade;
    public blinkController blink;
    public void FadeIn(TMP_Text text)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(Fade(0f, 1f, false, text));
    }

    public void FadeOut(TMP_Text text)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(Fade(1f, 0f, true, text));
    }

    IEnumerator Fade(float startAlpha, float endAlpha, bool fadingOut, TMP_Text text)
    {
        float time = 0f;
        Color color = text.color;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            text.color = new Color(color.r, color.g, color.b, alpha);

            time += Time.deltaTime;
            yield return null;
        }

        text.color = new Color(color.r, color.g, color.b, endAlpha);

        
        if(!fadingOut)
        {
            yield return new WaitForSeconds(2f);

            //FadeOut(text);
            StartCoroutine(blink.PlayBlink(true));
        }
    }
}
