using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABL_balanceSidesController : MonoBehaviour
{
    public int sideIndex;
    float currentWeight = 0f;

    List<Transform> slots = new List<Transform>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.CompareTag("PlaceHolder"))
            {
                slots.Add(child);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("ABLP_Pieces"))
        {
            collision.transform.SetParent(transform);
            //collision.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            //collision.transform.localScale = collision.gameObject.GetComponent<placeableObjectController>().objectSize;
            collision.gameObject.GetComponent<placeableObjectController>().placed = true;
            currentWeight += collision.gameObject.GetComponent<placeableObjectController>().weightValue;
            FindObjectOfType<ABL_PuzzleController>().saveWeightPlaced(sideIndex, currentWeight);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ABLP_Pieces"))
        {
            //collision.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            //collision.transform.SetParent(null);
            collision.transform.localScale = collision.gameObject.GetComponent<placeableObjectController>().objectSize;
            collision.gameObject.GetComponent<placeableObjectController>().placed = false;
            currentWeight -= collision.gameObject.GetComponent<placeableObjectController>().weightValue;
            FindObjectOfType<ABL_PuzzleController>().saveWeightPlaced(sideIndex, currentWeight);
        }
    }
}
