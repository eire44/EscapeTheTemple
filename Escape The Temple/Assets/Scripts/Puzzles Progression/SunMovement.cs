using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public Light directionalLight;

    public int totalPuzzles = 10;
    private int progressionIndex = 0;

    public float startAngle = 90f;
    public float endAngle = 20f;

    public float startIntensity = 1f;
    public float endIntensity = 0.5f;

    public Color startColor = Color.white;
    public Color endColor = new Color(1f, 0.82f, 0.14f);

    public void sunProgression()
    {
        Debug.Log("SUN MOVED");
        if (progressionIndex >= totalPuzzles)
            return;

        progressionIndex++;

        float t = (float)progressionIndex / totalPuzzles;

        float angle = Mathf.Lerp(startAngle, endAngle, t);
        directionalLight.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
        directionalLight.intensity = Mathf.Lerp(startIntensity, endIntensity, t);
        directionalLight.color = Color.Lerp(startColor, endColor, t);
    }
}

