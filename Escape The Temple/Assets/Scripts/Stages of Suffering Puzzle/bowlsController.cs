using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlsController : MonoBehaviour
{
    public int bowlIndex;
    
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<stagesController>().callForCheckConfiguration(bowlIndex, other.gameObject.tag);
    }
}
