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

    AudioSource audioSource;
    public AudioClip[] woodHit_clips;

    bool onPlace = true;

    private void Start()
    {
        puzzleController = FindObjectOfType<LC_PuzzleController>();
        candlesPuzzleController = FindObjectOfType<candlesPuzzleSolution>();
        audioSource = GetComponent<AudioSource>();
    }

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

        FindObjectOfType<LC_PuzzleController>().checkOrder();
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
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Life Cycle Puzzle Pieces") || collision.gameObject == woodPlank) && onPlace)
        {
            if(candlesPuzzleController.candlesPuzzleSolved)
            {
                audioSource.PlayOneShot(woodHit_clips[Random.Range(0, woodHit_clips.Length)]);
                Debug.Log(audioSource.isPlaying + " ESTE ");
            }
        }
    }
}
