using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interiorLanternsController : MonoBehaviour
{
    public Light pointLight;
    public float maxIntensity = 5f;
    public float fadeDuration = 1.5f;
    Coroutine currentRoutine;
    public AudioSource audiosource;

    public float maxFlickerIntensity = 4f;
    bool isFlickering = false;

    bool isLightOn = false;
    bool flag = true;

    private void Awake()
    {
        if (pointLight == null)
            pointLight = GetComponentInChildren<Light>();
    }

    void Start()
    {
        pointLight.intensity = 0f;
    }

    private void Update()
    {
        if (isFlickering)
        {
            if(isLightOn)
            {
                if (flag)
                {
                    flag = false;
                    TurnOff();
                }
            }
            else
            {
                if(flag)
                {
                    flag = false;
                    TurnOn();
                }
            }
        }
    }

    public void StartFlicker()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        isFlickering = true;
    }

    public void StopFlicker()
    {
        isFlickering = false;
    }

    public void TurnOn()
    {
        float intensity = maxIntensity;
        if(!isFlickering)
        {
            audiosource.Play();
        }
        else
        {
            intensity = maxFlickerIntensity;
        }
        

        StartFade(intensity, true);
    }
    public void TurnOff()
    {
        float intensity = 0f;
        if (isFlickering)
        {
            intensity = 0.2f;
        }
        StartFade(intensity, false);
    }

    void StartFade(float target, bool on)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(FadeLight(target, on));
    }

    IEnumerator FadeLight(float target, bool on)
    {
        float start = pointLight.intensity;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            float currentIntensity = Mathf.Lerp(start, target, t);
            pointLight.intensity = currentIntensity;

            yield return null;
        }

        pointLight.intensity = target;
        flag = true;
        isLightOn = on;
    }
}
