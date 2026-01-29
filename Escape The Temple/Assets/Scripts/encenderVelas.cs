using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class encenderVelas : MonoBehaviour
{
    bool jugadorCerca = false;
    public ParticleSystem llama;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindObjectOfType<candlesPuzzleSolution>().candlesPuzzleSolved)
        {
            if (Input.GetKeyDown(KeyCode.E) && jugadorCerca)
            {
                if (llama.gameObject.activeInHierarchy)
                {
                    llama.gameObject.SetActive(false);
                }
                else
                {
                    llama.gameObject.SetActive(true);
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}
