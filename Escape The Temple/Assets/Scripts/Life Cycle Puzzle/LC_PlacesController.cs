using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LC_PlacesController : MonoBehaviour
{
    public int placeIndex;
    public bool piecePlaced = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Life Cycle Puzzle Pieces"))
        {
            piecePlaced = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Life Cycle Puzzle Pieces"))
        {
            piecePlaced = false;
        }
    }
}
