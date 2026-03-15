using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class paperClue : MonoBehaviour
{
    public GameObject imgPaper;
    GameObject crosshair;
    Volume blurVolume;
    [HideInInspector] public bool showPaper = false;
    void Awake()
    {
        blurVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
        crosshair = GameObject.Find("Crosshair");
        blurVolume.weight = 0f;
    }
    public void openPaper()
    {
        showPaper = !showPaper;
        if (showPaper)
        {
            Time.timeScale = 0f;
            blurVolume.weight = 1f;
            crosshair.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            blurVolume.weight = 0f;
            crosshair.SetActive(true);
        }
        imgPaper.SetActive(showPaper);
    }
}
