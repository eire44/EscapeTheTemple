using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockPuzzle5 : MonoBehaviour
{
    public GameObject[] keyPieces;
    public GameObject[] placesForKeyPieces;
    

    public bool checkPlaces()
    {
        foreach (var item in placesForKeyPieces)
        {
            if(!item.GetComponent<keyPieceHolder_Controller>().keyPlaced)
            {
                return false;
            }
        }

        foreach (var item in keyPieces)
        {
            item.gameObject.layer = LayerMask.NameToLayer("Default");
            
        }
        return true;
    }
}
