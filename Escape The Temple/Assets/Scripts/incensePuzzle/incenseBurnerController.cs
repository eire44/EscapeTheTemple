using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incenseBurnerController : MonoBehaviour
{
    bool playerIn = false;

    public string herbNeeded;
    public GameObject coloredIncense;

    incensePuzzleSolution IPcontroller;

    private void Start()
    {
        IPcontroller = FindObjectOfType<incensePuzzleSolution>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!FindObjectOfType<incensePuzzleSolution>().incensePuzzleSolved)
        //{
        //    if (playerIn)
        //    {
        //        if (Input.GetKeyDown(KeyCode.E))
        //        {
        //            if (FindObjectOfType<incensePuzzleSolution>().herbPicked)
        //            {
        //                Transform humoNormal = transform.Find("GraySmoke");
        //                humoNormal.localScale = Vector3.one;
        //                if (herbNeeded == "Pine")
        //                {
        //                    if (FindObjectOfType<incensePuzzleSolution>().pinePicked)
        //                    {
        //                        coloredIncense.SetActive(true);
        //                        humoNormal.gameObject.SetActive(false);
        //                        FindObjectOfType<incensePuzzleSolution>().pineIncenseLit = true;
        //                    }
        //                    else
        //                    {
        //                        humoNormal.gameObject.SetActive(true);
        //                        coloredIncense.SetActive(false);
        //                        FindObjectOfType<incensePuzzleSolution>().pineIncenseLit = false;
        //                    }
        //                }
        //                else if (herbNeeded == "Cinnamon")
        //                {
        //                    if (FindObjectOfType<incensePuzzleSolution>().cinnamonPicked)
        //                    {
        //                        coloredIncense.SetActive(true);
        //                        humoNormal.gameObject.SetActive(false);
        //                        FindObjectOfType<incensePuzzleSolution>().cinnamonIncenseLit = true;
        //                    }
        //                    else
        //                    {

        //                        coloredIncense.SetActive(false);
        //                        humoNormal.gameObject.SetActive(true);
        //                        FindObjectOfType<incensePuzzleSolution>().cinnamonIncenseLit = false;
        //                    }
        //                }
        //                else if (herbNeeded == "Dried Lotus")
        //                {
        //                    if (FindObjectOfType<incensePuzzleSolution>().lotusPicked)
        //                    {
        //                        coloredIncense.SetActive(true);
        //                        humoNormal.gameObject.SetActive(false);
        //                        FindObjectOfType<incensePuzzleSolution>().lotusIncenseLit = true;
        //                    }
        //                    else
        //                    {
        //                        coloredIncense.SetActive(false);
        //                        humoNormal.gameObject.SetActive(true);
        //                        FindObjectOfType<incensePuzzleSolution>().lotusIncenseLit = false;
        //                    }
        //                }
        //                else if (herbNeeded == "Sagebrush")
        //                {
        //                    if (FindObjectOfType<incensePuzzleSolution>().sagebrushPicked)
        //                    {
        //                        coloredIncense.SetActive(true);
        //                        humoNormal.gameObject.SetActive(false);
        //                        FindObjectOfType<incensePuzzleSolution>().sagebrushIncenseLit = true;
        //                    }
        //                    else
        //                    {
        //                        coloredIncense.SetActive(false);
        //                        humoNormal.gameObject.SetActive(true);
        //                        FindObjectOfType<incensePuzzleSolution>().sagebrushIncenseLit = false;
        //                    }
        //                }
        //                else if (herbNeeded == "Sandalwood")
        //                {
        //                    if (FindObjectOfType<incensePuzzleSolution>().sandalwoodPicked)
        //                    {
        //                        coloredIncense.SetActive(true);
        //                        humoNormal.gameObject.SetActive(false);
        //                        FindObjectOfType<incensePuzzleSolution>().sandalwoodIncenseLit = true;
        //                    }
        //                    else
        //                    {
        //                        coloredIncense.SetActive(false);
        //                        humoNormal.gameObject.SetActive(true);
        //                        FindObjectOfType<incensePuzzleSolution>().sandalwoodIncenseLit = false;
        //                    }
        //                }
        //            }
        //        }
        //    }
        
        //}
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
                //FindObjectOfType<incensePuzzleSolution>().sandalwoodIncenseLit = true;
            }
            else
            {
                humoNormal.gameObject.SetActive(true);
                humoNormal.localScale = Vector3.one;
            }
        }
    }
}
