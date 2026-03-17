using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incensePuzzleSolution : MonoBehaviour
{
    [HideInInspector] public bool incensePuzzleSolved = false;

    public interiorLanternsController[] interiorLantern;
    public interiorLanternsController[] interiorLanternTiles;
    public exteriorLanternsController[] lanternsRoomTiles;

    bool checkIncenseConfiguration()
    {
        foreach (incenseBurnerController incenseBurner in FindObjectsOfType<incenseBurnerController>())
        {
            if(!incenseBurner.rightHerbPlaced)
            {
                return false;
            }
        }

        return true;
    }

    public void checkForPuzzleSolution()
    {
        if (checkIncenseConfiguration())
        {
            incensePuzzleSolved = true;
            FindObjectOfType<puzzleProgressionController>().turnOn_ExteriorLanterns(lanternsRoomTiles, false);
            FindObjectOfType<puzzleProgressionController>().turnOn_InteriorLanterns(interiorLantern);
            FindObjectOfType<puzzleProgressionController>().flicker_InteriorLanterns(interiorLanternTiles);
            FindObjectOfType<SunMovement>().sunProgression();
            FindObjectOfType<grabItem_wRaycast>().dropOnTheGround();
            foreach (IP_HerbController herb in FindObjectsOfType<IP_HerbController>())
            {
                herb.gameObject.layer = LayerMask.NameToLayer("Default");
            }

            foreach (incenseBurnerController incenseBurner in FindObjectsOfType<incenseBurnerController>())
            {
                incenseBurner.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
    }
}
