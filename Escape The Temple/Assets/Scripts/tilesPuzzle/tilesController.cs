using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilesController : MonoBehaviour
{
    [HideInInspector] public Transform pattern;
    // Start is called before the first frame update
    void Start()
    {
        pattern = transform.Find("Pattern");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!pattern.gameObject.activeInHierarchy)
            {
                FindObjectOfType<tilesPuzzleController>().addPattern();
                pattern.gameObject.SetActive(true);
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        pattern.gameObject.SetActive(false);
    //    }
    //}
}
