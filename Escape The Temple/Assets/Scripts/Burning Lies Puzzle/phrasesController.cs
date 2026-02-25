using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phrasesController : MonoBehaviour
{
    public int index;
    bool alreadyBurned = false;
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
            Debug.Log("colisionó con " + other.gameObject.name);
            controller.checkBurntPaper(index);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!alreadyBurned) return;

        if (other.CompareTag("Fire"))
        {
            alreadyBurned = false;
        }
    }
}
