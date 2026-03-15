using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPieceHolder_Controller : MonoBehaviour
{
    [HideInInspector] public bool keyPlaced = false;
    
    public void placeKeyPiece(GameObject keypiece)
    {
        keyPlaced = true;
        gameObject.layer = LayerMask.NameToLayer("Default");
        keypiece.gameObject.layer = LayerMask.NameToLayer("Default");
        FindObjectOfType<unlockPuzzle5>().checkPlaces();
    }

    //public void removeKeyPiece(GameObject keypiece)
    //{
    //    keyPlaced = false;
    //    gameObject.layer = LayerMask.NameToLayer("Life Cycle Puzzle Containers");
    //    keypiece.gameObject.layer = LayerMask.NameToLayer("Life Cycle Puzzle Pieces");
    //}
}
