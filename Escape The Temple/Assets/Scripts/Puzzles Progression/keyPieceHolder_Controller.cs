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
            if(FindObjectOfType<unlockPuzzle5>().checkPlaces())
            {
                Debug.Log("SPAWNEAR PUZZLE 5");
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("KeyPiece"))
        {
            keyPlaced = false;
        }
    }
}
