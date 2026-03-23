using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class keyPieceHolder_Controller : MonoBehaviour
{
    [HideInInspector] public bool keyPlaced = false;
    
    public void placeKeyPiece(GameObject keypiece)
    {
        keyPlaced = true;
        keypiece.GetComponent<kayPieceSound>().handleKeypiece(false);
        gameObject.layer = LayerMask.NameToLayer("Default");
        keypiece.gameObject.layer = LayerMask.NameToLayer("Default");
        Instantiate(keypiece, transform.position, Quaternion.Euler(-90f, 0f, 180f));
        keypiece.SetActive(false);
        //destruir keypiece set active la otra
        FindObjectOfType<unlockPuzzle5>().checkPlaces();
    }
}
