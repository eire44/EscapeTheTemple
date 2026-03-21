using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleProgressionController : MonoBehaviour
{
    public musicManager musicController;
    public void turnOn_ExteriorLanterns(exteriorLanternsController[] extLanterns, bool ending)
    {
        bool flag = true;
        foreach (exteriorLanternsController item in extLanterns)
        {
            Debug.Log(item.name);
            item.TurnOn();
            if(flag)
            {
                flag = false;
                musicController.changeMusic(ending);
            }
            
        }
    }
    public void turnOn_InteriorLanterns(interiorLanternsController[] intLanterns)
    {
        foreach (interiorLanternsController item in intLanterns)
        {
            //item.StopFlicker();
            item.TurnOn();
        }
    }
    public void flicker_InteriorLanterns(interiorLanternsController[] intLanterns)
    {
        //foreach (interiorLanternsController item in intLanterns)
        //{
        //    Debug.Log(item.name);
        //    item.StartFlicker();
        //}
    }

    public void checkIfRoom2Completed()
    {
        if(FindObjectOfType<burningLiesController>().puzzleAlreadySolved && FindObjectOfType<LC_PuzzleController>().puzzleAlreadySolved && FindObjectOfType<stagesController>().puzzleAlreadySolved)
        {
            turnOn_ExteriorLanterns(FindObjectOfType<burningLiesController>().lanternsRoomABL, false);
            flicker_InteriorLanterns(FindObjectOfType<burningLiesController>().interiorLanternABL);
        }
    }
    public IEnumerator turnOn_TilesLanterns(exteriorLanternsController[] extLanterns, bool ending)
    {
        bool flag = true;

        for (int i = 0; i < extLanterns.Length; i++)
        {
            extLanterns[i].TurnOn();

            if (flag)
            {
                flag = false;
                musicController.changeMusic(ending);
            }

            // Lógica de espera: 
            // Si el índice es impar (1, 3, 5, 7), significa que ya encendió una pareja
            if (i % 2 != 0)
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

}
