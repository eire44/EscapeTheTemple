using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LC_PiecesController : MonoBehaviour
{
    public int pieceIndex;
    LC_PuzzleController puzzleController;
    candlesPuzzleSolution candlesPuzzleController;
    public GameObject woodBottomPlank;
    public GameObject woodPlank;

    bool onPlace = true;

    private void Start()
    {
        puzzleController = FindObjectOfType<LC_PuzzleController>();
        candlesPuzzleController = FindObjectOfType<candlesPuzzleSolution>();
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

        //Debug.Log(string.Join(", ", FindObjectOfType<LC_PuzzleController>().currentOrder));
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

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject == woodPlank)
        //{
        //    onPlace = true;
        //} 
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Life Cycle Puzzle Pieces") || collision.gameObject == woodBottomPlank) && onPlace)
        {
            if(candlesPuzzleController.candlesPuzzleSolved)
            {
                puzzleController.audioSource.PlayOneShot(puzzleController.woodHit_clips[Random.Range(0, puzzleController.woodHit_clips.Length)]);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //if(collision.gameObject == woodPlank)
        //{
        //    onPlace = false;
        //}
    }
}
