using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class encenderVelas : MonoBehaviour
{
    public ParticleSystem llama;
    public string nombreTabla;
    
    public void encenderVela()
    {
        if (!FindObjectOfType<candlesPuzzleSolution>().candlesPuzzleSolved)
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
