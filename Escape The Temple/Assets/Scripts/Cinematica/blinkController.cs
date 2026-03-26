using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blinkController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeOutTime = 0.3f;
    public float holdTime = 1f;
    public float fadeInTime = 0.3f;
    cinematicaController cinematicaController;

    private void Start()
    {
        cinematicaController = FindObjectOfType<cinematicaController>();
    }
    public IEnumerator PlayBlink(bool endKinematics)
    {
        //StartCoroutine(BlinkCoroutine());
        yield return StartCoroutine(BlinkCoroutine(endKinematics));
    }

    IEnumerator BlinkCoroutine(bool endKinematics)
    {
        fadeImage.gameObject.SetActive(true);

        float t = 0;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, t / fadeOutTime);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(1);


        if(endKinematics)
        {
            cinematicaController.goBackToMenu();
        }
        else
        {
            cinematicaController.mainCamera.transform.position = cinematicaController.cameraSpots[cinematicaController.spotIndex].transform.position;
            cinematicaController.mainCamera.transform.rotation = cinematicaController.cameraSpots[cinematicaController.spotIndex].transform.rotation;

            yield return new WaitForSeconds(holdTime);

            t = 0;
            while (t < fadeInTime)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(1, 0, t / fadeInTime);
                SetAlpha(alpha);
                yield return null;
            }

            SetAlpha(0);
            fadeImage.gameObject.SetActive(false);
        }
    }

    void SetAlpha(float a)
    {
        Color c = fadeImage.color;
        c.a = a;
        fadeImage.color = c;
    }
}
