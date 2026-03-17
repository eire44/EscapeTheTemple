using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABL_balanceSidesController : MonoBehaviour
{
    public int sideIndex;
    float currentWeight = 0f;

    public GameObject[] objects;

    public AudioClip[] audioClips;
    AudioSource audiosource;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("ABLP_Pieces"))
    //    {

    //        //Destroy(other.gameObject);
    //    }
    //} 

    public void sumWeight(GameObject objectPlaced)
    {
        audiosource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        placeableObjectController pOC = objectPlaced.GetComponent<placeableObjectController>();
        Debug.Log(objectPlaced.name + " se agrega. indice " + pOC.index);
        placeObject(pOC, true);
        objects[pOC.index].GetComponent<ABL_GrabObjectFromBalance>().grabbedObject = objectPlaced;

        pOC.placed = true;
        currentWeight += pOC.weightValue;
        FindObjectOfType<ABL_PuzzleController>().saveWeightPlaced(sideIndex, currentWeight);
        FindObjectOfType<ABL_PuzzleController>().saveObject(pOC);
        objectPlaced.SetActive(false);
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

        if (pOC == null)
        {
            Debug.LogError("El objeto visual no tiene placeableObjectController: " + objectToRemove.name);
            return;
        }
        Debug.Log(objectToRemove.name + " se esta eliminando. indice " + pOC.index);
        placeObject(pOC, false);
        FindObjectOfType<ABL_PuzzleController>().clearObject(pOC.index);
        pOC.placed = false;
        currentWeight -= pOC.weightValue;
        FindObjectOfType<ABL_PuzzleController>().saveWeightPlaced(sideIndex, currentWeight);
    }
}
