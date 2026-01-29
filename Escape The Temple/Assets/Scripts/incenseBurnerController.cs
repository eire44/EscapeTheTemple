using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incenseBurnerController : MonoBehaviour
{
    bool playerIn = false;

    public string herbNeeded;
    public GameObject coloredIncense;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIn)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (FindObjectOfType<incensePuzzleSolution>().herbPicked)
                {
                    Transform humoNormal = transform.Find("GraySmoke");
                    humoNormal.localScale = Vector3.one;
                    if (herbNeeded == "Pine")
                    {
                        if (FindObjectOfType<incensePuzzleSolution>().pinePicked)
                        {
                            coloredIncense.SetActive(true);
                            humoNormal.gameObject.SetActive(false);
                            FindObjectOfType<incensePuzzleSolution>().pineIncenseLit = true;
                        }
                        else
                        {
                            Debug.Log("MAL");
                            humoNormal.gameObject.SetActive(true);
                            coloredIncense.SetActive(false);
                            FindObjectOfType<incensePuzzleSolution>().pineIncenseLit = false;
                        }
                    }
                    else if (herbNeeded == "Cinnamon")
                    {
                        if (FindObjectOfType<incensePuzzleSolution>().cinnamonPicked)
                        {
                            coloredIncense.SetActive(true);
                            humoNormal.gameObject.SetActive(false);
                            FindObjectOfType<incensePuzzleSolution>().cinnamonIncenseLit = true;
                        }
                        else
                        {

                            coloredIncense.SetActive(false);
                            humoNormal.gameObject.SetActive(true);
                            FindObjectOfType<incensePuzzleSolution>().cinnamonIncenseLit = false;
                        }
                    }
                    else if (herbNeeded == "Dried Lotus")
                    {
                        if (FindObjectOfType<incensePuzzleSolution>().lotusPicked)
                        {
                            coloredIncense.SetActive(true);
                            humoNormal.gameObject.SetActive(false);
                            FindObjectOfType<incensePuzzleSolution>().lotusIncenseLit = true;
                        }
                        else
                        {
                            coloredIncense.SetActive(false);
                            humoNormal.gameObject.SetActive(true);
                            FindObjectOfType<incensePuzzleSolution>().lotusIncenseLit = false;
                        }
                    }
                    else if (herbNeeded == "Sagebrush")
                    {
                        if (FindObjectOfType<incensePuzzleSolution>().sagebrushPicked)
                        {
                            coloredIncense.SetActive(true);
                            humoNormal.gameObject.SetActive(false);
                            FindObjectOfType<incensePuzzleSolution>().sagebrushIncenseLit = true;
                        }
                        else
                        {
                            coloredIncense.SetActive(false);
                            humoNormal.gameObject.SetActive(true);
                            FindObjectOfType<incensePuzzleSolution>().sagebrushIncenseLit = false;
                        }
                    }
                    else if (herbNeeded == "Sandalwood")
                    {
                        if (FindObjectOfType<incensePuzzleSolution>().sandalwoodPicked)
                        {
                            coloredIncense.SetActive(true);
                            humoNormal.gameObject.SetActive(false);
                            FindObjectOfType<incensePuzzleSolution>().sandalwoodIncenseLit = true;
                        }
                        else
                        {
                            coloredIncense.SetActive(false);
                            humoNormal.gameObject.SetActive(true);
                            FindObjectOfType<incensePuzzleSolution>().sandalwoodIncenseLit = false;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIn = false;
        }
    }

    
}
