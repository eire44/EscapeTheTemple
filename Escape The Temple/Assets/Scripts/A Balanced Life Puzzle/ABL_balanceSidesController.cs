using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABL_balanceSidesController : MonoBehaviour
{
    public int sideIndex;
    float currentWeight = 0f;
    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("A Balanced Life Puzzle Pieces"))
        {
            collision.transform.SetParent(transform);
            currentWeight += collision.gameObject.GetComponent<placeableObjectController>().weightValue;
            FindObjectOfType<ABL_PuzzleController>().saveWeightPlaced(sideIndex, currentWeight);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("A Balanced Life Puzzle Pieces"))
        {
            collision.transform.SetParent(null);
            currentWeight -= collision.gameObject.GetComponent<placeableObjectController>().weightValue;
            FindObjectOfType<ABL_PuzzleController>().saveWeightPlaced(sideIndex, currentWeight);
        }
    }
}
