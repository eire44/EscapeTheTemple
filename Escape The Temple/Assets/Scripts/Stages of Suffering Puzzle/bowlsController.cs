using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlsController : MonoBehaviour
{
    public int bowlIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<stagesController>().callForCheckConfiguration(bowlIndex, other.gameObject.tag);
    }
}
