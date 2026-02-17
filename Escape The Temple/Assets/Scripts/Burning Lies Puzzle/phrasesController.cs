using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phrasesController : MonoBehaviour
{
    public int index;
    burningLiesController controller;

    void Start()
    {
        controller = FindObjectOfType<burningLiesController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            controller.checkBurntPaper(index);
        }
    }
}
