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
    Dictionary<int, List<int>> grabToPlaceLayers = new Dictionary<int, List<int>>();

    private void Start()
    {
        foreach (var pairOfLayers in grabToPlaceLayers_List)
        {
            grabToPlaceLayers[pairOfLayers.grabbableLayerIndex] = pairOfLayers.placeLayerIndexes;
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
                    int grabbedLayer = currentGrabbedItem.layer;
                    int hitLayer = hit.transform.gameObject.layer;

                    if (grabToPlaceLayers.TryGetValue(grabbedLayer, out List<int> allowedPlaceLayer))
                    {
                        if (allowedPlaceLayer.Contains(hitLayer))
                        {
                            if(hitLayer == 0)
                            {
                                currentGrabbedItem.transform.SetParent(null);

                                if (rb != null)
                                {
                                    rb.isKinematic = false;
                                    rb.useGravity = true;
                                    rb = null;
                                }

                                currentGrabbedItem = null;
                            } 
                            else
                            {
                                Transform placePoint = HasChildWithTag(hit.transform.gameObject);

                                if (placePoint != null)
                                {
                                    currentGrabbedItem.transform.SetParent(null);

                                    currentGrabbedItem.transform.position = placePoint.position;

                                    if (hitLayer == 9 || hitLayer == 14 || hitLayer == 19)
                                    {
                                        Quaternion newRotation = new Quaternion(hit.transform.rotation.x, hit.transform.rotation.y + 90f, hit.transform.rotation.z, hit.transform.rotation.w);
                                        currentGrabbedItem.transform.rotation = newRotation;
                                    }


                                    if (rb != null)
                                    {
                                        rb.isKinematic = false;
                                        rb.useGravity = true;
                                        rb = null;
                                    }

                                    currentGrabbedItem = null;
                                } 
                                else
                                {
                                    currentGrabbedItem.transform.SetParent(null);

                                    currentGrabbedItem.transform.position = hit.transform.position;

                                    if (hitLayer == 9 || hitLayer == 14 || hitLayer == 19)
                                    {
                                        Quaternion newRotation = new Quaternion(hit.transform.rotation.x, hit.transform.rotation.y + 90f, hit.transform.rotation.z, hit.transform.rotation.w);
                                        currentGrabbedItem.transform.rotation = newRotation;
                                    }

                                    if (rb != null)
                                    {
                                        rb.isKinematic = false;
                                        rb.useGravity = true;
                                        rb = null;
                                    }

                                    currentGrabbedItem = null;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    Transform HasChildWithTag(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag("PlaceHolder"))
                return child;
        }

        return null;
    }
}
