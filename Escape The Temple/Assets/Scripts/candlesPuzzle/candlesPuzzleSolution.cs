using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candlesPuzzleSolution : MonoBehaviour
{
    public List<GameObject> llamas = new List<GameObject>();
    [HideInInspector] public bool candlesPuzzleSolved = false;
    bool flagHideText = true;
    public GameObject room2_Door;
    
    void Update()
    {
        if (llamas[0].activeInHierarchy && !llamas[1].activeInHierarchy && llamas[2].activeInHierarchy && llamas[3].activeInHierarchy
            && !llamas[4].activeInHierarchy && !llamas[5].activeInHierarchy && llamas[6].activeInHierarchy && !llamas[7].activeInHierarchy
             && !llamas[8].activeInHierarchy && llamas[9].activeInHierarchy)
        {
            candlesPuzzleSolved = true;
            if (flagHideText)
            {
                foreach (var vela in FindObjectsOfType<encenderVelas>())
                {
                    vela.gameObject.layer = LayerMask.NameToLayer("Default");
                }
                unlockRoom2();
                flagHideText = false;
                FindObjectOfType<SunMovement>().sunProgression();
            }
            
        }
    }

    void unlockRoom2()
    {
        room2_Door.GetComponent<fadeRoomDoor>().StartFade();
    }
}
