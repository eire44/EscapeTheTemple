using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class collectItems : MonoBehaviour
{
    public Image interactionText;
    public Image collectItemText;
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
                if (!FindObjectOfType<candlesPuzzleSolution>().candlesPuzzleSolved)
                {
                    interactionText.gameObject.SetActive(true);
                }
            } 
            else if (other.gameObject.CompareTag("Incense Burner"))
            {
                if (!FindObjectOfType<incensePuzzleSolution>().incensePuzzleSolved)
                {
                    interactionText.gameObject.SetActive(true);
                }
            }
            else
            {
                interactionText.gameObject.SetActive(true);
            }
        } else if(other.gameObject.layer == LayerMask.NameToLayer("CollectableItems"))
        {
            collectItemText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Items"))
        {
            interactionText.gameObject.SetActive(false);
        } else if(other.gameObject.layer == LayerMask.NameToLayer("CollectableItems"))
        {
            collectItemText.gameObject.SetActive(false);
        }
    }
}
