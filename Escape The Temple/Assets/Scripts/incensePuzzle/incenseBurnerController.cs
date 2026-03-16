using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incenseBurnerController : MonoBehaviour
{
    public string herbNeeded;
    public GameObject coloredIncense;
    [HideInInspector] public bool rightHerbPlaced = false;
    incensePuzzleSolution IPcontroller;
    public bool herbPlaced = false;
    AudioSource audiosource;

    List<incenseBurnerController> burners = new List<incenseBurnerController>();
    int burnersIndex = 0;
    private void Start()
    {
        IPcontroller = FindObjectOfType<incensePuzzleSolution>();
        audiosource = GetComponent<AudioSource>();

        foreach (incenseBurnerController incenseBurner in FindObjectsOfType<incenseBurnerController>())
        {
            burners.Add(incenseBurner);
        }
    }

    public void placeHerb(IP_HerbController currentObject)
    {
        if (!IPcontroller.incensePuzzleSolved)
        {
            audiosource.Play();
            herbPlaced = true;
            Transform humoNormal = transform.Find("GraySmoke");
            if (herbNeeded == currentObject.herb_id)
            {
                coloredIncense.SetActive(true);
                humoNormal.gameObject.SetActive(false);
                rightHerbPlaced = true;
                IPcontroller.checkForPuzzleSolution();
            }
            else
            {
                foreach (incenseBurnerController incenseBurner in burners)
                {
                    if(incenseBurner.herbPlaced)
                    {
                        Transform humoGris = incenseBurner.transform.Find("GraySmoke");
                        humoGris.gameObject.SetActive(true);
                        incenseBurner.coloredIncense.SetActive(false);
                        humoGris.localScale = Vector3.one;
                        incenseBurner.rightHerbPlaced = false;
                    }
                }
            }
        }
    }
}
