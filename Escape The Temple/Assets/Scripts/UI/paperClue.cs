using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paperClue : MonoBehaviour
{
    public GameObject imgPaper;
    [HideInInspector] public bool showPaper = false;
    
    public void openPaper()
    {
        showPaper = !showPaper;
        if (showPaper)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        imgPaper.SetActive(showPaper);
    }
}
