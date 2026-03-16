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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, distance, grabbableLayer))
            {
                if (hit.transform.gameObject.layer == 17)
                {
                    hit.transform.gameObject.GetComponent<phrasesController>().grabPaper();
                } else if(hit.transform.gameObject.layer == 13)
                {
                    hit.transform.gameObject.GetComponent<kayPieceSound>().handleKeypiece(true);
                }

                if (currentGrabbedItem == null)
                {
                    if(hit.transform.gameObject.layer == 20)
                    {
                        ABL_GrabObjectFromBalance objectHit = hit.transform.GetComponent<ABL_GrabObjectFromBalance>();
                        currentGrabbedItem = FindObjectOfType<ABL_PuzzleController>().getObject(hit.transform.GetComponent<placeableObjectController>().index);
                        objectHit.balanceSide.GetComponent<ABL_balanceSidesController>().removeObjectFromBalance(hit.transform.gameObject);
                    }
                    else
                    {
                        currentGrabbedItem = hit.collider.gameObject;
                    }
                    
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


                    if (hit.transform.gameObject.layer == 20)
                    {
                        ABL_GrabObjectFromBalance objectHit = hit.transform.GetComponent<ABL_GrabObjectFromBalance>();
                        currentGrabbedItem = FindObjectOfType<ABL_PuzzleController>().getObject(hit.transform.GetComponent<placeableObjectController>().index);
                        objectHit.balanceSide.GetComponent<ABL_balanceSidesController>().removeObjectFromBalance(hit.transform.gameObject);
                    }
                    else
                    {
                        currentGrabbedItem = hit.collider.gameObject;
                    }
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
                                dropOnTheGround();
                            }
                            else if (hitLayer == 22)
                            {
                                incenseBurnerController IBcontroller = hit.transform.GetComponent<incenseBurnerController>();
                                IP_HerbController herbController = currentGrabbedItem.GetComponent<IP_HerbController>();
                                IBcontroller.placeHerb(herbController);
                            }
                            else
                            {
                                Transform placePoint = HasChildWithTag(hit.transform.gameObject);

                                if (placePoint != null)
                                {
                                    currentGrabbedItem.transform.SetParent(null);

                                    currentGrabbedItem.transform.position = placePoint.position;

                                    if (hitLayer == 9)
                                    {
                                        Quaternion newRotation = new Quaternion(hit.transform.rotation.x, hit.transform.rotation.y + 90f, hit.transform.rotation.z, hit.transform.rotation.w);
                                        currentGrabbedItem.transform.rotation = newRotation;
                                    }
                                    else if (hitLayer == 14)
                                    {
                                        currentGrabbedItem.transform.localRotation = Quaternion.Euler(-90f, 0f, 180f);
                                        keyPieceHolder_Controller kPieceHolder = hit.transform.GetComponent<keyPieceHolder_Controller>();
                                        kPieceHolder.placeKeyPiece(currentGrabbedItem);
                                    }
                                    else if (hitLayer == 16)
                                    {
                                        currentGrabbedItem.transform.localRotation = Quaternion.Euler(-90f, -90f, 0f);
                                    } 
                                    else if(hitLayer == 19)
                                    {
                                        ABL_balanceSidesController bSC = hit.transform.GetComponent<ABL_balanceSidesController>();
                                        bSC.sumWeight(currentGrabbedItem);
                                    }
                                    else if (hitLayer == 24)
                                    {
                                        currentGrabbedItem.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
                                        FindObjectOfType<ESP_LeverMovement>().placeLever();
                                    }

                                    if (currentGrabbedItem.transform.gameObject.layer == 17)
                                    {
                                        currentGrabbedItem.transform.gameObject.GetComponent<phrasesController>().grabPaper();
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

                                    if (hitLayer == 9)
                                    {
                                        Quaternion newRotation = new Quaternion(hit.transform.rotation.x, hit.transform.rotation.y + 90f, hit.transform.rotation.z, hit.transform.rotation.w);
                                        currentGrabbedItem.transform.rotation = newRotation;
                                    }
                                    else if (hitLayer == 14)
                                    {
                                        currentGrabbedItem.transform.localRotation = Quaternion.Euler(-90f, 0f, 180f);
                                        keyPieceHolder_Controller kPieceHolder = hit.transform.GetComponent<keyPieceHolder_Controller>();
                                        kPieceHolder.placeKeyPiece(currentGrabbedItem);
                                    }
                                    else if (hitLayer == 16)
                                    {
                                        currentGrabbedItem.transform.localRotation = Quaternion.Euler(-90f, -90f, 0f);
                                    }
                                    else if (hitLayer == 19)
                                    {
                                        ABL_balanceSidesController bSC = hit.transform.GetComponent<ABL_balanceSidesController>();
                                        bSC.sumWeight(currentGrabbedItem);
                                    }
                                    else if (hitLayer == 24)
                                    {
                                        currentGrabbedItem.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
                                        FindObjectOfType<ESP_LeverMovement>().placeLever();
                                    }

                                    if (currentGrabbedItem.transform.gameObject.layer == 17)
                                    {
                                        currentGrabbedItem.transform.gameObject.GetComponent<phrasesController>().grabPaper();
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

    public void dropOnTheGround()
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
