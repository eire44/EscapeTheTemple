using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxController : MonoBehaviour
{
    //public Light sun;
    //public Gradient skyTint;
    ////public Gradient groundTint;

    //void Update()
    //{
    //    float sunHeight = sun.transform.forward.y;
    //    float t = Mathf.InverseLerp(-0.2f, 0.3f, sunHeight);

    //    RenderSettings.skybox.SetColor("_SkyTint", skyTint.Evaluate(t));
    //    //RenderSettings.skybox.SetColor("_GroundColor", groundTint.Evaluate(t));

    //    // A medida que el sol baja (t disminuye), subimos la exposición 
    //    // para que el celeste claro no se vea oscuro.
    //    float boostExposure = Mathf.Lerp(1.5f, 1.0f, t);
    //    RenderSettings.skybox.SetFloat("_Exposure", boostExposure);

    //    DynamicGI.UpdateEnvironment();
    //}

    //void Update()
    //{
    //    float sunHeight = sun.transform.forward.y;

    //    float t = Mathf.InverseLerp(-0.2f, 0.3f, sunHeight);

    //    RenderSettings.skybox.SetColor("_SkyTint", skyTint.Evaluate(t));
    //    RenderSettings.skybox.SetColor("_GroundColor", groundTint.Evaluate(t));

    //    DynamicGI.UpdateEnvironment();
    //}
}
