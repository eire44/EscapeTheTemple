using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LC_PuzzleController : MonoBehaviour
{
    public GameObject[] places;
    int[] correctOrder = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    [HideInInspector] public int[] currentOrder = new int[9];

    public GameObject fadeOutPiece;
    public GameObject fadeInKeyPiece;
    [HideInInspector] public bool puzzleAlreadySolved = false;
    public int puzzleIndex = 1;
    public interiorLanternsController[] interiorLantern;

    private void Start()
    {
        for (int i = 0; i < currentOrder.Length; i++)
        {
            currentOrder[i] = -1;
        }
    }
    public void checkOrder()
    {
        for (int i = 0; i < currentOrder.Length; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                return;
            }
        }

        if(!puzzleAlreadySolved)
        {
            foreach (var item in FindObjectsOfType<LC_PiecesController>())
            {
                item.gameObject.layer = LayerMask.NameToLayer("Default");
                item.GetComponent<LC_PiecesController>().enabled = false;
                item.GetComponent<Rigidbody>().isKinematic = true;
                item.GetComponent<Rigidbody>().useGravity = false;
            }

            //fadeOutPiece.GetComponent<BoxCollider>().isTrigger = true;
            //fadeOutPiece.GetComponent<fadeRoomDoor>().StartFade();
            puzzleAlreadySolved = true;
            fadeOutPiece.SetActive(false);
            fadeInKeyPiece.SetActive(true);
            fadeInKeyPiece.GetComponent<fadeIn_PuzzlePieces>().StartFade();
            GameManager.instance.checkIfRoom2Completed();
            GameManager.instance.turnOn_InteriorLanterns(interiorLantern);
            GameManager.instance.callForSunMovement(puzzleIndex);
        }
    }
}
