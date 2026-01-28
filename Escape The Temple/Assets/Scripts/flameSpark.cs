using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameSpark : MonoBehaviour
{
    Light light;
    float baseIntensity = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = baseIntensity + Mathf.Sin(Time.time * 2f) * 0.1f;
        
    }
}
