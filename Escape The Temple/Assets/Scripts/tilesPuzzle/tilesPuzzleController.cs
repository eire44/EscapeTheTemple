using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class tilesPuzzleController : MonoBehaviour
{
    public GameObject secretAreaWall;
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
        secretAreaWall.GetComponent<fadeRoomDoor>().StartFade();
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
