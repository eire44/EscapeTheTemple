using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LC_PiecesController : MonoBehaviour
{
    public int pieceIndex;
    LC_PuzzleController puzzleController;

    private void Start()
    {
        puzzleController = FindObjectOfType<LC_PuzzleController>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.layer == LayerMask.NameToLayer("Life Cycle Puzzle Containers"))
    //    {
    //        for (int i = 0; i < puzzleController.places.Length; i++)
    //        {
    //            if (other.gameObject == puzzleController.places[i])
    //            {
    //                //Debug.Log(i);
    //                puzzleController.currentOrder[i] = pieceIndex;
    //            }
    //        }
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Life Cycle Puzzle Containers"))
        {
            LC_PlacesController place = other.GetComponent<LC_PlacesController>();

            if (place != null)
            {
                puzzleController.currentOrder[place.placeIndex] = pieceIndex;
            }
        }

        Debug.Log(string.Join(", ", FindObjectOfType<LC_PuzzleController>().currentOrder));
        if (FindObjectOfType<LC_PuzzleController>().checkOrder())
        {
            Debug.Log("WIIIIIIIIIIIIIIIIIIIII");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Life Cycle Puzzle Containers"))
        {
            LC_PlacesController place = other.GetComponent<LC_PlacesController>();

            if (place != null)
            {
                puzzleController.currentOrder[place.placeIndex] = pieceIndex;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(gameObject.name + " - " + other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Life Cycle Puzzle Containers"))
        {
            LC_PlacesController place = other.GetComponent<LC_PlacesController>();

            if (place != null)
            {
                puzzleController.currentOrder[place.placeIndex] = -1;
            }
        }
    }
}
