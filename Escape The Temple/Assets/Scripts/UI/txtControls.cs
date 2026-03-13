using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class txtControls : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] txtControlsRows;
    int txtControlsIndex = 0;

    public float fadeDuration = 1f;

    TextMeshProUGUI tmp;
    Coroutine currentFade;
    bool allowFadeOut = true;
    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        tmp.text = txtControlsRows[txtControlsIndex];
    }

    private void Update()
    {
        if (allowFadeOut && txtControlsIndex == 0 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            allowFadeOut = false;
            FadeOut();
        } 
        else if(allowFadeOut && txtControlsIndex == 1 && Input.GetKeyDown(KeyCode.E))
        {
            allowFadeOut = false;
            FadeOut();
        }
    }


    public void FadeIn()
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(Fade(0f, 1f, false));
    }

    public void FadeOut()
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(Fade(1f, 0f, true));
    }

    IEnumerator Fade(float startAlpha, float endAlpha, bool fadingOut)
    {
        float time = 0f;
        Color color = tmp.color;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            tmp.color = new Color(color.r, color.g, color.b, alpha);

            time += Time.deltaTime;
            yield return null;
        }

        tmp.color = new Color(color.r, color.g, color.b, endAlpha);

        if (fadingOut)
        {
            txtControlsIndex++;

            if (txtControlsIndex < txtControlsRows.Length)
            {
                tmp.text = txtControlsRows[txtControlsIndex];
                FadeIn();
            }
            else
            {
                gameObject.SetActive(false);
            }
        } 
        else
        {
            allowFadeOut = true;
        }
    }
}
