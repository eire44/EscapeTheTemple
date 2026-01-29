using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candlesPuzzleSolution : MonoBehaviour
{
    public List<GameObject> llamas = new List<GameObject>();
    [HideInInspector] public bool candlesPuzzleSolved = false;
    bool flagHideText = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (llamas[0].activeInHierarchy && !llamas[1].activeInHierarchy && llamas[2].activeInHierarchy && llamas[3].activeInHierarchy
            && !llamas[4].activeInHierarchy && !llamas[5].activeInHierarchy && llamas[6].activeInHierarchy && !llamas[7].activeInHierarchy
             && !llamas[8].activeInHierarchy && llamas[9].activeInHierarchy)
        {
            candlesPuzzleSolved = true;
            if (flagHideText)
            {
                FindObjectOfType<collectItems>().interactionText.gameObject.SetActive(false);
                flagHideText = false;
            }
            
        }
    }
}
