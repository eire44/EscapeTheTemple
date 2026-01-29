using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilesPuzzleController : MonoBehaviour
{
    int patternsLit = 6;
    int currentPatternAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPatternAmount >= patternsLit)
        {
            foreach (var tile in FindObjectsOfType<tilesController>())
            {
                tile.pattern.gameObject.SetActive(false);
            }
        }
    }

    public void addPattern()
    {
        currentPatternAmount++;
    }
}
