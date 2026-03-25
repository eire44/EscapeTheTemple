using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogController : MonoBehaviour
{
    public float startFog = 0.01f;
    public float endFog = 0.001f;

    public void ReduceFogStep(int currentStep, int totalSteps)
    {
        if (currentStep >= totalSteps) return;

        currentStep++;

        float t = (float)currentStep / totalSteps;
        float newFog = Mathf.Lerp(startFog, endFog, t);

        RenderSettings.fogDensity = newFog;
    }
}
