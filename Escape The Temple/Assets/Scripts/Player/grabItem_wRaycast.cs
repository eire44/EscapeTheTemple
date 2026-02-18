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

        //if (Physics.Raycast(ray, out hit, distance))
        //{
        //    if (hit.collider.gameObject.layer == grabbableLayer)
        //    {
        //        if (!holdingItem)
        //        {
        //            if (Input.GetKeyDown(KeyCode.R))
        //            {
        //                holdingItem = !holdingItem;
        //                currentGrabbedItem = hit.collider.gameObject;
        //                rb = currentGrabbedItem.GetComponent<Rigidbody>();

        //                currentGrabbedItem.transform.SetParent(posHand.transform);
        //                currentGrabbedItem.transform.position = posHand.transform.position;

        //                rb.isKinematic = true;
        //                rb.useGravity = false;
        //            }
        //        }
        //    }
        //    else if(hit.collider.gameObject.layer == placeLayer)
        //    {
        //        if(holdingItem)
        //        {
        //            if (Input.GetKeyDown(KeyCode.R))
        //            {
        //                currentGrabbedItem.transform.SetParent(null);

        //                // Colocarlo en el punto donde impacta el raycast
        //                currentGrabbedItem.transform.position = hit.transform.position;
        //                currentGrabbedItem.transform.rotation = hit.transform.rotation;

        //                rb.isKinematic = false;
        //                rb.useGravity = true;

        //                currentGrabbedItem = null;
        //                rb = null;
        //            }
        //        }
        //    } else
        //    {
        //        if (holdingItem)
        //        {
        //            if (Input.GetKeyDown(KeyCode.R))
        //            {
        //                holdingItem = !holdingItem;
        //                currentGrabbedItem.transform.SetParent(null);
        //                rb.isKinematic = false;
        //                rb.useGravity = true;

        //                currentGrabbedItem = null;
        //                rb = null;
        //            }
        //        }
        //    }
        //}







        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Physics.Raycast(ray, out hit, distance, grabbableLayer))
            {
                if (currentGrabbedItem == null)
                {
                    Debug.Log(hit.collider.name);
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
                    Debug.Log(hit.collider.name);
                    currentGrabbedItem.transform.SetParent(null);

                    // Colocarlo en el punto donde impacta el raycast
                    currentGrabbedItem.transform.position = hit.transform.position;
                    currentGrabbedItem.transform.rotation = hit.transform.rotation;

                    rb.isKinematic = false;
                    rb.useGravity = true;

                    currentGrabbedItem = null;
                    rb = null;
                }
            } 
            else
            {
                //holdingItem = !holdingItem;
                //currentGrabbedItem.transform.SetParent(null);
                //rb.isKinematic = false;
                //rb.useGravity = true;

                //currentGrabbedItem = null;
                //rb = null;
            }
        }
    }

    //public Camera playerCamera;
    //public float distance = 4f;
    //public GameObject posHand;

    //GameObject currentGrabbedItem;
    //Rigidbody rb;

    //bool holdingItem = false;

    //void Update()
    //{
    //    Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit, distance))
    //    {
    //        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Life Cycle Puzzle"))
    //        {
    //            if (holdingItem)
    //            {
    //                if (Input.GetKeyDown(KeyCode.R))
    //                {
    //                    if (currentGrabbedItem != null)
    //                    {
    //                        holdingItem = !holdingItem;
    //                        currentGrabbedItem.transform.SetParent(null);
    //                        rb.isKinematic = false;
    //                        rb.useGravity = true;

    //                        currentGrabbedItem = null;
    //                        rb = null;
    //                    }
    //                }
    //            } 
    //            else
    //            {
    //                if (Input.GetKeyDown(KeyCode.R))
    //                {
    //                    if (currentGrabbedItem == null)
    //                    {
    //                        holdingItem = !holdingItem;
    //                        currentGrabbedItem = hit.collider.gameObject;
    //                        rb = currentGrabbedItem.GetComponent<Rigidbody>();

    //                        currentGrabbedItem.transform.SetParent(posHand.transform);
    //                        currentGrabbedItem.transform.position = posHand.transform.position;

    //                        rb.isKinematic = true;
    //                        rb.useGravity = false;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}
