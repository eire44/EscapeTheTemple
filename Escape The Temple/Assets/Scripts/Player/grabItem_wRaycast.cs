using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class grabItem_wRaycast : MonoBehaviour
{
    public Camera playerCamera;
    public float distance = 4f;
    public GameObject posHand;
    public LayerMask grabbableLayer;
    public LayerMask placeLayer;

    bool holdingItem = false;

    GameObject currentGrabbedItem;
    Rigidbody rb;

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Physics.Raycast(ray, out hit, distance, grabbableLayer))
            {
                if (currentGrabbedItem == null)
                {
                    currentGrabbedItem = hit.collider.gameObject;
                    rb = currentGrabbedItem.GetComponent<Rigidbody>();

                    currentGrabbedItem.transform.SetParent(posHand.transform);
                    currentGrabbedItem.transform.position = posHand.transform.position;

                    rb.isKinematic = true;
                    rb.useGravity = false;
                } 
                else
                {
                    currentGrabbedItem.transform.SetParent(null);

                    //currentGrabbedItem.transform.position = hit.transform.position;
                    //currentGrabbedItem.transform.rotation = hit.transform.rotation;

                    rb.isKinematic = false;
                    rb.useGravity = true;

                    currentGrabbedItem = null;
                    rb = null;

                    currentGrabbedItem = hit.collider.gameObject;
                    rb = currentGrabbedItem.GetComponent<Rigidbody>();

                    currentGrabbedItem.transform.SetParent(posHand.transform);
                    currentGrabbedItem.transform.position = posHand.transform.position;

                    rb.isKinematic = true;
                    rb.useGravity = false;
                }
            } else if (Physics.Raycast(ray, out hit, distance, placeLayer))
            {
                if (currentGrabbedItem != null)
                {
                    currentGrabbedItem.transform.SetParent(null);

                    currentGrabbedItem.transform.position = hit.transform.position;
                    currentGrabbedItem.transform.rotation = hit.transform.rotation;

                    rb.isKinematic = false;
                    rb.useGravity = true;

                    currentGrabbedItem = null;
                    rb = null;

                    //if(FindObjectOfType<LC_PuzzleController>().checkOrder())
                    //{
                    //    Debug.Log("WIIIIIIIIIIIIIIIIIIIII");
                    //}
                    
                }
            }
            //else
            //{
            //    if (currentGrabbedItem != null)
            //    {
            //        holdingItem = !holdingItem;
            //        currentGrabbedItem.transform.SetParent(null);
            //        rb.isKinematic = false;
            //        rb.useGravity = true;

            //        currentGrabbedItem = null;
            //        rb = null;
            //    }
            //}
        }
    }
}
