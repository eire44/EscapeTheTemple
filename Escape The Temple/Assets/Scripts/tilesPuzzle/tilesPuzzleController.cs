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
    public int puzzleIndex = 6;

    public interiorLanternsController[] interiorLantern;
    public interiorLanternsController[] interiorLanternBell;
    public exteriorLanternsController[] lanternsRoomBell;

    public LTP_bellPattern lTP_BellPattern;
    public LTP_BellSwing lTP_BellSwing;
    public Collider[] bushColliders;

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

    IEnumerator SolvePuzzleRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        GameManager.instance.turnOn_ExteriorLanterns(lanternsRoomBell, false);
        yield return new WaitForSeconds(0.5f);

        GameManager.instance.turnOn_InteriorLanterns(interiorLantern);
        yield return new WaitForSeconds(0.5f);

        GameManager.instance.callForSunMovement(puzzleIndex);
    }

    IEnumerator ringBell()
    {
        yield return new WaitForSeconds(1f);
        lTP_BellPattern.startBellSoundsPattern();

        yield return new WaitForSeconds(2f);

        lTP_BellSwing.StartRinging();
    }

    IEnumerator openBushes()
    {
        tilesPuzzleSolved = true;


        foreach (Collider bushCol in bushColliders)
        {
            if (bushCol != null)
                bushCol.enabled = false;

            yield return null;
        }

        foreach (Transform bush in bushes)
        {
            bush.GetComponent<TP_bushesAudio>()?.bushSound();

            foreach (Transform child in bush)
            {
                fadeRoomDoor fader = child.GetComponent<fadeRoomDoor>();
                if (fader != null)
                    fader.StartFade();
            }

            //Collider col = bush.GetComponent<Collider>();
            //if (col != null)
            //    col.enabled = false;

            yield return null;
        }
    }

    void PuzzleSolved()
    {
        if(!tilesPuzzleSolved)
        {
            tilesPuzzleSolved = true;
            foreach (Transform bush in bushes)
            {
                bush.GetComponent<TP_bushesAudio>().bushSound();
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

            lTP_BellPattern.startBellSoundsPattern();
            lTP_BellSwing.StartRinging();
            GameManager.instance.turnOn_ExteriorLanterns(lanternsRoomBell, false);
            GameManager.instance.turnOn_InteriorLanterns(interiorLantern);
            GameManager.instance.callForSunMovement(puzzleIndex);
        }
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

        //PuzzleSolved();
        //StartCoroutine(SolvePuzzleRoutine()); 

        //StartCoroutine(openBushes());
        StartCoroutine(SolveSequence());
    }
    IEnumerator SolveSequence()
    {
        yield return StartCoroutine(openBushes());

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(SolvePuzzleRoutine());

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(ringBell());
    }

    void turnTilesOff()
    {
        foreach (var tile in FindObjectsOfType<tilesController>())
        {
            tile.pattern.gameObject.SetActive(false);
        }
    }
}
