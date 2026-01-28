using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class collectItems : MonoBehaviour
{
    public Image interactionText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Items"))
        {
            if (other.gameObject.CompareTag("Candles"))
            {
                if (FindObjectOfType<candlesPuzzleSolution>().candlesPuzzleSolved == false)
                {
                    interactionText.gameObject.SetActive(true);
                }
            } else
            {
                interactionText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Items"))
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}
