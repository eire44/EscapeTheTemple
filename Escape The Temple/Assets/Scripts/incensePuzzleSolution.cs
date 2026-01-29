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
}
