using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class tilesPuzzleController : MonoBehaviour
{
    public Transform[] bushes;
    int[] correctOrder = { 2, 5, 4, 7, 8, 9 };
    int[] currentOrder = { 0, 0, 0, 0, 0, 0 };
    int tileIndex = 0;
    [HideInInspector] public bool tilesPuzzleSolved = false;
    

    public void addPattern(int tileNumber)
    {
        currentOrder[tileIndex] = tileNumber;

        if (tileIndex < 5)
        {
            tileIndex++;
        }
        else
        {
            checkOrder();
        }
    }

    void PuzzleSolved()
    {
        tilesPuzzleSolved = true;
        foreach (Transform bush in bushes)
        {
            foreach (Transform child in bush)
            {
                fadeRoomDoor fader = child.GetComponent<fadeRoomDoor>();
                if (fader != null)
                {
                    fader.StartFade();
                }
            }
            Collider col = bush.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }
        }
        FindObjectOfType<LTP_bellPattern>().startBellSoundsPattern();
        FindObjectOfType<LTP_BellSwing>().StartRinging();
        Debug.Log("LOGRADO");
    }

    void checkOrder ()
    {
        for (int i = 0; i < correctOrder.Length; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                turnTilesOff();
                tileIndex = 0;
                return;
            }
        }

        PuzzleSolved();
    }

    void turnTilesOff()
    {
        foreach (var tile in FindObjectsOfType<tilesController>())
        {
            Debug.Log("OFF");
            tile.pattern.gameObject.SetActive(false);
        }
    }
}
