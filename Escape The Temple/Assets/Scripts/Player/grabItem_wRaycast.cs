using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ESP_Controller;

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

    public List<GrabToPlaceLayers> grabToPlaceLayers_List;
    Dictionary<int, int> grabToPlaceLayers = new Dictionary<int, int>();

    private void Start()
    {
        foreach (var pairOfLayers in grabToPlaceLayers_List)
        {
            int grabLayer = LayerMask.NameToLayer(pairOfLayers.grabbableLayerName);
            int placeLayer = LayerMask.NameToLayer(pairOfLayers.placeLayerName);

            grabToPlaceLayers[grabLayer] = placeLayer;
            Debug.Log("LAYER "+ grabLayer + " FOR " + placeLayer);
        }
    }

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

                    if(rb != null)
                    {
                        rb.isKinematic = true;
                        rb.useGravity = false;
                    }
                } 
                else
                {
                    currentGrabbedItem.transform.SetParent(null);

                    //currentGrabbedItem.transform.position = hit.transform.position;
                    //currentGrabbedItem.transform.rotation = hit.transform.rotation;

                    if(rb != null)
                    {
                        rb.isKinematic = false;
                        rb.useGravity = true;
                        rb = null;
                    }
                    
                    currentGrabbedItem = null;
                    

                    currentGrabbedItem = hit.collider.gameObject;
                    rb = currentGrabbedItem.GetComponent<Rigidbody>();

                    currentGrabbedItem.transform.SetParent(posHand.transform);
                    currentGrabbedItem.transform.position = posHand.transform.position;


                    if (rb != null)
                    {
                        rb.isKinematic = true;
                        rb.useGravity = false;
                    }
                }
            } else if (Physics.Raycast(ray, out hit, distance, placeLayer))
            {
                if (currentGrabbedItem != null)
                {
                    if (grabToPlaceLayers[currentGrabbedItem.layer] == hit.transform.gameObject.layer)
                    {
                        currentGrabbedItem.transform.SetParent(null);

                        currentGrabbedItem.transform.position = hit.transform.position;
                        currentGrabbedItem.transform.rotation = hit.transform.rotation;

                        if (rb != null)
                        {
                            rb.isKinematic = false;
                            rb.useGravity = true;
                            rb = null;
                        }


                        currentGrabbedItem = null;

                        //if(FindObjectOfType<LC_PuzzleController>().checkOrder())
                        //{
                        //    Debug.Log("WIIIIIIIIIIIIIIIIIIIII");
                        //}
                    }
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
