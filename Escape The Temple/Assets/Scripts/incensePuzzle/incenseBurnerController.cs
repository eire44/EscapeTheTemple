using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incenseBurnerController : MonoBehaviour
{
    public string herbNeeded;
    public GameObject coloredIncense;
    [HideInInspector] public bool rightHerbPlaced = false;
    incensePuzzleSolution IPcontroller;

    private void Start()
    {
        IPcontroller = FindObjectOfType<incensePuzzleSolution>();
    }

    public void placeHerb(IP_HerbController currentObject)
    {
        if (!IPcontroller.incensePuzzleSolved)
        {
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
                humoNormal.gameObject.SetActive(true);
                humoNormal.localScale = Vector3.one;
                rightHerbPlaced = false;
            }
        }
    }
}
