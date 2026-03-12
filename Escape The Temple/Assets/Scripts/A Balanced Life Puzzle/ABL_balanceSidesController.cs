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
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ABLP_Pieces"))
        {
            placeableObjectController pOC = other.gameObject.GetComponent<placeableObjectController>();
            placeObject(pOC, false);
            pOC.placed = false;
            currentWeight -= pOC.weightValue;
            FindObjectOfType<ABL_PuzzleController>().saveWeightPlaced(sideIndex, currentWeight);
            //other.gameObject.SetActive(false); INSTANCIAR EN MANO
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
}
