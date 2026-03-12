using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABL_balanceSidesController : MonoBehaviour
{
    public int sideIndex;
    float currentWeight = 0f;

    public GameObject[] objects;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ABLP_Pieces"))
        {
            placeableObjectController pOC = other.gameObject.GetComponent<placeableObjectController>();
            placeObject(pOC, true);
            pOC.placed = true;
            currentWeight += pOC.weightValue;
            FindObjectOfType<ABL_PuzzleController>().saveWeightPlaced(sideIndex, currentWeight);
            FindObjectOfType<ABL_PuzzleController>().saveObject(pOC);
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
        }
    } 

    public void placeObject(placeableObjectController objectToPlace, bool active)
    {
        if(objectToPlace.index == 0)
        {
            objects[0].SetActive(active);
        } 
        else if (objectToPlace.index == 1)
        {
            objects[1].SetActive(active);
        } 
        else if(objectToPlace.index == 2)
        {
            objects[2].SetActive(active);
        } 
        else
        {
            objects[3].SetActive(active);
        }
    }

    public void removeObjectFromBalance(GameObject objectToRemove)
    {
        placeableObjectController pOC = objectToRemove.GetComponent<placeableObjectController>();
        placeObject(pOC, false);
        pOC.placed = false;
        currentWeight -= pOC.weightValue;
        FindObjectOfType<ABL_PuzzleController>().saveWeightPlaced(sideIndex, currentWeight);
    }
}
