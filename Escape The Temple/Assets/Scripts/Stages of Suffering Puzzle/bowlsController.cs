using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlsController : MonoBehaviour
{
    public int bowlIndex;
    stagesController stages;

    private void Start()
    {
        stages = FindObjectOfType<stagesController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        stages.callForCheckConfiguration(bowlIndex, collision.gameObject.tag);
    }

    private void OnCollisionExit(Collision collision)
    {
        //stages.callForCheckConfiguration(bowlIndex, "");
    }
}
