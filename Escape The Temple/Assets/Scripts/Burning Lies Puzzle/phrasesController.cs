using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phrasesController : MonoBehaviour
{
    public int index;
    [HideInInspector] public bool alreadyBurned = false;
    burningLiesController controller;

    void Start()
    {
        controller = FindObjectOfType<burningLiesController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (alreadyBurned) return;

        if (other.CompareTag("Fire"))
        {
            alreadyBurned = true;
            controller.checkBurntPaper(index, this, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!alreadyBurned) return;

        if (other.CompareTag("Fire"))
        {
            alreadyBurned = false;
            controller.checkBurntPaper(index, this, false);
        }
    }
}
