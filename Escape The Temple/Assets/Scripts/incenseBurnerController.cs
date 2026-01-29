using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incenseBurnerController : MonoBehaviour
{
    bool pinePicked = false;
    bool cinnamonPicked = false;
    bool lotusPicked = false;
    bool sagebrushPicked = false;
    bool sandalwoodPicked = false;

    bool playerIn = false;
    bool herbPicked = false;

    public string herbNeeded;
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
                if(herbPicked)
                {
                    Transform humoNormal = transform.Find("GraySmoke");
                    humoNormal.localScale = Vector3.one;
                    if (herbNeeded == "Pine")
                    {
                        if (pinePicked)
                        {

                        }
                        else
                        {
                            humoNormal.gameObject.SetActive(true);
                        }
                    }
                    else if (herbNeeded == "Cinnamon")
                    {
                        if (cinnamonPicked)
                        {

                        }
                        else
                        {
                            humoNormal.gameObject.SetActive(true);
                        }
                    }
                    else if (herbNeeded == "Dried Lotus")
                    {
                        if (lotusPicked)
                        {

                        }
                        else
                        {
                            humoNormal.gameObject.SetActive(true);
                        }
                    }
                    else if (herbNeeded == "Sagebrush")
                    {
                        if (sagebrushPicked)
                        {

                        }
                        else
                        {
                            humoNormal.gameObject.SetActive(true);
                        }
                    }
                    else if (herbNeeded == "Sandalwood")
                    {
                        if (sandalwoodPicked)
                        {

                        }
                        else
                        {
                            humoNormal.gameObject.SetActive(true);
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

    public void grabbedItem(bool picked, GameObject item)
    {
        pinePicked = false;
        cinnamonPicked = false;
        lotusPicked = false;
        sagebrushPicked = false;
        sandalwoodPicked = false;

        if (picked)
        {
            if (item.CompareTag("Pine"))
            {
                pinePicked = true;
                herbPicked = true;
            }
            else if (item.CompareTag("Cinnamon"))
            {
                cinnamonPicked = true;
                herbPicked = true;
            }
            else if (item.CompareTag("DriedLotus"))
            {
                lotusPicked = true;
                herbPicked = true;
            }
            else if (item.CompareTag("Sagebrush"))
            {
                sagebrushPicked = true;
                herbPicked = true;
            }
            else if (item.CompareTag("Sandalwood"))
            {
                sandalwoodPicked = true;
                herbPicked = true;
            } else
            {
                herbPicked = false;
            }
        }
        else
        {
            herbPicked = false;
        }
    }
}
