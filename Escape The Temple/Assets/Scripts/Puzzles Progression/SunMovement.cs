using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public Light directionalLight;
    public Material skyboxMaterial;

    public int totalPuzzles = 10;
    private int progressionIndex = 0;

    [Header("Etapas")]
    public int sunsetIndex = 6;
    public int nightIndex = 10;

    [Header("Rotación")]
    public float startAngle = 80f;
    public float sunsetAngle = 20f;
    public float nightAngle = 0f;

    [Header("Intensidad")]
    public float startIntensity = 0.5f;
    public float sunsetIntensity = 1.5f;
    public float nightIntensity = 0f;

    [Header("Color luz")]
    public Color startColor = new Color(1f, 0.95f, 0.8f);
    public Color sunsetColor = new Color(1f, 0.6f, 0.2f);
    public Color nightColor = new Color(0.2f, 0.3f, 0.5f);

    [Header("Ambient")]
    public Color dayAmbient = new Color(0.7f, 0.7f, 0.7f);
    public Color sunsetAmbient = new Color(0.4f, 0.3f, 0.25f);
    public Color nightAmbient = new Color(0.1f, 0.1f, 0.15f);


    public Color daySkyColor = new Color(1f, 0.5f, 0.2f);
    public Color dayGroundColor = new Color(0.6f, 0.3f, 0.2f);
    public Color sunsetSkyColor = new Color(1f, 0.5f, 0.2f);
    public Color sunsetGroundColor = new Color(0.6f, 0.3f, 0.2f);

    private void Start()
    {
        skyboxMaterial = new Material(skyboxMaterial);
        RenderSettings.skybox = skyboxMaterial;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            sunProgression();
        }
    }

    public void sunProgression()
    {
        if (progressionIndex >= totalPuzzles)
            return;

        progressionIndex++;

        float t = (float)progressionIndex / totalPuzzles;

        float sunsetT = (float)sunsetIndex / totalPuzzles;

        float dayT = Mathf.Clamp01(t / sunsetT);
        float nightT = Mathf.Clamp01((t - sunsetT) / (1f - sunsetT));

        if (progressionIndex  < 3)
        {
            ApplySun(
                Mathf.Lerp(startAngle, sunsetAngle, dayT),
                directionalLight.intensity,
                directionalLight.color
            );
        }
        else if(progressionIndex >= 3 && progressionIndex <= sunsetIndex)
        {
            ApplySun(
                Mathf.Lerp(startAngle, sunsetAngle, dayT),
                Mathf.Lerp(startIntensity, sunsetIntensity, dayT),
                Color.Lerp(startColor, sunsetColor, dayT)
            );

            RenderSettings.ambientLight =
                Color.Lerp(dayAmbient, sunsetAmbient, dayT);

            UpdateSkyboxDayToSunset(dayT);
        }
        else
        {
            ApplySun(
                Mathf.Lerp(sunsetAngle, nightAngle, nightT),
                Mathf.Lerp(sunsetIntensity, nightIntensity, nightT),
                Color.Lerp(sunsetColor, nightColor, nightT)
            );

            RenderSettings.ambientLight =
                Color.Lerp(sunsetAmbient, nightAmbient, nightT);

            UpdateSkyboxSunsetToNight(nightT);
        }
    }

    void ApplySun(float angle, float intensity, Color color)
    {
        directionalLight.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
        directionalLight.intensity = intensity;
        directionalLight.color = color;
    }
    void UpdateSkyboxDayToSunset(float t)
    {
        skyboxMaterial.SetFloat("_SunSize", Mathf.Lerp(0.04f, 0.06f, t));
        skyboxMaterial.SetFloat("_AtmosphereThickness", Mathf.Lerp(1f, 1.5f, t));


        skyboxMaterial.SetColor("_SkyTint", Color.Lerp(daySkyColor, sunsetSkyColor, t));
        skyboxMaterial.SetColor("_GroundColor", Color.Lerp(dayGroundColor, sunsetGroundColor, t));
    }
    void UpdateSkyboxSunsetToNight(float t)
    {
        skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(1.41f, 0.1f, t));

        //Color skyColor = Color.Lerp(sunsetSkyColor, Color.black, t);
        //Color groundColor = Color.Lerp(sunsetGroundColor, Color.black, t);

        //skyboxMaterial.SetColor("_SkyTint", skyColor);
        //skyboxMaterial.SetColor("_GroundColor", groundColor);
    }


    //public Light directionalLight;

    //public int totalPuzzles = 10;
    //private int progressionIndex = 0;

    //public float startAngle = 80f;
    //public float endAngle = 20f;

    //public float startIntensity = 0.5f;
    //public float sunSetIntensity = 1f;
    //public float nightIntensity = 0.5f;

    //public Color startColor = Color.white;
    //public Color sunSetColor = new Color(1f, 0.82f, 0.14f);
    //public Color nightColor = new Color(0.2f, 0.3f, 0.5f);

    //public Color dayAmbient = new Color(0.7f, 0.7f, 0.7f);
    //public Color nightAmbient = new Color(0.1f, 0.1f, 0.15f);

    //public int nightIndex = 7;

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Y))
    //    {
    //        sunProgression();
    //    }
    //}

    //public void sunProgression()
    //{
    //    Debug.Log("SUN MOVED");
    //    if (progressionIndex >= totalPuzzles)
    //        return;

    //    progressionIndex++;

    //    //float t = (float)progressionIndex / totalPuzzles;
    //    float t = (float)progressionIndex / totalPuzzles;

    //    float dayT = Mathf.Clamp01(t / ((float)nightIndex / totalPuzzles));
    //    float nightT = Mathf.Clamp01((t - ((float)nightIndex / totalPuzzles)) / (1f - ((float)nightIndex / totalPuzzles)));

    //    float angle = Mathf.Lerp(startAngle, endAngle, t);
    //    directionalLight.transform.rotation = Quaternion.Euler(angle, 0f, 0f);

    //    if (progressionIndex < nightIndex)
    //    {
    //        directionalLight.intensity = Mathf.Lerp(startIntensity, sunSetIntensity, dayT);
    //        directionalLight.color = Color.Lerp(startColor, sunSetColor, dayT);
    //        //RenderSettings.ambientLight = Color.Lerp(dayAmbient, sunSetColor * 0.5f, dayT);
    //    }
    //    else
    //    {
    //        directionalLight.intensity = Mathf.Lerp(sunSetIntensity, nightIntensity, nightT);
    //        directionalLight.color = Color.Lerp(sunSetColor, nightColor, nightT);
    //        RenderSettings.ambientLight = Color.Lerp(sunSetColor * 0.5f, nightAmbient, nightT);
    //    }
    //    //if(progressionIndex < nightIndex)
    //    //{
    //    //    directionalLight.intensity = Mathf.Lerp(startIntensity, sunSetIntensity, t * 2);
    //    //    directionalLight.color = Color.Lerp(startColor, sunSetColor, t * 2);
    //    //}
    //    //else
    //    //{
    //    //    directionalLight.intensity = Mathf.Lerp(sunSetIntensity, nightIntensity, t * 2);
    //    //    directionalLight.color = Color.Lerp(sunSetColor, nightColor, t * 2);
    //    //}

    //}
}

