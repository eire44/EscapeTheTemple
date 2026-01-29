using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incensePuzzleSolution : MonoBehaviour
{
    [HideInInspector] public bool pineIncenseLit = false;
    [HideInInspector] public bool cinnamonIncenseLit = false;
    [HideInInspector] public bool lotusIncenseLit = false;
    [HideInInspector] public bool sagebrushIncenseLit = false;
    [HideInInspector] public bool sandalwoodIncenseLit = false;

    [HideInInspector] public bool incensePuzzleSolved = false;


    [HideInInspector] public bool pinePicked = false;
    [HideInInspector] public bool cinnamonPicked = false;
    [HideInInspector] public bool lotusPicked = false;
    [HideInInspector] public bool sagebrushPicked = false;
    [HideInInspector] public bool sandalwoodPicked = false;
    [HideInInspector] public bool herbPicked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pineIncenseLit && cinnamonIncenseLit && lotusIncenseLit && sagebrushIncenseLit && sandalwoodIncenseLit)
        {
            incensePuzzleSolved = true;
        } else
        {
            incensePuzzleSolved = false;
        }
    }

    public void grabbedItem(bool picked, GameObject item)
    {
        pinePicked = false;
        cinnamonPicked = false;
        lotusPicked = false;
        sagebrushPicked = false;
        sandalwoodPicked = false;
        Debug.Log(picked);
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
            }
            else
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
