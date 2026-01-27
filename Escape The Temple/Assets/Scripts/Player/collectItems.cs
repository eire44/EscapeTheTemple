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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Items"))
        {
            interactionText.gameObject.SetActive(true);
        }

        if(other.gameObject.CompareTag("Candles"))
        {

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
