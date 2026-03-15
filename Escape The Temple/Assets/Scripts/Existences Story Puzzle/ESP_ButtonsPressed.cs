using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESP_ButtonsPressed : MonoBehaviour
{
    public float pressDepth = 0.02f;
    public float pressTime = 0.1f;
    public float returnTime = 0.15f;

    bool isAnimating = false;

    public void Press()
    {
        if (!isAnimating)
            StartCoroutine(PressAnimation());
    }

    IEnumerator PressAnimation()
    {
        isAnimating = true;

        Vector3 startPos = transform.localPosition;

        Vector3 pressedPos = startPos + Vector3.down * pressDepth;
        float t = 0;

        while (t < pressTime)
        {
            transform.localPosition = Vector3.Lerp(startPos, pressedPos, t / pressTime);
            t += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = pressedPos;

        t = 0;

        while (t < returnTime)
        {
            transform.localPosition = Vector3.Lerp(pressedPos, startPos, t / returnTime);
            t += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPos;
        isAnimating = false;
    }
}
