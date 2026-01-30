using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilesController : MonoBehaviour
{
    [HideInInspector] public Transform pattern;
    public int tileNumber;
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
        if (!FindObjectOfType<tilesPuzzleController>().tilesPuzzleSolved)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!pattern.gameObject.activeInHierarchy)
                {
                    pattern.gameObject.SetActive(true);
                    FindObjectOfType<tilesPuzzleController>().addPattern(tileNumber);
                    Debug.Log("ON");
                }
            }
        }
    }
}
