using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPieceHolder_Controller : MonoBehaviour
{
    [HideInInspector] public bool keyPlaced = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("KeyPiece"))
        {
            keyPlaced = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
            other.gameObject.layer = LayerMask.NameToLayer("Default");
            FindObjectOfType<unlockPuzzle5>().checkPlaces();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("KeyPiece"))
        {
            keyPlaced = false;
            gameObject.layer = LayerMask.NameToLayer("Life Cycle Puzzle Containers");
            other.gameObject.layer = LayerMask.NameToLayer("Life Cycle Puzzle Pieces");
        }
    }
}
