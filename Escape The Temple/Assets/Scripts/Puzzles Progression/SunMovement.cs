using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public Light directionalLight;
    public Material skyboxMaterial;
    public ParticleSystem stars;

    public int totalPuzzles = 10;
    private int progressionIndex = 0;

    [Header("Etapas")]
    public int sunsetIndex = 6;
    //public int nightIndex = 10;

    [Header("Rotation")]
    public float startAngle = 80f;
    public float sunsetAngle = 20f;
    public float nightAngle = 0f;

    [Header("Directional Light Intensity")]
    public float startIntensity = 0.5f;
    public float sunsetIntensity = 1.5f;
    public float nightIntensity = 0f;

    [Header("Directional Light Color")]
    Color startColor = new Color(0.9547169f, 0.9149585f, 0.7691776f);
    Color sunsetColor = new Color(1f, 0.7882353f, 0f);
    Color nightColor = new Color(0f, 0f, 0f);

    [Header("Ambient Color")]
    Color dayAmbient = new Color(0.6150587f, 0.6553221f, 0.735849f);
    Color sunsetAmbient = new Color(0.53f, 0.53f, 0.53f);
    Color nightAmbient = new Color(0.2980392f, 0.2980392f, 0.2980392f);


    [Header("Procedural Skybox")]
    //public Color daySkyColor = new Color(1f, 0.5f, 0.2f);
    //public Color dayGroundColor = new Color(0.6f, 0.3f, 0.2f);
    //public Color sunsetSkyColor = new Color(1f, 0.5f, 0.2f);
    Color sunsetGroundColor = new Color(0.3689999f, 0.349f, 0.3409999f);

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
            stars.gameObject.SetActive(true);
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
        skyboxMaterial.SetFloat("_AtmosphereThickness", Mathf.Lerp(1f, 1.94f, t));


        //skyboxMaterial.SetColor("_SkyTint", Color.Lerp(daySkyColor, sunsetSkyColor, t));
        //skyboxMaterial.SetColor("_GroundColor", Color.Lerp(dayGroundColor, sunsetGroundColor, t));
    }
    void UpdateSkyboxSunsetToNight(float t)
    {
        skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(1.41f, 0.01f, t));

        //Color skyColor = Color.Lerp(sunsetSkyColor, Color.black, t);
        Color groundColor = Color.Lerp(sunsetGroundColor, Color.black, t);

        //skyboxMaterial.SetColor("_SkyTint", skyColor);
        skyboxMaterial.SetColor("_GroundColor", groundColor);
    }
}

