using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exteriorLanternsController : MonoBehaviour
{
    public Light pointLight;
    public float maxIntensity = 5f;
    public float fadeDuration = 1.5f;
    public Renderer emissiveRenderer;
    Coroutine currentRoutine;
    public float emissionMultiplier = 2f;
    public AudioSource audiosource;

    private void Awake()
    {
        if (pointLight == null)
            pointLight = GetComponentInChildren<Light>();
    }

    void Start()
    {
        pointLight.intensity = 0f;
        UpdateEmission(0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            TurnOn();
        } else if (Input.GetKeyDown(KeyCode.U))
        {
            TurnOff();
        }
    }

    public void TurnOn()
    {
        audiosource.Play();
        StartFade(maxIntensity);
    }

    public void TurnOff()
    {
        StartFade(0f);
    }

    void StartFade(float target)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(FadeLight(target));
    }

    IEnumerator FadeLight(float target)
    {
        float start = pointLight.intensity;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            float currentIntensity = Mathf.Lerp(start, target, t);
            pointLight.intensity = currentIntensity;

            UpdateEmission(currentIntensity);

            yield return null;
        }

        pointLight.intensity = target;
        UpdateEmission(target);
    }

    void UpdateEmission(float lightIntensity)
    {
        if (emissiveRenderer != null)
        {
            Color baseColor = pointLight.color;
            Color emission = baseColor * lightIntensity * emissionMultiplier;
            emissiveRenderer.material.SetColor("_EmissionColor", emission);
        }
    }
}
