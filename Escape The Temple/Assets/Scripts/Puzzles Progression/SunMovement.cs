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
        //Debug.Log("SUN MOVED");
        //if (progressionIndex >= totalPuzzles)
        //    return;

        //progressionIndex++;

        //float t = (float)progressionIndex / totalPuzzles;

        //float angle = Mathf.Lerp(startAngle, endAngle, t);
        //directionalLight.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
        //directionalLight.intensity = Mathf.Lerp(startIntensity, endIntensity, t);
        //directionalLight.color = Color.Lerp(startColor, endColor, t);
    }

    //[Header("Referencias")]
    //public Light directionalLight;
    //public Material skyboxMaterial; // Arrastra aquí tu material de Skybox

    //[Header("Configuración de Progresión")]
    //public int totalPuzzles = 10;
    //private int progressionIndex = 0;

    //[Header("Propiedades de la Luz")]
    //public float startAngle = 90f;
    //public float endAngle = 20f;
    //public float startIntensity = 1f;
    //public float endIntensity = 0.5f;
    //public Color startColor = Color.white;
    //public Color endColor = new Color(1f, 0.82f, 0.14f);

    //[Header("Propiedades del Skybox")]
    //public Gradient skyColorGradient; // De celeste clarito a naranja
    //public float startAtmosphere = 0.5f; // Atmósfera delgada = colores más vivos
    //public float endAtmosphere = 1.2f;   // Atmósfera gruesa = colores de atardecer

    //public void sunProgression()
    //{
    //    if (progressionIndex >= totalPuzzles) return;

    //    progressionIndex++;
    //    float t = (float)progressionIndex / totalPuzzles;

    //    // 1. Control de la Luz Direccional
    //    float angle = Mathf.Lerp(startAngle, endAngle, t);
    //    directionalLight.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
    //    directionalLight.intensity = Mathf.Lerp(startIntensity, endIntensity, t);
    //    directionalLight.color = Color.Lerp(startColor, endColor, t);

    //    // 2. Control del Skybox
    //    if (skyboxMaterial != null)
    //    {
    //        // Cambiamos el tinte según tu gradiente
    //        skyboxMaterial.SetColor("_SkyTint", skyColorGradient.Evaluate(t));

    //        // Ajustamos la atmósfera: esto hace que el naranja se disperse mejor al final
    //        float atmos = Mathf.Lerp(startAtmosphere, endAtmosphere, t);
    //        skyboxMaterial.SetFloat("_AtmosphereThickness", atmos);

    //        // Opcional: Oscurecer el suelo ligeramente para que no emita luz blanca
    //        Color groundColor = Color.Lerp(new Color(0.2f, 0.2f, 0.2f), new Color(0.1f, 0.05f, 0f), t);
    //        skyboxMaterial.SetColor("_GroundColor", groundColor);
    //    }

    //    // 3. Forzar actualización de iluminación global
    //    DynamicGI.UpdateEnvironment();
    //    Debug.Log($"Progreso: {progressionIndex}/{totalPuzzles} - T: {t}");
    //}
}

